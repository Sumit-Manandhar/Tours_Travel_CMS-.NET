namespace Travels.Utility.Extensions
{
    public static class DateOnlyExtension
    {
        public static string ddMMMMyyyyFormat(this DateOnly? value)
        {
            if (value is null)
                return string.Empty;
            return value.Value.ToString("dddd, dd MMMM yyyy");
        }
        public static string ddMMyyyy(this DateOnly? value)
        {
            if (value is null)
                return string.Empty;
            return value.Value.ToString("dd/MM/yyyy");
        }
    }
}
