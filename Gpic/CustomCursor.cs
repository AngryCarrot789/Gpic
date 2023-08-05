using System.Drawing;
using System.Windows;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace Gpic {
    public class CustomCursor : FrameworkElement {
        public static readonly DependencyProperty PencilWidthProperty = DependencyProperty.Register("PencilWidth", typeof(int), typeof(CustomCursor), new PropertyMetadata(0, (d, e) => ((CustomCursor) d).UpdateCursorSize()));
        public static readonly DependencyProperty PencilHeightProperty = DependencyProperty.Register("PencilHeight", typeof(int), typeof(CustomCursor), new PropertyMetadata(0, (d, e) => ((CustomCursor) d).UpdateCursorSize()));
        public static readonly DependencyProperty ZoomScaleProperty = DependencyProperty.Register("ZoomScale", typeof(double), typeof(CustomCursor), new PropertyMetadata(1d));

        public int PencilWidth {
            get => (int) this.GetValue(PencilWidthProperty);
            set => this.SetValue(PencilWidthProperty, value);
        }

        public int PencilHeight {
            get => (int) this.GetValue(PencilHeightProperty);
            set => this.SetValue(PencilHeightProperty, value);
        }

        public double ZoomScale {
            get => (double) this.GetValue(ZoomScaleProperty);
            set => this.SetValue(ZoomScaleProperty, value);
        }

        private readonly Rectangle rectangle;

        public CustomCursor() {
            this.rectangle = new Rectangle();
            this.AddVisualChild(this.rectangle);
        }

        public void UpdateCursorSize() {
            this.rectangle.Width = this.PencilWidth / this.ZoomScale;
            this.rectangle.Height = this.PencilHeight / this.ZoomScale;
        }
    }
}