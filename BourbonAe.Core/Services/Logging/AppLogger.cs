using Microsoft.Extensions.Logging;

namespace BourbonAe.Core.Services.Logging
{
    public interface IAppLogger<T>
    {
        void Debug(string message);
        void Info(string message);
        void Warn(string message);
        void Error(string message, Exception? ex = null);
        void Critical(string message, Exception? ex = null);
    }

    public sealed class AppLogger<T> : IAppLogger<T>
    {
        private readonly ILogger<T> _logger;
        public AppLogger(ILogger<T> logger) => _logger = logger;

        public void Debug(string message) => _logger.LogDebug(message);
        public void Info(string message) => _logger.LogInformation(message);
        public void Warn(string message) => _logger.LogWarning(message);
        public void Error(string message, Exception? ex = null) => _logger.LogError(ex, "{Message}", message);
        public void Critical(string message, Exception? ex = null) => _logger.LogCritical(ex, "{Message}", message);
    }
}
