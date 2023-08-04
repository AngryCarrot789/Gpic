using System;
using Gpic.Core.Editor.Tools.Brushes.ViewModels;
using Gpic.Core.Services;
using SkiaSharp;

namespace Gpic.Core.Editor.Tools.ViewModels {
    public class ToolPaletteViewModel : BaseViewModel {
        public ToolPalette Model { get; }

        public SKColor PrimaryColour {
            get => this.Model.PrimaryColour;
            set => this.RaisePropertyChanged(ref this.Model.PrimaryColour, value);
        }

        public SKColor SecondaryColour {
            get => this.Model.SecondaryColour;
            set => this.RaisePropertyChanged(ref this.Model.SecondaryColour, value);
        }

        private PencilBrushViewModel pencilBrush;
        public PencilBrushViewModel PencilBrush {
            get => this.pencilBrush;
            set => this.RaisePropertyChanged(ref this.pencilBrush, value);
        }

        public bool IsToolUsingSecondaryColour {
            get => this.Model.IsToolUsingSecondaryColour;
            set => this.RaisePropertyChanged(ref this.Model.IsToolUsingSecondaryColour, value);
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

        public RelayCommand EditPrimaryColourCommand { get; }
        public RelayCommand EditSecondaryColourCommand { get; }

        public ToolPaletteViewModel(ToolPalette model) {
            this.Model = model ?? throw new ArgumentNullException(nameof(model));
            this.IncreaseBrushSize = new RelayCommand(() => ((IBrushSize) this.ActiveBrush).Increse(), () => this.ActiveBrush is IBrushSize);
            this.DecreaseBrushSize = new RelayCommand(() => ((IBrushSize) this.ActiveBrush).Decrease(), () => this.ActiveBrush is IBrushSize);

            this.EditPrimaryColourCommand = new RelayCommand(() => {
                if (PickColour(this.PrimaryColour, out SKColor primary))
                    this.PrimaryColour = primary;
            });

            this.EditSecondaryColourCommand = new RelayCommand(() => {
                if (PickColour(this.SecondaryColour, out SKColor primary))
                    this.SecondaryColour = primary;
            });

            this.ActiveBrush = this.pencilBrush = new PencilBrushViewModel(model.PencilBrush);
        }

        private static bool PickColour(SKColor? input, out SKColor output) {
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