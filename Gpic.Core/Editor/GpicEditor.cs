using System.Collections.Generic;
using Gpic.Core.Editor.Tools;
using SkiaSharp;

namespace Gpic.Core.Editor {
    public class GpicEditor {
        /// <summary>
        /// A list of canvases currently associated with this editor
        /// </summary>
        public readonly List<GpicCanvas> Canvasses;

        /// <summary>
        /// The currently active canvas (aka the canvas the user has focused in the UI). May be null
        /// </summary>
        public GpicCanvas ActiveCanvas;

        /// <summary>
        /// This editor's current tool palette, which stores all of the available brushes, tools, active colours, etc
        /// </summary>
        public readonly ToolPalette ToolPalette;

        public GpicEditor() {
            this.Canvasses = new List<GpicCanvas>();
            this.ToolPalette = new ToolPalette();
        }
    }
}