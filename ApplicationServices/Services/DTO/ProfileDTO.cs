using Core.DominModels.UserAggregate;

namespace ApplicationServices.Services.DTO
{
    public class ProfileDTO
    {
        public Guid ID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public Gender? Gender { get; set; }
        public string? Address { get; set; }

    }
}
