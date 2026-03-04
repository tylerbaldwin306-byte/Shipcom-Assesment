using Shipcom_Assesment.Constants;
using Shipcom_Assesment.Services.Abstract;

namespace Shipcom_Assesment.Services
{
    public class TimeService : ITimeService
    {
        public double CalculateTimeAngle(int hours, int minutes)
        {
            if (hours < TimeConstants.MIN_HOURS || hours > TimeConstants.MAX_HOURS)
            {
                throw new ArgumentOutOfRangeException(nameof(hours), $"Hour '{hours}' must be between {TimeConstants.MIN_HOURS} and {TimeConstants.MAX_HOURS}.");
            }

            if (minutes < TimeConstants.MIN_MINUTES || minutes > TimeConstants.MAX_MINUTES)
            {
                throw new ArgumentOutOfRangeException(nameof(minutes), $"Minute '{minutes}' must be between {TimeConstants.MIN_MINUTES} and {TimeConstants.MAX_MINUTES}.");
            }

            // 360 degrees / 60 minutes = 6 degrees per minute
            double minuteAngle = minutes * 6.0;

            // 360 degrees / 12 hours = 30 degrees per hour, plus 0.5 degrees for each minute elapsed
            double hourAngle = (hours % TimeConstants.MAX_HOURS) * 30.0 + minutes * 0.5;

            return hourAngle + minuteAngle;
        }
    }
}
