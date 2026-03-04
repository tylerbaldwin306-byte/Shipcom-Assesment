using Microsoft.AspNetCore.Mvc;
using Shipcom_Assesment.Constants;
using Shipcom_Assesment.Services.Abstract;

namespace Shipcom_Assesment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimeController(ITimeService timeService) : ControllerBase
    {
        private readonly ITimeService _timeService = timeService;

        /// <summary>
        /// Calculate the total angle of the clock hands for a given time, or hour and minute.
        /// </summary>
        /// <param name="timeAngleRequest"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CalculateTimeAngle")]
        public ActionResult<double> CaulculateTimeAngle(
            [FromQuery] string? time, 
            [FromQuery] int? hour, 
            [FromQuery] int? minute)
        {
            try
            {
                var (parsedHour, parsedMinute) = ParseTime(time, hour, minute);

                double timeAngle = _timeService.CalculateTimeAngle(parsedHour, parsedMinute);

                return Ok(timeAngle);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return Problem(
                    detail: "Unexpected error occurred during time angle calculation.",
                    statusCode: 500);
            }
        }

        private static (int hour, int minute) ParseTime(string? time, int? hour, int? minute)
        {
            if (time != null && (hour != null ||  minute != null))
            {
                throw new ArgumentException($"Cannot provide both {nameof(time)} AND {nameof(hour)} or {nameof(minute)}");
            }

            if (time != null)
            {
                if (!TimeOnly.TryParseExact(time, TimeConstants.TIME_FORMAT, out TimeOnly parsedTime))
                {
                    throw new ArgumentException(nameof(time), $"Time '{time}' could not be parsed to format '{TimeConstants.TIME_FORMAT}'");
                }

                return (parsedTime.Hour, parsedTime.Minute);
            }

            if (hour == null || minute == null)
            {
                throw new ArgumentException("If time is not provided, BOTH hour and minute must be provided.");
            }

            return (hour.Value, minute.Value);
        }
    }
}
