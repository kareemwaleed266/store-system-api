namespace Store.Service.Services.CacheService
{
    public interface ICacheService
    {
        Task SetCacheRespoonseAsync(string key, object response, TimeSpan timeToLivw);
        Task<string> GetCacheRespoonseAsync(string key);
    }
}
