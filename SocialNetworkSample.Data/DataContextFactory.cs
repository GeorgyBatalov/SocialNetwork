using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SocialNetworkSample.Data.Abstract;

namespace SocialNetworkSample.Data
{
    /// <summary>
    ///     Data context factory. Use as singleton please.
    /// </summary>
    /// <remarks>Можно конечно самому здесь сделать синглтон, но лучше укажем это при регистрации сервиса</remarks>
    public class DataContextFactory : IDataContextFactory
    {
        private readonly ILogger<DataContextFactory> _logger;

        public DataContextFactory(ILogger<DataContextFactory> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            Migrate();
        }

        public DataContext Create()
        {
            return new DataContext();
        }

        private void Migrate()
        {
            var context = Create();

            try
            {
                context.Database.MigrateAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while trying to migrate database.");
                throw;
            }
        }
    }
}