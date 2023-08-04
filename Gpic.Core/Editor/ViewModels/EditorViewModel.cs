using System;
using System.Collections.ObjectModel;
using Gpic.Core.Editor.Tools;
using Gpic.Core.Editor.Tools.Brushes.ViewModels;
using Gpic.Core.Editor.Tools.ViewModels;
using Gpic.Core.Services;
using SkiaSharp;

namespace Gpic.Core.Editor.ViewModels {
    public class EditorViewModel : BaseViewModel {
        private readonly ObservableCollection<CanvasViewModel> canvasses;
        public ReadOnlyObservableCollection<CanvasViewModel> Canvasses { get; }

        private CanvasViewModel activeCanvas;

        /// <summary>
        /// The currently active canvas
        /// </summary>
        public CanvasViewModel ActiveCanvas {
            get => this.activeCanvas;
            set {
                if (value != null && !this.canvasses.Contains(value))
                    throw new InvalidOperationException("Value must be stored in the view model's canvas list");
                if (value != null && !this.Model.Canvasses.Contains(value.Model))
                    throw new InvalidOperationException("Value must be stored in the model's canvas list");
                this.Model.ActiveCanvas = value?.Model;
                this.RaisePropertyChanged(ref this.activeCanvas, value);
            }
        }

        public ToolPaletteViewModel ToolPalette { get; }

        public GpicEditor Model { get; }

        public EditorViewModel(GpicEditor model) {
            this.Model = model ?? throw new ArgumentNullException(nameof(model));
            this.ToolPalette = new ToolPaletteViewModel(model.ToolPalette);
            this.canvasses = new ObservableCollection<CanvasViewModel>();
            this.Canvasses = new ReadOnlyObservableCollection<CanvasViewModel>(this.canvasses);
        }

        public void SetupInitial() {
            GpicCanvas canvas = new GpicCanvas();
            canvas.SetSizeAndCreateBitmap(1280, 720);
            using (SKCanvas skc = new SKCanvas(canvas.Bitmap)) {
                using (SKPaint p = new SKPaint() {Color = SKColors.Black}) {
                    skc.DrawRect(new SKRect(0, 0, 1280, 720), p);
                }

                using (SKPaint paint = new SKPaint() {Color = SKColors.WhiteSmoke}) {
                    using (SKTypeface face = SKTypeface.FromFamilyName("Consolas")) {
                        using (SKFont font = new SKFont(face, 25f)) {
                            skc.DrawText("Use CTRL + MouseWheel to zoom", 0, 20, font, paint);
                            skc.DrawText("Use the left mouse to draw pixels", 0, 60, font, paint);
                            skc.DrawText("(you won't be able to see the pixels until you zoom in enough)", 0, 100, font, paint);
                        }
                    }
                }
            }

            this.Model.Canvasses.Add(canvas);
            CanvasViewModel vm = new CanvasViewModel(canvas);
            this.canvasses.Add(vm);
            this.ActiveCanvas = vm;
            vm.RaiseRenderInvalidated();
        }
    }
}