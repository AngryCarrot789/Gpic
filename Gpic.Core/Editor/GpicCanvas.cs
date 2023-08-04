using SkiaSharp;

namespace Gpic.Core.Editor {
    /// <summary>
    /// A class for storing information about a canvas
    /// </summary>
    public class GpicCanvas {
        public string FilePath;
        public int Width, Height;

        /// <summary>
        /// The current skia bitmap used for storing the pixel data
        /// </summary>
        public SKBitmap Bitmap;

        public GpicCanvas() {
        }

        public void SetSizeAndCreateBitmap(int width, int height) {
            this.Width = width;
            this.Height = height;
            this.CreateBitmap();
        }

        public void CreateBitmap() {
            this.Bitmap?.Dispose();
            this.Bitmap = new SKBitmap(this.Width, this.Height, SKColorType.Rgba8888, SKAlphaType.Premul);
        }
    }
}