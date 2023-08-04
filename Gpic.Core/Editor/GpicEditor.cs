using System.Collections.Generic;
using Gpic.Core.Editor.Brushes;
using SkiaSharp;

namespace Gpic.Core.Editor {
    public class GpicEditor {
        /// <summary>
        /// The primary colour for this editor
        /// </summary>
        public SKColor PrimaryColour;

        /// <summary>
        /// An optional secondary colour
        /// </summary>
        public SKColor SecondaryColour;

        public bool IsToolUsingSecondaryColour;

        /// <summary>
        /// Gets the primary or secondary based on <see cref="IsToolUsingSecondaryColour"/>
        /// </summary>
        public SKColor ActiveToolColour => this.IsToolUsingSecondaryColour ? this.SecondaryColour : this.PrimaryColour;

        /// <summary>
        /// The currently active canvas. May be null
        /// </summary>
        public GpicCanvas MainCanvas;

        /// <summary>
        /// A list of canvases currently associated with this editor
        /// </summary>
        public readonly List<GpicCanvas> Canvasses;

        /// <summary>
        /// This editor's pencil brush. Must not be null
        /// </summary>
        public PencilBrush PencilBrush;

        /// <summary>
        /// This editor's active brush used for drawing pixels (e.g. pencil, paint brush, fill, etc). May be null
        /// </summary>
        public BrushTool ActiveBrush;

        public GpicEditor() {
            this.Canvasses = new List<GpicCanvas>();
            this.PrimaryColour = SKColors.WhiteSmoke;
            this.SecondaryColour = SKColors.Black;
            this.PencilBrush = new PencilBrush();
            this.ActiveBrush = this.PencilBrush;
        }
    }
}