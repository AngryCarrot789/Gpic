using System;
using System.Collections.ObjectModel;
using Gpic.Core.Utils;

namespace Gpic.Core.Exceptions {
    public class ExceptionStackViewModel : BaseViewModel {
        public ObservableCollection<ExceptionViewModel> Exceptions { get; }

        public ExceptionStackViewModel(ExceptionStack stack) {
            this.Exceptions = new ObservableCollection<ExceptionViewModel>();
            foreach (Exception exception in stack.Exceptions) {
                this.Exceptions.Add(new ExceptionViewModel(null, exception, false));
            }
        }
    }
}