namespace weather.station.server.Services
{
    public interface IRateLimitService
    {
        bool AllowRequest(string ip);

        void RegisterRequest(string ip);
    }
}