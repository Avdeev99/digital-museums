using System.Collections.Generic;
using System.Security.Claims;

namespace DigitalMuseums.Auth.Factories.Interfaces
{
    public interface ITokenFactory
    {
        string Create(List<Claim> claims);
    }
}