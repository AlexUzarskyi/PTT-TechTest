namespace PTTTest01.DataAccess
{
    public interface IAvatarRepository
    {
        public Task<string?> GetImageUrlById(char lastDigit);
    }
}
