using System.Threading.Tasks;
using Gpic.Core.Shortcuts.Managing;

namespace Gpic.Shortcuts {
    public delegate Task<bool> ShortcutActivateHandler(ShortcutProcessor processor, GroupedShortcut shortcut);
}