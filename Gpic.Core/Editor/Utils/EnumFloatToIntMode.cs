using System;

namespace Gpic.Core.Editor.Utils {
    /// <summary>
    /// An enum for specifying how a float should be converted into an int
    /// </summary>
    public enum EnumFloatToIntMode {
        /// <summary>
        /// Directly cast the float to an int
        /// </summary>
        Cast,
        /// <summary>
        /// Round the float to an int
        /// </summary>
        Round,
        /// <summary>
        /// Floor the float to the nearest int
        /// </summary>
        Floor,
        /// <summary>
        /// Ceil the float to the nearest int
        /// </summary>
        Ceil
    }

    public static class EnumFloatToIntModeHelper {
        public static int FloatToInt32(this EnumFloatToIntMode mode, float value) => mode.FloatToInt32((double) value);

        public static int FloatToInt32(this EnumFloatToIntMode mode, double value) {
            switch (mode) {
                case EnumFloatToIntMode.Cast: return (int) value;
                case EnumFloatToIntMode.Round: return (int) Math.Round(value);
                case EnumFloatToIntMode.Floor: return (int) Math.Floor(value);
                case EnumFloatToIntMode.Ceil: return (int) Math.Ceiling(value);
                default: throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }
    }
}