using System;

namespace Jukebox
{
    public static class Extensions
    {
        public static bool Contains(this string source, string value, bool caseSensitive)
        {
            return (source ?? "").IndexOf(value ?? "", caseSensitive ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture) >= 0;
        }
    }
}
