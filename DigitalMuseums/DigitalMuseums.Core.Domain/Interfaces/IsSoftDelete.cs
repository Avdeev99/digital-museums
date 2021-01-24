namespace DigitalMuseums.Core.Domain.Interfaces
{
    public interface IsSoftDelete
    {
        bool IsDeleted { get; set; }
    }
}