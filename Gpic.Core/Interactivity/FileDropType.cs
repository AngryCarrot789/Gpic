using System;

namespace Gpic.Core.Interactivity {
    [Flags]
    public enum FileDropType {
        None,
        Copy,
        Move,
        All = Copy | Move
    }
}