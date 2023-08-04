using Gpic.Core;

namespace Gpic.Editor.Canvases {
    public class CanvasViewModel : BaseViewModel {
        private int width;
        public int Width {
            get => this.width;
            set => this.RaisePropertyChanged(ref this.width, value);
        }

        private int height;
        public int Height {
            get => this.height;
            set => this.RaisePropertyChanged(ref this.height, value);
        }

        public CanvasViewModel() {
            this.width = 1280;
            this.height = 720;
        }
    }
}