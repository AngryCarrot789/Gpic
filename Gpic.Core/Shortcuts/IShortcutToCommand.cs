using System.Windows.Input;

namespace Gpic.Core.Shortcuts {
    public interface IShortcutToCommand {
        ICommand GetCommandForShortcut(string shortcutId);
    }
}