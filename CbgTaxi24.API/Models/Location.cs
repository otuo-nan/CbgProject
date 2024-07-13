using System.ComponentModel.DataAnnotations.Schema;

namespace CbgTaxi24.API.Models
{
    [Table("Locations")]
    public class Location
    {
        public Guid LocationId { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public required string Region { get; set; }
        public string? Name { get; set; }
    }
}
