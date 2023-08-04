using Gpic.Core.Editor.Tools.Brushes;
using SkiaSharp;

namespace Gpic.Core.Editor.Tools {
    public class ToolPalette {
        /// <summary>
        /// The primary colour
        /// </summary>
        public SKColor PrimaryColour;

        /// <summary>
        /// An optional secondary colour
        /// </summary>
        public SKColor SecondaryColour;

        public bool IsToolUsingSecondaryColour;

        public PencilBrush PencilBrush;
        public BrushTool ActiveBrush;

        /// <summary>
        /// Gets the primary or secondary based on <see cref="IsToolUsingSecondaryColour"/>
        /// </summary>
        public SKColor ActiveToolColour => this.IsToolUsingSecondaryColour ? this.SecondaryColour : this.PrimaryColour;

        public ToolPalette() {
            this.PencilBrush = new PencilBrush();
            this.ActiveBrush = this.PencilBrush;

            this.PrimaryColour = SKColors.Aqua;
            this.SecondaryColour = SKColors.DarkCyan;
        }
    }
}