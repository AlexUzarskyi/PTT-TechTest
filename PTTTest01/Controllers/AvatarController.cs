using Microsoft.AspNetCore.Mvc;
using PTTTest01.Helpers;
using PTTTest01.Services;

namespace PTTTest01.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AvatarController(ILogger<AvatarController> logger, IAvatarService avatarService) : ControllerBase
    {
        private readonly ILogger<AvatarController> _logger = logger;
        private readonly IAvatarService _avatarService = avatarService;

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? userIdentifier)
        {
            var avatarUrl = await _avatarService.GetAvatarUrl(userIdentifier);

            if (string.IsNullOrEmpty(avatarUrl))
            {
                _logger.LogError("Image URL not returned from avatar service for user identifier: {userIdentifier}.", userIdentifier);
                return StatusCode(500, new
                {
                    status = AvatarConstants.InternalServerErrorCode,
                    message = AvatarConstants.InternalServerErrorMessage
                });
            }

            return Ok(new { url = avatarUrl });
        }
    }
}
