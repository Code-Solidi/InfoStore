using System;

namespace ToDos.Code
{
    public static class Extensions
    {
        public static DateTime NoSeconds(this DateTime original)
            => new DateTime(original.Year, original.Month, original.Day, original.Hour, original.Minute, 0);
    }
}