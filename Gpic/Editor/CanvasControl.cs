using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Gpic.Controls;
using Gpic.Core;
using Gpic.Core.Editor;
using Gpic.Core.Editor.ViewModels;
using Gpic.Core.Utils;
using Gpic.Utils;
using SkiaSharp;

namespace Gpic.Editor {
    [TemplatePart(Name = "PART_FreeMoveViewPort", Type = typeof(FreeMoveViewPortV2))]
    [TemplatePart(Name = "PART_SkiaViewPort", Type = typeof(SKAsyncViewPort))]
    public class CanvasControl : Control, ICanvasView {
        public static readonly DependencyProperty ZoomScaleProperty = FreeMoveViewPortV2.ZoomScaleProperty.AddOwner(typeof(CanvasControl));
        public static readonly DependencyProperty IsDrawingWithCursorProperty = DependencyProperty.Register("IsDrawingWithCursor", typeof(bool), typeof(CanvasControl), new PropertyMetadata(BoolBox.False));
        public static readonly DependencyProperty EditorProperty = DependencyProperty.Register("Editor", typeof(EditorViewModel), typeof(CanvasControl), new PropertyMetadata(null));
        public static readonly DependencyProperty CanvasProperty =
            DependencyProperty.Register(
                "Canvas",
                typeof(CanvasViewModel),
                typeof(CanvasControl),
                new PropertyMetadata(null, (o, args) => {
                    if (args.OldValue is CanvasViewModel oldVm)
                        oldVm.InvalidateRender -= ((CanvasControl) o).invalidateRenderEventHandler;
                    if (args.NewValue is CanvasViewModel newVm)
                        newVm.InvalidateRender += ((CanvasControl) o).invalidateRenderEventHandler;
                }));

        /// <summary>
        /// The zoom level of the view port
        /// </summary>
        public double ZoomScale {
            get => (double) this.GetValue(ZoomScaleProperty);
            set => this.SetValue(ZoomScaleProperty, value);
        }

        /// <summary>
        /// Whether or not the user has their left mouse down and is drawing with the mouse
        /// </summary>
        public bool IsDrawingWithCursor {
            get => (bool) this.GetValue(IsDrawingWithCursorProperty);
            set => this.SetValue(IsDrawingWithCursorProperty, value.Box());
        }

        /// <summary>
        /// Gets or sets the canvas editor, aka main view, for this canvas control. This is used to know what brush to draw with, what brush colours to use, etc
        /// </summary>
        public EditorViewModel Editor {
            get => (EditorViewModel) this.GetValue(EditorProperty);
            set => this.SetValue(EditorProperty, value);
        }

        /// <summary>
        /// The canvas that this control will draw into and generally modify
        /// </summary>
        public CanvasViewModel Canvas {
            get => (CanvasViewModel) this.GetValue(CanvasProperty);
            set => this.SetValue(CanvasProperty, value);
        }

        private readonly InvalidateRenderEventHandler invalidateRenderEventHandler;
        private FreeMoveViewPortV2 PART_FreeMoveViewPort;
        private SKAsyncViewPort PART_SkiaViewPort;

        private Point? lastClickPoint;
        private Point? lastMousePoint;

        public CanvasControl() {
            this.Loaded += this.OnLoaded;
            this.invalidateRenderEventHandler = this.OnCanvasRenderInvalidated;
        }

        public Point GetPointOnCanvas(MouseEventArgs e) {
            return e.GetPosition(this.PART_SkiaViewPort);
        }

        private void OnCanvasRenderInvalidated(CanvasViewModel canvas) {
            if (this.Canvas == canvas) {
                this.InvalidateRender();
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e) {
            this.Loaded -= this.OnLoaded;
            this.InvalidateRender();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) {
            if (!this.IsDrawingWithCursor && (this.IsFocused || this.Focus())) {
                this.lastMousePoint = this.lastClickPoint = this.GetPointOnCanvas(e);
                this.IsDrawingWithCursor = true;
                this.CaptureMouse();
            }

            base.OnMouseLeftButtonDown(e);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e) {
            if (this.IsMouseCaptured && this.IsDrawingWithCursor) {
                e.Handled = true;
                this.lastMousePoint = this.lastClickPoint = null;
                this.ClearValue(IsDrawingWithCursorProperty);
                this.ReleaseMouseCapture();
            }

            base.OnMouseLeftButtonUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            if (!this.IsDrawingWithCursor) {
                return;
            }

            if (e.LeftButton != MouseButtonState.Pressed) {
                if (ReferenceEquals(e.MouseDevice.Captured, this))
                    this.ReleaseMouseCapture();
                this.ClearValue(IsDrawingWithCursorProperty);
                this.lastClickPoint = null;
                return;
            }

            Point pos = this.GetPointOnCanvas(e);
            EditorViewModel editor = this.Editor;
            CanvasViewModel canvas = this.Canvas;
            if (editor != null && canvas != null && editor.ToolPalette.ActiveBrush != null) {
                editor.ToolPalette.ActiveBrush.Model.Draw(editor.Model, canvas.Model, (this.lastMousePoint ?? pos).ToVec2(), new Vector2((float) pos.X, (float) pos.Y));
                this.InvalidateRender();
            }

            this.lastMousePoint = pos;
        }

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            this.PART_FreeMoveViewPort = (FreeMoveViewPortV2) this.GetTemplateChild("PART_FreeMoveViewPort");
            this.PART_SkiaViewPort = (SKAsyncViewPort) this.GetTemplateChild("PART_SkiaViewPort");
            if (this.PART_SkiaViewPort != null) {
                RenderOptions.SetBitmapScalingMode(this.PART_SkiaViewPort, BitmapScalingMode.NearestNeighbor);
                RenderOptions.SetEdgeMode(this.PART_SkiaViewPort, EdgeMode.Aliased);
            }
        }

        public bool BeginCanvasRender(out SKSurface surface) {
            if (this.PART_SkiaViewPort == null) {
                surface = null;
                return false;
            }

            return this.PART_SkiaViewPort.BeginRender(out surface);
        }

        public void EndCanvasRender() => this.PART_SkiaViewPort?.EndRender();

        public void InvalidateRender() {
            CanvasViewModel canvas = this.Canvas;
            if (canvas != null) {
                if (this.BeginCanvasRender(out SKSurface surface)) {
                    surface.Canvas.DrawBitmap(canvas.Bitmap, 0, 0);
                    this.EndCanvasRender();
                }
            }
        }

        protected override void OnRender(DrawingContext dc) {
            base.OnRender(dc);
            if (this.Canvas is CanvasViewModel canvas && this.BeginCanvasRender(out SKSurface suface)) {
                suface.Canvas.DrawBitmap(canvas.Bitmap, 0, 0);
                this.EndCanvasRender();
            }
        }
    }
}