using System.ComponentModel;

namespace Gpic.Core.PropertyEditing {
    public interface IPropertyEditReceiver : INotifyPropertyChanged {
        void OnExternalPropertyModified(BasePropertyEditorViewModel handler, string property);
    }
}