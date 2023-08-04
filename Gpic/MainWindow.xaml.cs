using Gpic.Core;
using Gpic.Core.Editor.ViewModels;
using Gpic.Views;
using SkiaSharp;

namespace Gpic {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : WindowEx {
        public EditorViewModel Editor {
            get => (EditorViewModel) this.DataContext;
            set => this.DataContext = value;
        }

        public MainWindow() {
            this.InitializeComponent();
        }

        public void SetupInitial() {
            SKBitmap bitmap = this.Editor.ActiveCanvas.Bitmap;
            using (SKCanvas canvas = new SKCanvas(bitmap)) {
                using (SKPaint p = new SKPaint() {Color = SKColors.Black}) {
                    canvas.DrawRect(new SKRect(0, 0, bitmap.Width, bitmap.Height), p);
                }

                using (SKPaint paint = new SKPaint() {Color = SKColors.WhiteSmoke}) {
                    using (SKTypeface face = SKTypeface.FromFamilyName("Consolas")) {
                        using (SKFont font = new SKFont(face, 25f)) {
                            canvas.DrawText("Use CTRL + MouseWheel to zoom", 0, 20, font, paint);
                            canvas.DrawText("Use the left mouse to draw pixels", 0, 60, font, paint);
                            canvas.DrawText("(you won't be able to see the pixels until you zoom in enough)", 0, 100, font, paint);
                        }
                    }
                }
            }

            this.Editor.ActiveCanvas.RaiseRenderInvalidated();
        }
    }
}