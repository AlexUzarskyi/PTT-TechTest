using System.Text.RegularExpressions;

namespace PTTTest01.Helpers
{
    public static partial class RegexHelper
    {
        // Regex to match vowels (lowercase and uppercase)
        [GeneratedRegex("[aeiouAEIOU]")]
        public static partial Regex VowelRegex();

        // Regex to match non-alphanumeric characters
        [GeneratedRegex(@"[^a-zA-Z0-9]")]
        public static partial Regex NonAlphanumericRegex();
    }
}