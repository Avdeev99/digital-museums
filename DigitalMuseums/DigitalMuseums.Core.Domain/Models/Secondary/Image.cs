using DigitalMuseums.Core.Domain.Models.Domain;

namespace DigitalMuseums.Core.Domain.Models.Secondary
{
    public class Image : BaseEntity
    {
        public string Path { get; set; }

        public int? MuseumId { get; set; }
        
        public Museum Museum { get; set; }
        
        public int? ExhibitId { get; set; }
        
        public Exhibit Exhibit { get; set; }
        
        public int? ExhibitionId { get; set; }
        
        public Exhibition Exhibition { get; set; }
        
        public int? SouvenirId { get; set; }
        
        public Souvenir Souvenir { get; set; }
    }
}