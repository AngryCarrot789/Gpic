using System;
using System.Diagnostics.Tracing;
using SkiaSharp;

namespace Gpic.Core.Editor.ViewModels {
    public class CanvasViewModel : BaseViewModel {
        private ICanvasView canvasView;

        /// <summary>
        /// This canvas' current width
        /// </summary>
        public int Width => this.Model.Width;

        /// <summary>
        /// This canvas' current height
        /// </summary>
        public int Height => this.Model.Height;

        public SKBitmap Bitmap => this.Model.Bitmap;

        public GpicCanvas Model { get; }

        public string DisplayName {
            get => this.Model.DisplayName;
            set => this.RaisePropertyChanged(ref this.Model.DisplayName, value);
        }

        public event InvalidateRenderEventHandler InvalidateRender;

        public CanvasViewModel(GpicCanvas model) {
            this.Model = model ?? throw new ArgumentNullException(nameof(model));
        }

        /// <summary>
        /// Fires the <see cref="InvalidateRender"/> event, notifying any UI components to re-draw this canvas
        /// </summary>
        public void RaiseRenderInvalidated() {
            this.InvalidateRender?.Invoke(this);
        }
    }
}