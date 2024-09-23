using PTTTest01.DataAccess;
using PTTTest01.Helpers;

namespace PTTTest01.Services
{
    public class AvatarService (IAvatarRepository avatarRepository, IAvatarJsonHelper avatarJsonHelper, ILogger logger) : IAvatarService
    {
        private readonly IAvatarRepository _avatarRepository = avatarRepository;
        private readonly IAvatarJsonHelper _avatarJsonHelper = avatarJsonHelper;
        private readonly ILogger _logger = logger;

        public async Task<string?> GetAvatarUrl(string userIdentifier)
        {
            char lastChar = userIdentifier[^1];

            if (char.IsDigit(lastChar) && "6789".Contains(lastChar))
            {
                var lastDigit = lastChar.ToString();
                string jsonUrl = $"{AvatarConstants.JsonApiBaseUrl}{lastDigit}";

                var imageUrl = await _avatarJsonHelper.FetchAndExtractProperty(jsonUrl, "url");

                if (string.IsNullOrEmpty(imageUrl))
                {
                    _logger.LogError("Image URL is empty after retrieving image URL from JSON URL for user identifier: {userIdentifier}.", userIdentifier);
                }

                return imageUrl;
            }

            if (char.IsDigit(lastChar) && "12345".Contains(lastChar))
            {
                var imageUrl = await _avatarRepository.GetImageUrlById(lastChar);

                if (string.IsNullOrEmpty(imageUrl))
                {
                    _logger.LogError("Image URL is empty after retrieving image URL from JSON URL for user identifier: {userIdentifier}.", userIdentifier);
                }

                return imageUrl;
            }

            if (RegexHelper.VowelRegex().IsMatch(userIdentifier))
            {
                return AvatarConstants.VowelAvatarUrl;
            }

            if (RegexHelper.NonAlphanumericRegex().IsMatch(userIdentifier))
            {
                var random = new Random().Next(AvatarConstants.RandomAvatarSeedMin, AvatarConstants.RandomAvatarSeedMax);
                return AvatarConstants.RandomAvatarImageApiBaseUrlTemplate.Replace("{random}", random.ToString());
            }

            return AvatarConstants.DefaultAvatarUrl;
        }
    }
}