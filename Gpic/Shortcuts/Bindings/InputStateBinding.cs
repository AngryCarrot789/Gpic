using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Gpic.Core.Utils;

namespace Gpic.Shortcuts.Bindings {
    public class InputStateBinding : Freezable {
        public static readonly DependencyProperty CommandProperty = InputBinding.CommandProperty.AddOwner(typeof(InputStateBinding));
        public static readonly DependencyProperty InputStatePathProperty = DependencyProperty.Register(nameof(InputStatePath), typeof(string), typeof(InputStateBinding));
        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register("IsActive", typeof(bool), typeof(InputStateBinding), new FrameworkPropertyMetadata(BoolBox.False));

        /// <summary>
        /// The command to execute when the shortcut is activated
        /// </summary>
        [Localizability(LocalizationCategory.NeverLocalize)]
        [TypeConverter("System.Windows.Input.CommandConverter, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, Custom=null")]
        public ICommand Command {
            get => (ICommand) this.GetValue(CommandProperty);
            set => this.SetValue(CommandProperty, value);
        }

        /// <summary>
        /// The full path of the shortcut that must be activated in order for this binding's command to be executed
        /// </summary>
        public string InputStatePath {
            get => (string) this.GetValue(InputStatePathProperty);
            set => this.SetValue(InputStatePathProperty, value);
        }

        public bool IsActive {
            get => (bool) this.GetValue(IsActiveProperty);
            set => this.SetValue(IsActiveProperty, value.Box());
        }

        public InputStateBinding() {

        }

        public InputStateBinding(string inputStatePath, ICommand command) {
            if (string.IsNullOrWhiteSpace(inputStatePath))
                throw new ArgumentException("Input state ID must not be null, empty or consist of only whitespaces", nameof(inputStatePath));
            this.InputStatePath = inputStatePath;
            this.Command = command;
        }

        protected override Freezable CreateInstanceCore() => new InputStateBinding();
    }
}