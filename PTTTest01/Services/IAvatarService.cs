namespace PTTTest01.Services
{
    public interface IAvatarService
    {
        Task<string?> GetAvatarUrl(string? userIdentifier);
    }
}
