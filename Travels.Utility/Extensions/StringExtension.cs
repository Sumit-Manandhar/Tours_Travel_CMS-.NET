namespace Travels.Utility.Extensions
{
    public static class StringExtension
    {
        public static string TruncateWithLink(this string text, int maxLength, string link)
        {
            if (text.Length <= maxLength)
            {
                return text;
            }
            return $"{text.Substring(0, maxLength)}... <a href='{link}'>See more</a>";
        }
    }
}
