using System.Threading.Tasks;

namespace Gpic.Core.Editor {
    public interface IRenameable {
        Task<bool> RenameAsync();
    }
}