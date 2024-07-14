namespace CbgTaxi24.API.Models
{
    public class Entity
    {
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; set; }

        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public bool SoftDeleted { get; set; }
    }
}
