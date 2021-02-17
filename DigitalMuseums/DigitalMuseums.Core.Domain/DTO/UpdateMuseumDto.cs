namespace DigitalMuseums.Core.Domain.DTO
{
    public class UpdateMuseumDto
    {
        public int Id { get; set; }

        public string Description { get; set; }
        
        public string Name { get; set; }
        
        public int CityId { get; set; }

        public string Address { get; set; }
        
        public int GenreId { get; set; }

        public MuseumImagesUnit ImagesData { get; set; }
    }
}