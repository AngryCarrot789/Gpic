using System;
using System.Collections.ObjectModel;
using Gpic.Core.Editor.Tools.Brushes.ViewModels;
using Gpic.Core.Services;
using SkiaSharp;

namespace Gpic.Core.Editor.Tools.ViewModels {
    public class ToolPaletteViewModel : BaseViewModel {
        public ToolPalette Model { get; }

        public ReadOnlyObservableCollection<SelectableColourViewModel> Colours { get; }

        public SKColor PrimarySKColour => this.Model.PrimaryColour;
        public SKColor SecondarySKColour => this.Model.SecondaryColour;

        private SelectableColourViewModel activeColour;
        public SelectableColourViewModel ActiveColour {
            get => this.activeColour ?? this.Colours[0];
            private set => this.RaisePropertyChanged(ref this.activeColour, value);
        }

        public int ActiveIndex {
            get => this.Model.ActiveColourIndex;
            set {
                this.ActiveColour = this.Colours[value];
                this.ActiveColour.IsActive = true;
                this.RaisePropertyChanged(ref this.Model.ActiveColourIndex, value);
            }
        }

        private PencilBrushViewModel pencilBrush;
        public PencilBrushViewModel PencilBrush {
            get => this.pencilBrush;
            set => this.RaisePropertyChanged(ref this.pencilBrush, value);
        }

        private BrushToolViewModel activeBrush;
        public BrushToolViewModel ActiveBrush {
            get => this.activeBrush;
            set {
                if (this.activeBrush != null) {
                    this.activeBrush.IsActive = false;
                }

                if (value != null) {
                    value.IsActive = true;
                }

                this.activeBrush = value;
                this.RaisePropertyChanged();
                this.IncreaseBrushSize.RaiseCanExecuteChanged();
                this.DecreaseBrushSize.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand IncreaseBrushSize { get; }
        public RelayCommand DecreaseBrushSize { get; }

        public RelayCommand<string> ActivateColourCommand { get; }
        public RelayCommand<string> EditColourCommand { get; }

        public ToolPaletteViewModel(ToolPalette model) {
            this.Model = model ?? throw new ArgumentNullException(nameof(model));
            ObservableCollection<SelectableColourViewModel> colours = new ObservableCollection<SelectableColourViewModel>() {
                new SelectableColourViewModel(model.Colours[0]), new SelectableColourViewModel(model.Colours[1]), new SelectableColourViewModel(model.Colours[2]),
                new SelectableColourViewModel(model.Colours[3]), new SelectableColourViewModel(model.Colours[4]), new SelectableColourViewModel(model.Colours[5]),
                new SelectableColourViewModel(model.Colours[6]), new SelectableColourViewModel(model.Colours[7]), new SelectableColourViewModel(model.Colours[8])
            };

            this.Colours = new ReadOnlyObservableCollection<SelectableColourViewModel>(colours);

            this.IncreaseBrushSize = new RelayCommand(() => ((IBrushSize) this.ActiveBrush).Increse(), () => this.ActiveBrush is IBrushSize);
            this.DecreaseBrushSize = new RelayCommand(() => ((IBrushSize) this.ActiveBrush).Decrease(), () => this.ActiveBrush is IBrushSize);
            this.ActiveBrush = this.pencilBrush = new PencilBrushViewModel(model.PencilBrush);

            this.EditColourCommand = new RelayCommand<string>(x => {
                this.Colours[int.Parse(x) - 1].PickColour();
            });

            this.ActivateColourCommand = new RelayCommand<string>(x => {
                this.SetActiveColour(int.Parse(x) - 1);
            });
        }

        public void SetActiveColour(int index) {
            if (index < 0 || index > 8) {
                throw new IndexOutOfRangeException();
            }

            foreach (SelectableColourViewModel colour in this.Colours) {
                colour.IsActive = false;
            }

            this.ActiveIndex = index;
        }

        public static bool PickColour(SKColor? input, out SKColor output) {
            IColourPicker picker = IoC.Provide<IColourPicker>();
            if (picker.PickARGB(input) is SKColor colour) {
                output = colour;
                return true;
            }

            output = default;
            return false;
        }
    }
}