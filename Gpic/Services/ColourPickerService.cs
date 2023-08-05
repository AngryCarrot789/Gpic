using System.Windows;
using Gpic.Controls;
using Gpic.Core;
using Gpic.Core.Services;
using Gpic.Services.ColourPickers;
using SkiaSharp;

namespace Gpic.Services {
    [ServiceImplementation(typeof(IColourPicker))]
    public class ColourPickerService : IColourPicker {
        public SKColor? PickARGB(SKColor? def = null) {
            ColourPickerWindow window = new ColourPickerWindow {
                WindowStyle = WindowStyle.None,
                WindowStartupLocation = WindowStartupLocation.Manual,
                Height = 426
            };

            if (def is SKColor colour) {
                window.Colour = colour;
            }

            CursorUtils.POINT pos = CursorUtils.GetCursorPos();
            window.Left = pos.x;
            window.Top = pos.y - 426;

            if (window.ShowDialog() == true) {
                return window.ActiveColour;
            }

            return null;

            // ColorDialog dialog = new ColorDialog() {
            //     AnyColor = true,
            //     SolidColorOnly = false,
            //     AllowFullOpen = true,
            //     FullOpen = true
            // };
            // 
            // if (def is SKColor d) {
            //     dialog.Color = Color.FromArgb(d.Alpha, d.Red, d.Green, d.Blue);
            // }
            // 
            // if (dialog.ShowDialog() == DialogResult.OK) {
            //     Color c = dialog.Color;
            //     return new SKColor(c.R, c.G, c.B, c.A);
            // }
            // 
            // return null;
        }
    }
}