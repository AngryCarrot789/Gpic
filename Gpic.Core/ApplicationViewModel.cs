using Gpic.Core.Editor.Utils;
using Gpic.Core.Settings.ViewModels;

namespace Gpic.Core {
    public class ApplicationViewModel : BaseViewModel {
        public ApplicationSettings Settings { get; }

        private EnumFloatToIntMode pointerToPixelMode;

        /// <summary>
        /// The mode which specifies how a floating point value (location of the mouse pointer) should be converted into a pixel
        /// </summary>
        public EnumFloatToIntMode PointerToPixelMode {
            get => this.pointerToPixelMode;
            set => this.RaisePropertyChanged(ref this.pointerToPixelMode, value);
        }

        public ApplicationViewModel() {
            this.Settings = new ApplicationSettings();
        }
    }
}