using System;
using TaskManager.Application.Interfaces;

namespace TaskManager.Infrastucture.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.Now;
    }
}
