using System;

namespace Jukebox
{
    public static class Extensions
    {
        public static bool Contains(this string source, string value, bool caseSensitive)
        {
            return (source ?? "").IndexOf(value ?? "", caseSensitive ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture) >= 0;
        }

        private static System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();

        public static string ToASCII(this string source)
        {
            if (source == null)
            {
                return source;
            }
            return ascii.GetString(ascii.GetBytes(source));
        }
    }
}
