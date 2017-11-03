using System;

namespace OcenUczelnie.Infrastructure.Extensions
{
    public static class DateTimeExtensions
    {
        public static long ToTimestamp(this DateTime dateTime)
        {
            return (long) (dateTime.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
        }
    }
}
  