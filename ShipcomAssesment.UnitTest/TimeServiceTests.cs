using Shipcom_Assesment.Services;
using Shipcom_Assesment.Services.Abstract;

namespace ShipcomAssesment.UnitTest
{
    public class TimeServiceTests
    {
        private readonly ITimeService _timeService;
        public TimeServiceTests()
        {
            _timeService = new TimeService();
        }

        [Theory]
        [InlineData(3,0,90)]        // Hour alone cases
        [InlineData(12,0,0)]            
        [InlineData(11,0,330)]
        [InlineData(3,15,187.5)]    // Minute cases, adds percentage of hour
        [InlineData(1,30,225)]
        [InlineData(12,15,97.5)]
        [InlineData(11,59,713.5)]   // Max time case
        public void CalculateTimeAngle_ValidInputs_ReturnsCorrectResult(int hour, int minute, double expectedTotal)
        {
            // Arrange

            // Nothing to be arranged

            // Act

            var total = _timeService.CalculateTimeAngle(hour, minute);

            // Assert

            Assert.Equal(expectedTotal, total);
        }

        [Theory]
        [InlineData(-1, 0)]   // Negative hour
        [InlineData(13, 0)]   // Hour out of range
        [InlineData(0, -1)]   // Negative minute
        [InlineData(0, 60)]   // Minute out of range
        public void CalculateAngles_InvalidInput_ThrowsArgumentOutOfRangeException(int hour, int minute)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _timeService.CalculateTimeAngle(hour, minute));
        }
    }
}
