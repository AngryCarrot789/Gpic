using System.Threading.Tasks;

namespace Gpic.Core.Views.Dialogs.Progression {
    public interface IProgressionDialogService {
        Task ShowIndeterminateAsync(IndeterminateProgressViewModel viewModel);
    }
}