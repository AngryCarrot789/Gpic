using System;

namespace Gpic.Core.Utils {
    public interface IRealDisposable : IDisposable {
        bool IsDisposed { get; }

        void Dispose(bool isDisposing);
    }
}