using System.Windows;
using System.Windows.Controls;

namespace Gpic.PropertyEditing {
    public class PropertyEditorItemsControl : ItemsControl {
        public PropertyEditorItemsControl() {
        }

        protected override bool IsItemItsOwnContainerOverride(object item) {
            return item is PropertyEditorItemContainer;
        }

        protected override DependencyObject GetContainerForItemOverride() {
            return new PropertyEditorItemContainer();
        }
    }
}