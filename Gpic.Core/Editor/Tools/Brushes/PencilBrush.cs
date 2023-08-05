using System;
using System.Numerics;
using Gpic.Core.Utils;
using SkiaSharp;

namespace Gpic.Core.Editor.Tools.Brushes {
    public class PencilBrush : BrushTool {
        public int Width, Height;
        public EnumPencilMode Mode;

        public PencilBrush() {
            this.Width = this.Height = 1;
            this.Mode = EnumPencilMode.Square;
        }

        public override void Draw(GpicEditor editor, GpicCanvas canvas, Vector2 p1, Vector2 p2) {
            SKColor colour = editor.ToolPalette.ActiveToolColour;
            SKPath path = new SKPath() {
                Convexity = SKPathConvexity.Concave
            };

            Vector2 change = p2 - p1;
            if (change.X < 0.5d) {
                p1.X = Math.Max((float) Math.Floor(p1.X), p1.X - 0.25f);
                p2.X = Math.Min((float) Math.Ceiling(p2.X), p2.X + 0.25f);
            }
            if (change.Y < 0.5d) {
                p1.Y = Math.Max((float) Math.Floor(p1.Y), p1.Y - 0.25f);
                p2.Y = Math.Min((float) Math.Ceiling(p2.Y), p2.Y + 0.25f);
            }

            path.MoveTo(p1.ToSk());
            path.LineTo(p2.ToSk());
            using (SKCanvas c = new SKCanvas(canvas.Bitmap)) {
                using (SKPaint p = new SKPaint() {Color = colour, IsAntialias = false, StrokeWidth = this.Width, Style = SKPaintStyle.Stroke}) {
                    c.DrawPath(path, p);
                }
            }

            // int startX = (int) p1.X - this.Width / 2;
            // int startY = (int) p1.Y - this.Height / 2;
            // int endX = startX + this.Width - 1;
            // int endY = startY + this.Height - 1;

            // // Make sure the boundaries stay within the bitmap size
            // startX = Math.Max(0, startX);
            // startY = Math.Max(0, startY);
            // endX = Math.Min(canvas.Width - 1, endX);
            // endY = Math.Min(canvas.Height - 1, endY);


            // // Set pixels within the rectangle to red color
            // for (int y = startY; y <= endY; y++) {
            //     for (int x = startX; x <= endX; x++) {
            //         if (x < 0 || y < 0 || x > canvas.Width || y > canvas.Height)
            //             continue;
            //         canvas.Bitmap.SetPixel(x, y, colour);
            //     }
            // }
        }
    }
}