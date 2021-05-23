namespace DigitalMuseums.Infrastructure.Options
{
    public class SmtpClientOptions
    {
        public string Smtp { get; set; }

        public int Port { get; set; }

        public string SenderEmail { get; set; }

        public string SenderPassword { get; set; }

        public string EmailTitle { get; set; }
    }
}
