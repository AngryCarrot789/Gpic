using System.Threading.Tasks;
using Gpic.Core;
using Gpic.Core.Views.Dialogs.Progression;

namespace Gpic.Views.Progression {
    [ServiceImplementation(typeof(IProgressionDialogService))]
    public class ProgressionDialogService : IProgressionDialogService {
        public Task ShowIndeterminateAsync(IndeterminateProgressViewModel viewModel) {
            IndeterminateProgressionWindow window = new IndeterminateProgressionWindow();
            viewModel.Window = window;
            window.DataContext = viewModel;
            window.Show();
            return Task.CompletedTask;
        }
    }
}