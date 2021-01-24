using System.IO;

namespace DigitalMuseums.Core.Domain.DTO
{
    public class ImageData
    {
        public Stream Stream { get; set; }
        
        public string FileName { get; set; }
    }
}