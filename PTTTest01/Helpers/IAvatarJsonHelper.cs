namespace PTTTest01.Helpers
{
    public interface IAvatarJsonHelper
    {
        public Task<string?> FetchAndExtractProperty(string url, string propertyName);
    }
}
