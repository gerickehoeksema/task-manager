using TaskManager.Application.Interfaces;

namespace TaskManager.Infrastucture.Configuration
{
    public class JwtConfiguration : IJwtConfiguration
    {
        public string Secret { get; set; }
    }
}
