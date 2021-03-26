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
        private const string MigrationErrorMessage = "Error while trying to migrate database.";
        private readonly string _connectionString;
        private readonly ILogger<DataContextFactory> _logger;

        public DataContextFactory(ILogger<DataContextFactory> logger, string connectionString)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _connectionString = connectionString;

            Migrate();
        }

        public DataContext Create()
        {
            return new DataContext(_connectionString);
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
                _logger.LogError(e, MigrationErrorMessage);
                throw;
            }

            //if (context.Database?.EnsureCreated() != true)
            //{
            //    _logger.LogError(MigrationErrorMessage);
            //    throw new InvalidOperationException(nameof(Migrate));
            //}
        }
    }
}