using System;

namespace DigitalMuseums.Core.Services.Contracts.Providers
{
    public interface IClock
    {
        DateTime GetNow();
        
        DateTime GetUtcNow();
    }
}