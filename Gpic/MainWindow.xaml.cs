using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Gpic.Views;
using SkiaSharp;

namespace Gpic {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : WindowEx {
        public SKBitmap DrawingBitmap;

        public MainWindow() {
            this.InitializeComponent();
            this.MainCanvas.MouseLeftButtonDown += this.OnDrawPixel;
            this.MainCanvas.MouseMove += this.MainCanvasOnMouseMove;
            this.VPViewBox.FitContentToCenter();
            RenderOptions.SetBitmapScalingMode(this.MainCanvas, BitmapScalingMode.NearestNeighbor);
            RenderOptions.SetEdgeMode(this.MainCanvas, EdgeMode.Aliased);
        }

        private int lastX, lastY;

        private void MainCanvasOnMouseMove(object sender, MouseEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) {
                this.DrawPixelAt(e.GetPosition(this.MainCanvas));
            }
        }

        private void OnDrawPixel(object sender, MouseButtonEventArgs e) {
            this.DrawPixelAt(e.GetPosition(this.MainCanvas));
        }

        private void DrawPixelAt(Point point) {
            int x = (int) point.X;
            int y = (int) point.Y;
            if (x == this.lastX && y == this.lastY) {
                return;
            }

            this.lastX = x;
            this.lastY = y;
            this.DrawPixelAt(x, y);
        }

        private void DrawPixelAt(int x, int y) {
            if (this.MainCanvas.BeginRender(out SKSurface surface)) {
                this.DrawingBitmap.SetPixel(x, y, SKColors.Aqua);
                surface.Canvas.DrawBitmap(this.DrawingBitmap, 0, 0);
                this.MainCanvas.EndRender();
            }
        }

        public void SetupInitial() {
            if (this.MainCanvas.BeginRender(out SKSurface surface)) {
                this.DrawingBitmap = new SKBitmap(this.MainCanvas.FrameInfo);
                using (SKCanvas canvas = new SKCanvas(this.DrawingBitmap)) {
                    using (SKPaint p = new SKPaint() {Color = SKColors.Black}) {
                        canvas.DrawRect(new SKRect(0, 0, this.DrawingBitmap.Width, this.DrawingBitmap.Height), p);
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

                surface.Canvas.DrawBitmap(this.DrawingBitmap, 0, 0);
                this.MainCanvas.EndRender();
            }
        }
    }
}