using System;
using System.Text.RegularExpressions;

namespace Bookmarks.Models.Validators
{
    public static class StringExtensions
    {
        /// <summary>
        /// https://stackoverflow.com/questions/3228984/a-better-way-to-validate-url-in-c-sharp-than-try-catch
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool ValidateUri(this string url)
        {
            var valid = false;
            if (Uri.TryCreate(url, new UriCreationOptions { DangerousDisablePathAndQueryCanonicalization = false }, out var result))
            {
                valid = result.IsWellFormedOriginalString();
            }

            return valid;
        }
    }
}