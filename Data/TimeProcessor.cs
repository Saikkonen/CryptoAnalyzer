using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoAnalyzer.Data
{
    internal class TimeProcessor
    {
        public static string DateTimeToUnixTime(DateTime dateTime)
        {
            DateTimeOffset dto = new(dateTime.ToUniversalTime());
            return dto.ToUnixTimeSeconds().ToString();
        }

        public static DateTime UnixToDatetime(double unixTimestamp)
        {
            DateTimeOffset dto = DateTimeOffset.FromUnixTimeMilliseconds((long)unixTimestamp);
            return dto.LocalDateTime;
        }

        public static DateTime GetDateAtMidnight(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
        }
    }
}
