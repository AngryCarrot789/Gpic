using System.Reflection;

namespace Gpic.Core.Settings.ViewModels {
    public class ApplicationSettings : BaseViewModel {
        public AppSettings Settings { get; private set; }

        public ApplicationSettings() {
            this.Settings = AppSettings.Defaults();
        }

        public void SetSettings(AppSettings settings) {
            this.Settings = settings ?? AppSettings.Defaults();

            // lazy lol
            // includes settings property, even though it shouldn't be bound to
            foreach (PropertyInfo info in this.GetType().GetProperties()) {
                this.RaisePropertyChanged(info.Name);
            }
        }
    }
}