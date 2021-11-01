using Core.ServiceBus.Dto;

namespace Center.Common.Api
{
    public class SiteSettings
    {
        public int DataCacheTimeInMinute { get; set; }
        public HttpBaseUrls HttpBaseUrls { get; set; }
        public JwtSettings JwtSettings { get; set; }
        public FileUploadSettings FileUpload { get; set; }
        public MassTransitConfigDto MassTransitConfig { get; set; }
        public CapConfigDto CapConfig { get; set; }
    }

    public class HttpBaseUrls
    {
        public string SecurityWebApi { get; set; }
        public string GeneralVariableWebApi { get; set; }
        public string GeneralApi { get; set; }
    }

    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int NotBeforeMinutes { get; set; }
        public int ExpirationMinutes { get; set; }
    }
    public class FileUploadSettings
    {
        public string ImageUploadPath { get; set; }
    }
    public class CapConfigDto
    {
        public int FailedRetryCount { get; set; }
        public int FailedRetryInterval { get; set; }
        public int SucceedMessageExpiredAfter { get; set; }
        public CapRabbitMQConfigDto RabbitMQ { get; set; }
    }

    public class CapRabbitMQConfigDto
    {
        public string Host { get; set; }
        public ushort Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
