using System.Threading.Tasks;

namespace DigitalMuseums.Core.Services.Contracts
{
    public interface IEmailService
    {
        public void Send(string mailTo, string mailSubject, string body);
    }
}
