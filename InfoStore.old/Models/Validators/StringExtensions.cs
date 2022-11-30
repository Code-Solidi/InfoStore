using System;
using System.Text.RegularExpressions;

namespace InfoStore.Models.Validators
{
    public static class StringExtensions
    {
        public static bool ValidateUri(this string url)
        {
            //const string pattern = "(http|http(s)?://)?([\\w-]+\\.)+[\\w-]+[.com|.in|.org]+(\\[\\?%&=]*)?";
            const string pattern = "([\\w-]+\\.)+[\\w-]+[.com|.in|.org]+(\\[\\?%&=]*)?";

            //var result = Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out var parsed); -- does not work when schema is missing

            var mended = true;
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