using System;

namespace Gpic.Utils {
    public static class Validate {
        public static void ValidateNotNull(object value, string param, string msg) {
            if (value == null) {
                throw new ArgumentNullException(param, msg);
            }
        }
    }
}