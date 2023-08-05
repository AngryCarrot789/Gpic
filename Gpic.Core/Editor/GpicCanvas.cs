using SkiaSharp;

namespace Gpic.Core.Editor {
    /// <summary>
    /// A class for storing information about a canvas
    /// </summary>
    public class GpicCanvas {
        public string FilePath;
        public int Width, Height;
        public string DisplayName;

        /// <summary>
        /// The current skia bitmap used for storing the pixel data
        /// </summary>
        public SKBitmap Bitmap;

        public GpicEditor Editor;

        public GpicCanvas() {
        }

        public void SetSizeAndCreateBitmap(int width, int height) {
            this.Width = width;
            this.Height = height;
            this.CreateBitmap();
            if (string.IsNullOrEmpty(this.DisplayName)) {
                this.DisplayName = $"Canvas ({width} x {height})";
            }
        }

        public void CreateBitmap() {
            this.Bitmap?.Dispose();
            this.Bitmap = new SKBitmap(this.Width, this.Height, SKColorType.Bgra8888, SKAlphaType.Premul);
        }
    }
}