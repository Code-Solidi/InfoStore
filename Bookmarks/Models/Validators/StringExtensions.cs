using System;
using System.Text.RegularExpressions;

namespace Bookmarks.Models.Validators
{
    public static class StringExtensions
    {
        public static bool ValidateUri(this string url)
        {
            //const string pattern = "([\\w-]+\\.)+[\\w-]+[.com|.in|.org]+(\\[\\?%&=]*)?";
            const string pattern = @"^http(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_]*)?$";
            var mended = !string.IsNullOrEmpty(url);
            while (mended)
            {
                mended = false;
                try
                {
                    var address = new Uri(url);
                    //var result = Regex.IsMatch(address.DnsSafeHost, pattern);
                    //return result;
                    return Regex.IsMatch(url, pattern);
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