using System.Numerics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Gpic.Core.Editor;
using Gpic.Core.Editor.Brushes;
using Gpic.Core.Editor.Brushes.ViewModels;
using Gpic.Core.Editor.ViewModels;
using Gpic.Utils;
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
            this.MainCanvas.MouseLeftButtonDown += this.OnDrawPixel;
            this.MainCanvas.MouseMove += this.MainCanvasOnMouseMove;
            this.VPViewBox.FitContentToCenter();
            RenderOptions.SetBitmapScalingMode(this.MainCanvas, BitmapScalingMode.NearestNeighbor);
            RenderOptions.SetEdgeMode(this.MainCanvas, EdgeMode.Aliased);

            this.PreviewKeyDown += this.OnKeyState;
            this.PreviewKeyUp += this.OnKeyState;
        }

        private void OnKeyState(object sender, KeyEventArgs e) {
            // if (this.Editor.ActiveBrush is PencilBrushViewModel) {
            //     this.Editor.IsToolUsingSecondaryColour = KBUtils.AreModsPressed(ModifierKeys.Control | ModifierKeys.Shift);
            // }
        }

        private void MainCanvasOnMouseMove(object sender, MouseEventArgs e) {
            if (e.LeftButton == MouseButtonState.Pressed) {
                this.DrawPixelAt(e.GetPosition(this.MainCanvas));
            }
        }

        private void OnDrawPixel(object sender, MouseButtonEventArgs e) {
            this.DrawPixelAt(e.GetPosition(this.MainCanvas));
        }

        private void DrawPixelAt(Point point) {
            this.DrawPixelAt(new Vector2((float) point.X, (float) point.Y));
        }

        private void DrawPixelAt(Vector2 point) {
            GpicEditor editor = this.Editor.Model;
            if (editor.ActiveBrush != null && this.MainCanvas.BeginRender(out SKSurface surface)) {
                editor.ActiveBrush.Draw(editor, editor.MainCanvas, point);
                surface.Canvas.DrawBitmap(this.Editor.MainCanvas.Bitmap, 0, 0);
                this.MainCanvas.EndRender();
            }
        }

        public void SetupInitial() {
            if (this.MainCanvas.BeginRender(out SKSurface surface)) {
                SKBitmap bitmap = this.Editor.MainCanvas.Bitmap;
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

                surface.Canvas.DrawBitmap(bitmap, 0, 0);
                this.MainCanvas.EndRender();
            }
        }
    }
}