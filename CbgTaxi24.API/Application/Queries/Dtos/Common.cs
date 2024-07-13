
#nullable disable

namespace CbgTaxi24.API.Application.Queries.Dtos
{
    public record BaseDto
    {
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
        public string CreatedBy { get; set; } = default!;
        public string UpdatedBy { get; set; }
    }

    public record ErrorDto
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
