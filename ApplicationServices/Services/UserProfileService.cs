using ApplicationServices.Services.DTO;
using AutoMapper;
using Core.DominModels.UserAggregate;
using LinkDev.Wasel.Core.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApplicationServices.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserProfileService(
        IUserProfileRepository userProfileRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
        {
            _userProfileRepository = userProfileRepository ;
            _unitOfWork = unitOfWork ;
            _mapper = mapper;
        }
        public async Task<bool> AddProfile(ProfileDTO profileDTO, CancellationToken cancellationToken)
        {
            try
            {
                var profile = _mapper.Map<UserProfile>(profileDTO);
                await _userProfileRepository.AddAsync(profile, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ProfileDTO> GetProfileById(Guid id, CancellationToken cancellationToken)
        {
            var profile = await _userProfileRepository.GetProfileAsync(p => p.ID == id, cancellationToken);
            return _mapper.Map<ProfileDTO>(profile);
        }

        public async Task<IEnumerable<ProfileDTO>> GetAllProfiles(CancellationToken cancellationToken)
        {
            var profiles = await _userProfileRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<ProfileDTO>>(profiles);
        }

        public async Task<bool> UpdateProfile( ProfileDTO profileDTO, CancellationToken cancellationToken)
        {
            try
            {
                var profile = await _userProfileRepository.GetProfileAsync(p => p.ID == profileDTO.ID, cancellationToken);

                if (profile == null) return false;
                _mapper.Map(profileDTO, profile);
                _userProfileRepository.UpdateAsync(profile);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteProfile(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var profile = await _userProfileRepository.GetProfileAsync(p => p.ID == id, cancellationToken);
                if (profile == null) return false;
                profile.DeleteUser();
                _userProfileRepository.UpdateAsync(profile);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
