namespace BeamCalculation.Client.Extensions
{
    /// <summary>
    /// Provides extension methods for IEnumerable.
    /// </summary>
    public static class EnumerableExtensions
    {
        private const int decimalPlaces = 10;

        /// <summary>
        /// Returns the minimum value of the specified properties from a sequence of values. The second property can be nullable. Also deals with floating point problem.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the sequence.</typeparam>
        /// <param name="values">The sequence of values to determine the minimum value of.</param>
        /// <param name="property1">A function to extract the first property value from an element.</param>
        /// <param name="property2">A function to extract the second property value from an element, which can be nullable.</param>
        /// <returns>The minimum value of the specified properties.</returns>
        public static double Min<T>(this IEnumerable<T> values, Func<T, double> property1, Func<T, double?> property2)
        {
            return Math.Round(values.Min(p => Math.Min(property1(p), property2(p) ?? double.MaxValue)), decimalPlaces);
        }

        /// <summary>
        /// Returns the maximum value of the specified properties from a sequence of values. The second property can be nullable. Also deals with floating point problem.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the sequence.</typeparam>
        /// <param name="values">The sequence of values to determine the maximum value of.</param>
        /// <param name="property1">A function to extract the first property value from an element.</param>
        /// <param name="property2">A function to extract the second property value from an element, which can be nullable.</param>
        /// <returns>The maximum value of the specified properties.</returns>
        public static double Max<T>(this IEnumerable<T> values, Func<T, double> property1, Func<T, double?> property2)
        {
            return Math.Round(values.Max(p => Math.Max(property1(p), property2(p) ?? double.MinValue)), decimalPlaces);
        }
    }
}
