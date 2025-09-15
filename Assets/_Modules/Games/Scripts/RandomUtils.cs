using System;
using System.Collections.Generic;

namespace Mimi.Math.Utils
{
    /// <summary>
    ///   Utility methods which have to do with randomness.
    /// </summary>
    public static class RandomUtils
    {
        /// <summary>
        ///   Random number generator.
        /// </summary>
        private static Random random;

        /// <summary>
        ///   Random number generator singleton.
        /// </summary>
        public static Random Random
        {
            get { return random ?? (random = new Random()); }
        }

        /// <summary>
        ///   Generates a random sign.
        /// </summary>
        /// <returns>-1 or 1.</returns>
        public static int RandomSign()
        {
            return Random.NextDouble() < .5 ? 1 : -1;
        }

        /// <summary>
        ///   Returns a random double number between the specified minimum and maximum (exclusive).
        /// </summary>
        /// <param name="min">Minimum value.</param>
        /// <param name="max">Maximum value (exclusive).</param>
        /// <returns>Random double number between the specified minimum and maximum (exclusive).</returns>
        public static double RangeDouble(double min, double max)
        {
            if (min > max)
            {
                throw new ArgumentException(
                    $"Minium value ({min}) has to be less or equal maximum value ({max})", nameof(min));
            }

            double range = max - min;
            return min + Random.NextDouble() * range;
        }

        public static bool RandomBoolean(float trueChance = 0.5f)
        {
            return RangeDouble(0f, 1f) <= trueChance;
        }
    }
}