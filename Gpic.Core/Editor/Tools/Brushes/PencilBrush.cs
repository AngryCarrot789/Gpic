using System;
using System.Numerics;
using SkiaSharp;

namespace Gpic.Core.Editor.Tools.Brushes {
    public class PencilBrush : BrushTool {
        public int Width, Height;

        public PencilBrush() {
            this.Width = this.Height = 1;
        }

        public override void Draw(GpicEditor editor, GpicCanvas canvas, Vector2 point) {
            int startX = (int) point.X - this.Width / 2;
            int startY = (int) point.Y - this.Height / 2;
            int endX = startX + this.Width - 1;
            int endY = startY + this.Height - 1;

            // Make sure the boundaries stay within the bitmap size
            startX = Math.Max(0, startX);
            startY = Math.Max(0, startY);
            endX = Math.Min(canvas.Width - 1, endX);
            endY = Math.Min(canvas.Height - 1, endY);

            SKColor colour = editor.ToolPalette.ActiveToolColour;

            // Set pixels within the rectangle to red color
            for (int y = startY; y <= endY; y++) {
                for (int x = startX; x <= endX; x++) {
                    if (x < 0 || y < 0 || x > canvas.Width || y > canvas.Height)
                        continue;
                    canvas.Bitmap.SetPixel(x, y, colour);
                }
            }
        }
    }
}