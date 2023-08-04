using System;

namespace Gpic.Core.Editor.Tools.Brushes.ViewModels {
    public abstract class BrushToolViewModel : BaseViewModel {
        public BrushTool Model { get; }

        private bool isActive;
        public bool IsActive {
            get => this.isActive;
            set => this.RaisePropertyChanged(ref this.isActive, value);
        }

        protected BrushToolViewModel(BrushTool model) {
            this.Model = model ?? throw new ArgumentNullException(nameof(model));
        }
    }
}