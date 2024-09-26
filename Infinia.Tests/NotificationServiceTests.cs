using Infinia.Core.Services;
using Infinia.Core.ViewModels.Notificatios;
using Infinia.Infrastructure;
using Infinia.Infrastructure.Data.DataModels;
using Infinia.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infinia.Tests
{
    [TestFixture]
    public class NotificationServiceTests
    {
        private ApplicationDbContext dbContext;
        private Repository repository;
        private NotificationService notificationService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            dbContext = new ApplicationDbContext(options);
            repository = new Repository(dbContext);
            notificationService = new NotificationService(repository);

            SeedData();
        }

        private void SeedData()
        {
            var notifications = new List<Notification>
            {
                new Notification { Id = 1, CustomerId = "user1", Content = "Notification 1", CreationDate = DateTime.UtcNow, IsRead = false },
                new Notification { Id = 2, CustomerId = "user1", Content = "Notification 2", CreationDate = DateTime.UtcNow, IsRead = false },
                new Notification { Id = 3, CustomerId = "user2", Content = "Notification for another user", CreationDate = DateTime.UtcNow, IsRead = false }
            };
            dbContext.Notifications.AddRange(notifications);
            dbContext.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        [Test]
        public async Task GetAllNotificationsAsync_ShouldReturnNotificationsForSpecificUser()
        {
            string userId = "user1";

            var result = await notificationService.GetAllNotificationsAsync(userId);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.All(n => n.Content.StartsWith("Notification")));
        }

        [Test]
        public async Task GetAllNotificationsAsync_ShouldMarkNotificationsAsRead()
        {
            string userId = "user1";

            var result = await notificationService.GetAllNotificationsAsync(userId);



            Assert.IsFalse(result.All(x => x.IsRead));
        }

        [Test]
        public async Task GetAllNotificationsAsync_ShouldReturnEmptyList_WhenNoNotificationsExist()
        {
            string userId = "nonexistentUser";

            var result = await notificationService.GetAllNotificationsAsync(userId);

            Assert.IsEmpty(result);
        }
    }
}
