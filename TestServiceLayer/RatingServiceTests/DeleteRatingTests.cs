// <copyright file="DeleteRatingTests.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace RatingServiceTests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using DataMapper.Interfaces;
    using DomainModel.Enums;
    using DomainModel.Models;
    using log4net;
    using Moq;
    using NUnit.Framework;
    using ServiceLayer.Implementation;

    /// <summary>
    ///     Test class for <see cref="RatingServicesImplementation.DeleteRating(Rating)"/> method.
    /// </summary>
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal class DeleteRatingTests
    {
        /// <summary>Null rating log message.</summary>
        private const string LogDeleteNullRating = "Attempted to delete a null rating.";

        /// <summary>Existing rating log message.</summary>
        private const string LogDeleteNonexistingRating = "Attempted to delete a nonexisting rating.";

        /// <summary>
        ///     Test for deleting a null rating.
        /// </summary>
        [Test]
        public void DELETE_NullRating()
        {
            Rating rating = null;

            var ratingServiceMock = new Mock<IRatingDataServices>();
            var loggerMock = new Mock<ILog>();

            var ratingServices = new RatingServicesImplementation(ratingServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(ratingServices.DeleteRating(rating));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogDeleteNullRating)));
        }

        /// <summary>
        ///     Test for deleting a non-existing rating.
        /// </summary>
        [Test]
        public void DELETE_NonExistingRating()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today,
                    DateTime.Today.AddDays(1)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                8);
            Rating nullRating = null;

            var ratingServiceMock = new Mock<IRatingDataServices>();
            ratingServiceMock.Setup(x => x.GetRatingById(rating.Id)).Returns(nullRating);
            var loggerMock = new Mock<ILog>();

            var ratingServices = new RatingServicesImplementation(ratingServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(ratingServices.DeleteRating(rating));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogDeleteNonexistingRating)));
        }

        /// <summary>
        ///     Test for deleting a rating.
        /// </summary>
        [Test]
        public void DELETE_ValidRating()
        {
            Rating rating = new Rating(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today,
                    DateTime.Today.AddDays(1)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                8);

            var ratingServiceMock = new Mock<IRatingDataServices>();
            ratingServiceMock.Setup(x => x.GetRatingById(rating.Id)).Returns(rating);
            ratingServiceMock.Setup(x => x.DeleteRating(rating)).Returns(true);
            var loggerMock = new Mock<ILog>();

            var ratingServices = new RatingServicesImplementation(ratingServiceMock.Object, loggerMock.Object);

            Assert.IsTrue(ratingServices.DeleteRating(rating));
        }
    }
}
