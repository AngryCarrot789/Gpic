using SkiaSharp;

namespace Gpic.Core {
    public interface ICanvasView {
        /// <summary>
        /// Attempts to begin a render phase for this canvas
        /// </summary>
        /// <param name="surface">The surface to draw into</param>
        /// <returns>True if the canvas was drawable into (width and height were non-zero, Skia is available, etc)</returns>
        bool BeginCanvasRender(out SKSurface surface);

        /// <summary>
        /// Closes the last render phases, passing the canvas surface data to WPF
        /// </summary>
        void EndCanvasRender();

        /// <summary>
        /// Automatically invalidates this canvas' render, drawing the current state of the canvas
        /// </summary>
        void InvalidateRender();
    }
}