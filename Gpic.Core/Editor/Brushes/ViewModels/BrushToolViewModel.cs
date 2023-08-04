using System;

namespace Gpic.Core.Editor.Brushes.ViewModels {
    public abstract class BrushToolViewModel : BaseViewModel {
        public BrushTool Model { get; }

        protected BrushToolViewModel(BrushTool model) {
            this.Model = model ?? throw new ArgumentNullException(nameof(model));
        }
    }
}