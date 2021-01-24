namespace DigitalMuseums.Core.Domain.DTO
{
    public class MuseumDto
    {
        public string Name { get; set; }
        
        public int CityId { get; set; }

        public string Address { get; set; }
        
        public int GenreId { get; set; }

        public MuseumImagesUnit ImagesData { get; set; }
    }
}