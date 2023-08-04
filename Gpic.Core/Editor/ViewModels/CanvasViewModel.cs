using System;
using SkiaSharp;

namespace Gpic.Core.Editor.ViewModels {
    public class CanvasViewModel : BaseViewModel {
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

        public CanvasViewModel(GpicCanvas model) {
            this.Model = model ?? throw new ArgumentNullException(nameof(model));
        }
    }
}