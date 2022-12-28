using System;
using System.Text.RegularExpressions;

namespace Bookmarks.Models.Validators
{
    public static class StringExtensions
    {
        public static bool ValidateUri(this string url)
        {
            const string pattern = "([\\w-]+\\.)+[\\w-]+[.com|.in|.org]+(\\[\\?%&=]*)?";

            var mended = !string.IsNullOrEmpty(url);
            while (mended)
            {
                mended = false;
                try
                {
                    var address = new Uri(url);
                    return Regex.IsMatch(address.DnsSafeHost, pattern);
                }
                catch (Exception)
                {
                    if (!url.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                    {
                        url = "http://" + url;
                        mended = true;
                    }
                }
            }

            return false;
        }
    }
}