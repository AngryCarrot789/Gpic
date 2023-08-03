namespace Gpic.Core.PropertyEditing {
    public class GpicPropertyEditorRegistry : PropertyEditorRegistry {
        public static GpicPropertyEditorRegistry Instance { get; } = new GpicPropertyEditorRegistry();

        private GpicPropertyEditorRegistry() {
        }
    }
}