using SkiaSharp;

namespace Gpic.Core.Editor.Tools.ViewModels {
    public class SelectableColourViewModel : ColourViewModel {
        private bool isActive;
        public bool IsActive {
            get => this.isActive;
            set => this.RaisePropertyChanged(ref this.isActive, value);
        }

        public SelectableColourViewModel() {
        }

        public SelectableColourViewModel(bool isActive) {
            this.isActive = isActive;
        }

        public SelectableColourViewModel(SKColor colour) : base(colour) {
        }

        public SelectableColourViewModel(SKColor colour, bool isActive) : base(colour) {
            this.isActive = isActive;
        }
    }
}