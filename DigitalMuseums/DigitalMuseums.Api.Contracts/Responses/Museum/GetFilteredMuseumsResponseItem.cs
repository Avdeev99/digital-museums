namespace DigitalMuseums.Api.Contracts.Responses.Museum
{
    public class GetFilteredMuseumsResponseItem
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string GenreName { get; set; }

        public string ImagePath { get; set; }
    }
}