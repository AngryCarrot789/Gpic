using System.Threading.Tasks;

namespace Gpic.Core.Utils {
    public interface IRenameable {
        Task<bool> RenameAsync();
    }
}