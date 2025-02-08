using ApplicationServices.Services;
using ApplicationServices.Services.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;

        public UserController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService ?? throw new ArgumentNullException(nameof(userProfileService));
        }

        [HttpPost]
        public async Task<IActionResult> AddProfile([FromBody] ProfileDTO profileDTO, CancellationToken cancellationToken)
        {
            try
            {
                if (profileDTO == null)
                    return BadRequest("Profile data is required.");

                var result = await _userProfileService.AddProfile(profileDTO, cancellationToken);
                if (!result)
                    return StatusCode(500, "An error occurred while adding the profile.");

                return CreatedAtAction(nameof(GetProfileById), new { id = profileDTO.ID }, profileDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProfileById(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var profile = await _userProfileService.GetProfileById(id, cancellationToken);
                if (profile == null)
                    return NotFound($"Profile with ID {id} not found.");

                return Ok(profile);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProfiles(CancellationToken cancellationToken)
        {
            try
            {
                var profiles = await _userProfileService.GetAllProfiles(cancellationToken);
                return Ok(profiles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut ]
        public async Task<IActionResult> UpdateProfile( [FromBody] ProfileDTO profileDTO, CancellationToken cancellationToken)
        {
            try
            {
                if (profileDTO == null)
                    return BadRequest("Profile data is required.");

                var result = await _userProfileService.UpdateProfile( profileDTO, cancellationToken);
                if (!result)
                    return NotFound($"Profile with ID {profileDTO.ID} not found or update failed.");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteProfile(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userProfileService.DeleteProfile(id, cancellationToken);
                if (!result)
                    return NotFound($"Profile with ID {id} not found or delete failed.");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

}
