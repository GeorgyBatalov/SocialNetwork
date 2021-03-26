using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkSample.Data;
using SocialNetworkSample.Data.Entities;

namespace SocialNetworkSample.IntegrationTests
{
    [TestClass]
    public class DataContextFactory_Should
    {
        private string _databaseFilePath;

        [TestInitialize]
        public void TestInitialize()
        {
            _databaseFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{Guid.NewGuid():N}.db");
        }

        /// <summary>
        ///     Интеграционный тест на то создаётся/не создаётся база с помощью DataContextFactory, работают/не работают
        ///     элементарыне запросы и вставки в
        ///     табличку значений.
        /// </summary>
        [TestMethod]
        public async Task Create_SqliteDatabaseWithoutMigrationErrors()
        {
            var connectionString = $"Data Source={_databaseFilePath}";

            ILoggerFactory loggerFactory = new NullLoggerFactory();

            var dataContextFactory = new DataContextFactory(loggerFactory.CreateLogger<DataContextFactory>(), connectionString);

            using (var dataContext = dataContextFactory.Create())
            {
                //Assert.IsTrue(await dataContext.Database.EnsureCreatedAsync(), "Database hos not been created");

                Assert.IsTrue(dataContext.Database.IsSqlite(), "Is it not Sqlite database");
            }

            // Simple request
            using (var dataContext = dataContextFactory.Create())
            {
                Assert.AreEqual(await dataContext.Clients.CountAsync(), 0);
            }

            var clientName = Guid.NewGuid().ToString();
            Guid clientId;

            // Create entity request
            using (var dataContext = dataContextFactory.Create())
            {
                var clientEntity = new ClientEntity
                {
                    Name = clientName
                };

                await dataContext.AddAsync(clientEntity);

                await dataContext.SaveChangesAsync();

                clientId = clientEntity.Id;
            }

            // Simple request
            using (var dataContext = dataContextFactory.Create())
            {
                Assert.AreEqual(await dataContext.Clients.CountAsync(), 1);

                var client = await dataContext.Clients.SingleOrDefaultAsync(x => x.Id == clientId);

                Assert.IsNotNull(client);

                Assert.AreEqual(client.Id, clientId);
                Assert.AreEqual(client.Name, clientName);
            }
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (File.Exists(_databaseFilePath))
                File.Delete(_databaseFilePath);
        }
    }
}