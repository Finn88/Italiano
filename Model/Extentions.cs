using System;

namespace Model
{
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime date, DayOfWeek firstDayOfWeek)
        {
            var diff = date.DayOfWeek - firstDayOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }

            return date.AddDays(-1*diff).Date;
        }

        public static DateTime EndOfWeek(this DateTime date, DayOfWeek firstDayOfWeek)
        {
            return date.StartOfWeek(firstDayOfWeek).AddDays(6).EndOfDay();
        }

        public static DateTime EndOfDay(this DateTime date)
        {
            return (date.Date + new TimeSpan(23, 59, 59));
        }

    }

    public static class IntExtentions
    {

        public static string DayOfWeek(this int day)
        {
            switch (day)
            {
                case 1:
                    return "Пн";
                case 2:
                    return "Вт";
                case 3:
                    return "Ср";
                case 4:
                    return "Чт";
                case 5:
                    return "Пт";
                case 6:
                    return "Сб";
                default:
                    return "Вс";
            }
        }
    }
}
