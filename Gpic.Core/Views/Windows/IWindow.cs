using System.Threading.Tasks;

namespace Gpic.Core.Views.Windows {
    public interface IWindow : IViewBase {
        void CloseWindow();

        Task CloseWindowAsync();

        bool IsOpen { get; }
    }
}