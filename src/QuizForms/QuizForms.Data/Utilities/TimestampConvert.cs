using System;

namespace QuizForms.Data.Utilities
{
    public static class TimestampConvert
    {
        public static long ConvertToTimestamp(DateTime value)
        {
            long epoch = (value.Ticks - 621355968000000000) / 10000000;
            return epoch;
        }
    }
}
