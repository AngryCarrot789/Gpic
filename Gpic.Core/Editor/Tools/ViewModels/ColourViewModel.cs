using SkiaSharp;

namespace Gpic.Core.Editor.Tools.ViewModels {
    public class ColourViewModel : BaseViewModel {
        private SKColor colour;
        public SKColor Colour {
            get => this.colour;
            set => this.RaisePropertyChanged(ref this.colour, value);
        }

        public RelayCommand EditColourCommand { get; }

        public ColourViewModel() {
            this.EditColourCommand = new RelayCommand(this.PickColour);
        }

        public ColourViewModel(SKColor colour) : this() {
            this.colour = colour;
        }

        public void PickColour() {
            if (ToolPaletteViewModel.PickColour(this.colour, out SKColor c)) {
                this.Colour = c;
            }
        }
    }
}