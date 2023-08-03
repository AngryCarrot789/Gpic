using Gpic.Core.Settings.ViewModels;

namespace Gpic.Core {
    public class ApplicationViewModel : BaseViewModel {
        public ApplicationSettings Settings { get; }

        public ApplicationViewModel() {
            this.Settings = new ApplicationSettings();
        }
    }
}