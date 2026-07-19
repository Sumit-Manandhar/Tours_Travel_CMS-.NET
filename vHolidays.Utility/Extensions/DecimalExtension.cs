namespace vHolidays.Utility.Extensions
{
    public static class DecimalExtension
    {
        public static string FormatAsRating(this decimal value)
        {
            if (value % 1 == 0)
                return ((int)value).ToString();
            else
                return value.ToString("0.0");
        }
    }
}
