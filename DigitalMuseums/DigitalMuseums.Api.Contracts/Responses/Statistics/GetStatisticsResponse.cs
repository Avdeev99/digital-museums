namespace DigitalMuseums.Api.Contracts.Responses.Statistics
{
    public class GetStatisticsResponse
    {
        public int UsersCount { get; set; }
        
        public int ExhibitsCount { get; set; }
        
        public int OrdersCount { get; set; }
    }
}