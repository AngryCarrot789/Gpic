using Gpic.Core.Actions;
using Gpic.Core.Shortcuts.Managing;
using Gpic.Core.Shortcuts.ViewModels;
using Gpic.Core;
using Gpic.Resources.I18N;
using Gpic.Shortcuts.Converters;
using Gpic.Shortcuts;
using Gpic.Utils;
using Gpic.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Gpic.Core.Editor;
using Gpic.Core.Editor.ViewModels;
using Gpic.Core.Utils;
using SkiaSharp;
using System.Windows.Input;

namespace Gpic {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        public static Cursor PencilBrush_Square;

        private AppSplashScreen splash;

        public App() {

        }

        private async Task SetActivity(string activity) {
            this.splash.CurrentActivity = activity;
            await this.Dispatcher.InvokeAsync(() => {
            }, DispatcherPriority.ApplicationIdle);
        }

        public async Task InitApp() {
            await this.SetActivity("Loading services...");
            string[] envArgs = Environment.GetCommandLineArgs();
            if (envArgs.Length > 0 && Path.GetDirectoryName(envArgs[0]) is string dir && dir.Length > 0) {
                Directory.SetCurrentDirectory(dir);
            }

            IoC.Dispatcher = new DispatcherDelegate(this.Dispatcher);
            IoC.OnShortcutModified = (x) => {
                if (!string.IsNullOrWhiteSpace(x)) {
                    ShortcutManager.Instance.InvalidateShortcutCache();
                    GlobalUpdateShortcutGestureConverter.BroadcastChange();
                }
            };

            List<(TypeInfo, ServiceImplementationAttribute)> serviceAttributes = new List<(TypeInfo, ServiceImplementationAttribute)>();
            List<(TypeInfo, ActionRegistrationAttribute)> attributes = new List<(TypeInfo, ActionRegistrationAttribute)>();

            // Process all attributes in a single scan, instead of multiple scans for services, actions, etc
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies()) {
                foreach (TypeInfo typeInfo in assembly.DefinedTypes) {
                    ServiceImplementationAttribute serviceAttribute = typeInfo.GetCustomAttribute<ServiceImplementationAttribute>();
                    if (serviceAttribute?.Type != null) {
                        serviceAttributes.Add((typeInfo, serviceAttribute));
                    }

                    ActionRegistrationAttribute actionAttribute = typeInfo.GetCustomAttribute<ActionRegistrationAttribute>();
                    if (actionAttribute != null) {
                        attributes.Add((typeInfo, actionAttribute));
                    }
                }
            }

            foreach ((TypeInfo, ServiceImplementationAttribute) tuple in serviceAttributes) {
                object instance;
                try {
                    instance = Activator.CreateInstance(tuple.Item1);
                }
                catch (Exception e) {
                    throw new Exception($"Failed to create implementation of {tuple.Item2.Type} as {tuple.Item1}", e);
                }

                IoC.Instance.Register(tuple.Item2.Type, instance);
            }

            await this.SetActivity("Loading localization...");
            LocalizationController.SetLang(LangType.En);

            await this.SetActivity("Loading shortcuts and the action manager...");
            ShortcutManager.Instance = new WPFShortcutManager();
            ActionManager.Instance = new ActionManager();
            InputStrokeViewModel.KeyToReadableString = KeyStrokeStringConverter.ToStringFunction;
            InputStrokeViewModel.MouseToReadableString = MouseStrokeStringConverter.ToStringFunction;

            foreach ((TypeInfo type, ActionRegistrationAttribute attribute) in attributes.OrderBy(x => x.Item2.RegistrationOrder)) {
                AnAction action;
                try {
                    action = (AnAction) Activator.CreateInstance(type, true);
                }
                catch (Exception e) {
                    throw new Exception($"Failed to create an instance of the registered action '{type.FullName}'", e);
                }

                if (attribute.OverrideExisting && ActionManager.Instance.GetAction(attribute.ActionId) != null) {
                    ActionManager.Instance.Unregister(attribute.ActionId);
                }

                ActionManager.Instance.Register(attribute.ActionId, action);
            }

            await this.SetActivity("Loading keymap...");
            string keymapFilePath = Path.GetFullPath(@"Keymap.xml");
            if (File.Exists(keymapFilePath)) {
                using (FileStream stream = File.OpenRead(keymapFilePath)) {
                    ShortcutGroup group = WPFKeyMapSerialiser.Instance.Deserialise(stream);
                    WPFShortcutManager.WPFInstance.SetRoot(group);
                }
            }
            else {
                await IoC.MessageDialogs.ShowMessageAsync("No keymap available", "Keymap file does not exist: " + keymapFilePath + $".\nCurrent directory: {Directory.GetCurrentDirectory()}\nCommand line args:{string.Join("\n", Environment.GetCommandLineArgs())}");
            }
        }

        private async void Application_Startup(object sender, StartupEventArgs e) {
            // Dialogs may be shown, becoming the main window, possibly causing the
            // app to shutdown when the mode is OnMainWindowClose or OnLastWindowClose

#if false
            this.ShutdownMode = ShutdownMode.OnMainWindowClose;
            this.MainWindow = new PropertyPageDemoWindow();
            this.MainWindow.Show();
#else
            this.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            this.MainWindow = this.splash = new AppSplashScreen();
            this.splash.Show();

            try {
                await this.InitApp();
            }
            catch (Exception ex) {
                if (IoC.MessageDialogs != null) {
                    await IoC.MessageDialogs.ShowMessageExAsync("App init failed", "Failed to start Gpic", ex.GetToString());
                }
                else {
                    MessageBox.Show("Failed to start Gpic:\n\n" + ex, "Fatal App init failure");
                }

                this.Dispatcher.Invoke(() => {
                    this.Shutdown(0);
                }, DispatcherPriority.Background);
                return;
            }

            await this.SetActivity("Loading Gpic main window...");
            MainWindow window = new MainWindow();
            window.Editor = new EditorViewModel(new GpicEditor());
            this.splash.Close();
            this.MainWindow = window;
            this.ShutdownMode = ShutdownMode.OnMainWindowClose;
            window.Show();
            await this.Dispatcher.Invoke(async () => {
                await this.OnVideoEditorLoaded(window);
            }, DispatcherPriority.Loaded);
#endif
        }

        public async Task OnVideoEditorLoaded(MainWindow editor) {
            editor.Editor.SetupInitial();
        }
    }
}