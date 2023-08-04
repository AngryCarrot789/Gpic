using System;

namespace Gpic.Core.Editor.Brushes.ViewModels {
    public class PencilBrushViewModel : BrushToolViewModel, IBrushSize {
        public new PencilBrush Model => (PencilBrush) base.Model;

        /// <summary>
        /// Gets or sets the width of the pencil
        /// </summary>
        public int Width {
            get => this.Model.Width;
            set => this.RaisePropertyChanged(ref this.Model.Width, value);
        }

        /// <summary>
        /// Gets or sets the height of the pencil
        /// </summary>
        public int Height {
            get => this.Model.Height;
            set => this.RaisePropertyChanged(ref this.Model.Height, value);
        }

        public PencilBrushViewModel(PencilBrush model) : base(model) {

        }

        public void Increse() {
            // max handles overflow; max(int.MinValue, 1) == 1
            this.Width = Math.Max(this.Width + 1, 1);
            this.Height = Math.Max(this.Height + 1, 1);
        }

        public void Decrease() {
            this.Width = Math.Max(this.Width - 1, 1);
            this.Height = Math.Max(this.Height - 1, 1);
        }
    }
}