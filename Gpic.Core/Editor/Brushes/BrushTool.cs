using System.Numerics;

namespace Gpic.Core.Editor.Brushes {
    /// <summary>
    /// Base class for a brush
    /// </summary>
    public abstract class BrushTool {
        /// <summary>
        /// Main function for drawing this brush onto the canvas
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="canvas">The canvas to draw into</param>
        /// <param name="point">The location of the mouse cursor</param>
        public abstract void Draw(GpicEditor editor, GpicCanvas canvas, Vector2 point);
    }
}