using Moq;
using System;
using TaskManager.Application.Interfaces;

namespace TaskManager.Application.Test.Mocks
{
    /// <summary>
    /// Mock object for DateTimeService
    /// </summary>
    public static class DateTimeServiceMock
    {
        public static IDateTimeService GetDateTimeService()
        {
            var dateTimeMock = new Mock<IDateTimeService>();
            dateTimeMock.Setup(m => m.Now)
                .Returns(DateTime.Now);

            return dateTimeMock.Object;
        }
    }
}
