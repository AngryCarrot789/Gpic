using System;
using System.Collections.ObjectModel;
using Gpic.Core.Editor.Brushes;
using Gpic.Core.Editor.Brushes.ViewModels;
using Gpic.Core.Services;
using SkiaSharp;

namespace Gpic.Core.Editor.ViewModels {
    public class EditorViewModel : BaseViewModel {
        private readonly ObservableCollection<CanvasViewModel> canvasses;
        public ReadOnlyObservableCollection<CanvasViewModel> Canvasses { get; }

        private CanvasViewModel mainCanvas;

        /// <summary>
        /// The currently active canvas
        /// </summary>
        public CanvasViewModel MainCanvas {
            get => this.mainCanvas;
            set {
                if (value != null && !this.canvasses.Contains(value))
                    throw new InvalidOperationException("Value must be stored in the canvas list");
                this.RaisePropertyChanged(ref this.mainCanvas, value);
            }
        }

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
                this.RaisePropertyChanged(ref this.activeBrush, value);
                this.IncreaseBrushSize.RaiseCanExecuteChanged();
                this.DecreaseBrushSize.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand IncreaseBrushSize { get; }
        public RelayCommand DecreaseBrushSize { get; }

        public RelayCommand EditPrimaryColourCommand { get; }
        public RelayCommand EditSecondaryColourCommand { get; }

        public GpicEditor Model { get; }

        public EditorViewModel(GpicEditor model) {
            this.Model = model ?? throw new ArgumentNullException(nameof(model));
            this.canvasses = new ObservableCollection<CanvasViewModel>();
            this.Canvasses = new ReadOnlyObservableCollection<CanvasViewModel>(this.canvasses);
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

            this.activeBrush = this.pencilBrush = new PencilBrushViewModel(model.PencilBrush);
            this.mainCanvas = new CanvasViewModel(model.MainCanvas);
            this.mainCanvas.Model.SetSizeAndCreateBitmap(1280, 720);
            this.canvasses.Add(this.mainCanvas);
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