namespace CbgTaxi24.API.Utility
{
    public class TripPriceCalculator
    {
        /// <summary>
        /// distance, price
        /// </summary>
        static readonly Dictionary<double, float> priceTable = new() {
            { 1, 40.0f},
            { 2, 45.0f },
            { 3, 50.0f },
            { 4, 55.0f },
            { 5, 60.0f },
            { 6, 65.0f },
            { 7, 70.0f },
            { 8, 75.0f },
            { 9, 80.0f },
            { 8, 85.0f },
        };

        public static float GetPrice(double distance)
        {
            var d = double.Ceiling(distance);

            if (priceTable.TryGetValue(d, out float value))
                return value;

            return 100;
        }
    }
}
