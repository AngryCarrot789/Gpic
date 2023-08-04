using System.Drawing;
using System.Windows.Forms;
using Gpic.Core;
using Gpic.Core.Services;
using SkiaSharp;

namespace Gpic.Services {
    [ServiceImplementation(typeof(IColourPicker))]
    public class ColourPickerService : IColourPicker {
        public SKColor? PickARGB(SKColor? def = null) {
            ColorDialog dialog = new ColorDialog() {
                AnyColor = true,
                SolidColorOnly = false,
                AllowFullOpen = true,
                FullOpen = true
            };

            if (def is SKColor d) {
                dialog.Color = Color.FromArgb(d.Alpha, d.Red, d.Green, d.Blue);
            }

            if (dialog.ShowDialog() == DialogResult.OK) {
                Color c = dialog.Color;
                return new SKColor(c.R, c.G, c.B, c.A);
            }

            return null;
        }

    }
}