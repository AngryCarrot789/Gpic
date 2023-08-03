using System.Threading.Tasks;
using Gpic.Core.Shortcuts.Managing;

namespace Gpic.Core.Shortcuts {
    public interface IShortcutHandler {
        Task<bool> OnShortcutActivated(ShortcutProcessor processor, GroupedShortcut shortcut);
    }
}