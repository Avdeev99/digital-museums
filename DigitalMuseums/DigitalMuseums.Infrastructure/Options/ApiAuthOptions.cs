namespace DigitalMuseums.Infrastructure.Options
{
    public class ApiAuthOptions
    {
        public string Secret { get; set; }

        public string Issuer { get; set; }

        public int ExpirationTimeInSeconds { get; set; }
    }
}