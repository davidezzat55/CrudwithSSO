using ApplicationServices.Services.DTO;

namespace ApplicationServices.Services
{
    public interface IUserProfileService
    {
        Task<bool> AddProfile(ProfileDTO profileDTO, CancellationToken cancellationToken);
        Task<bool> DeleteProfile(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<ProfileDTO>> GetAllProfiles(CancellationToken cancellationToken);
        Task<ProfileDTO> GetProfileById(Guid id, CancellationToken cancellationToken);
        Task<bool> UpdateProfile( ProfileDTO profileDTO, CancellationToken cancellationToken);
    }
}