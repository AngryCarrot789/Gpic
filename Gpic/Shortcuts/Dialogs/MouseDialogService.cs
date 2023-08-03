using Gpic.Core;
using Gpic.Core.Shortcuts.Dialogs;
using Gpic.Core.Shortcuts.Inputs;

namespace Gpic.Shortcuts.Dialogs {
    [ServiceImplementation(typeof(IMouseDialogService))]
    public class MouseDialogService : IMouseDialogService {
        public MouseStroke? ShowGetMouseStrokeDialog() {
            MouseStrokeInputWindow window = new MouseStrokeInputWindow();
            if (window.ShowDialog() != true || window.Stroke.Equals(default)) {
                return null;
            }
            else {
                return window.Stroke;
            }
        }
    }
}