using System.Numerics;

namespace Gpic.Core.Editor.Tools.Brushes {
    /// <summary>
    /// Base class for a brush
    /// </summary>
    public abstract class BrushTool {
        /// <summary>
        /// Main function for drawing this brush onto the canvas
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="canvas">The canvas to draw into</param>
        /// <param name="p1">The starting point</param>
        /// <param name="p2">The ending point</param>
        public abstract void Draw(GpicEditor editor, GpicCanvas canvas, Vector2 p1, Vector2 p2);
    }
}