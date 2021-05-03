using System;
using DigitalMuseums.Core.Services.Contracts.Providers;

namespace DigitalMuseums.Core.Services.Providers
{
    public class Clock : IClock
    {
        public DateTime GetNow()
        {
            return DateTime.Now;
        }

        public DateTime GetUtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}