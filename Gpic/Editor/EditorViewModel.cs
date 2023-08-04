using Gpic.Core;
using Gpic.Editor.Canvases;

namespace Gpic.Editor {
    public class EditorViewModel : BaseViewModel {
        private CanvasViewModel canvas;
        public CanvasViewModel Canvas {
            get => this.canvas;
            set => this.RaisePropertyChanged(ref this.canvas, value);
        }

        public EditorViewModel() {
            this.canvas = new CanvasViewModel();
        }
    }
}