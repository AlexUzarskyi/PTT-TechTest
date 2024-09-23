namespace PTTTest01.Helpers
{
    public static class AvatarConstants
    {
        public const string DefaultAvatarUrl = "https://api.dicebear.com/8.x/pixel-art/png?seed=default&size=150";
        public const string VowelAvatarUrl = "https://api.dicebear.com/8.x/pixel-art/png?seed=vowel&size=150";

        public const int RandomAvatarSeedMin = 1;
        public const int RandomAvatarSeedMax = 6;
        public const string RandomAvatarImageApiBaseUrlTemplate = "https://api.dicebear.com/8.x/pixel-art/png?seed={random}&size=150";

        public const string JsonApiBaseUrl = "https://my-json-server.typicode.com/ck-pacificdev/tech-test/images/";

        public const string BadRequestMessageUserIDNotProvided = "Bad Request: User ID is required but not provided.";

        public const int InternalServerErrorCode = 500;
        public const string InternalServerErrorMessage = "Internal Server Error: An unexpected error occurred.";
    }
}