using System.Collections.Generic;

namespace Gpic.Core.AdvancedContextService {
    public interface IContextProvider {
        void GetContext(List<IContextEntry> list);
    }
}