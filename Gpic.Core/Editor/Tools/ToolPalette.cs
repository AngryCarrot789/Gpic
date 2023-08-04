using Gpic.Core.Editor.Tools.Brushes;
using SkiaSharp;

namespace Gpic.Core.Editor.Tools {
    public class ToolPalette {
        /// <summary>
        /// The 9 selectable colours
        /// </summary>
        public readonly SKColor[] Colours;

        /// <summary>
        /// The primary colour
        /// </summary>
        public SKColor PrimaryColour => this.Colours[0];

        /// <summary>
        /// An optional secondary colour
        /// </summary>
        public SKColor SecondaryColour => this.Colours[1];

        public int ActiveColourIndex;

        public PencilBrush PencilBrush;
        public BrushTool ActiveBrush;

        /// <summary>
        /// Gets the primary or secondary based on <see cref="IsToolUsingSecondaryColour"/>
        /// </summary>
        public SKColor ActiveToolColour => this.Colours[this.ActiveColourIndex];

        public ToolPalette() {
            this.PencilBrush = new PencilBrush();
            this.ActiveBrush = this.PencilBrush;
            this.Colours = new SKColor[9] {
                SKColors.Aqua, SKColors.DarkCyan, SKColors.Red,
                SKColors.Green, SKColors.Blue, SKColors.Orange,
                SKColors.Purple, SKColors.WhiteSmoke, SKColors.Black
            };
        }
    }
}