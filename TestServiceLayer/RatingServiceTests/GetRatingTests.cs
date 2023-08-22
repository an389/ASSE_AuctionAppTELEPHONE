// <copyright file="GetRatingTests.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace RatingServiceTests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using DataMapper.Interfaces;
    using DomainModel.Enums;
    using DomainModel.Models;
    using log4net;
    using Moq;
    using NUnit.Framework;
    using ServiceLayer.Implementation;

    /// <summary>
    ///     Test class for
    ///         <see cref="RatingServicesImplementation.GetAllRatings()"/>,
    ///         <see cref="RatingServicesImplementation.GetRatingById(int)"/> and
    ///         <see cref="RatingServicesImplementation.GetRatingsByUserId(int)"/>
    ///     methods.
    /// </summary>
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal class GetRatingTests
    {
        /// <summary>
        ///     Test for retrieving all existing ratings.
        /// </summary>
        [Test]
        public void GET_AllRatings()
        {
            List<Rating> ratings = GetSampleRatings();

            var ratingServiceMock = new Mock<IRatingDataServices>();
            ratingServiceMock.Setup(x => x.GetAllRatings()).Returns(ratings);
            var loggerMock = new Mock<ILog>();

            var bidServices = new RatingServicesImplementation(ratingServiceMock.Object, loggerMock.Object);

            var expected = ratings;
            var actual = bidServices.GetAllRatings();

            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Id, actual[i].Id);
                Assert.AreEqual(expected[i].DateAndTime, actual[i].DateAndTime);

                Assert.AreEqual(expected[i].Product.Id, actual[i].Product.Id);
                Assert.AreEqual(expected[i].Product.Name, actual[i].Product.Name);
                Assert.AreEqual(expected[i].Product.Description, actual[i].Product.Description);
                Assert.AreEqual(expected[i].Product.Category.Id, actual[i].Product.Category.Id);
                Assert.AreEqual(expected[i].Product.Category.Name, actual[i].Product.Category.Name);
                Assert.AreEqual(expected[i].Product.Category.ParentCategory, actual[i].Product.Category.ParentCategory);
                Assert.AreEqual(expected[i].Product.StartingPrice, actual[i].Product.StartingPrice);
                Assert.AreEqual(expected[i].Product.Currency, actual[i].Product.Currency);
                Assert.AreEqual(expected[i].Product.CreationDate, actual[i].Product.CreationDate);
                Assert.AreEqual(expected[i].Product.StartDate, actual[i].Product.StartDate);
                Assert.AreEqual(expected[i].Product.EndDate, actual[i].Product.EndDate);
                Assert.AreEqual(expected[i].Product.TerminationDate, actual[i].Product.TerminationDate);

                Assert.AreEqual(expected[i].RatingUser.Id, actual[i].RatingUser.Id);
                Assert.AreEqual(expected[i].RatingUser.FirstName, actual[i].RatingUser.FirstName);
                Assert.AreEqual(expected[i].RatingUser.LastName, actual[i].RatingUser.LastName);
                Assert.AreEqual(expected[i].RatingUser.UserName, actual[i].RatingUser.UserName);
                Assert.AreEqual(expected[i].RatingUser.PhoneNumber, actual[i].RatingUser.PhoneNumber);
                Assert.AreEqual(expected[i].RatingUser.Email, actual[i].RatingUser.Email);
                Assert.AreEqual(expected[i].RatingUser.Password, actual[i].RatingUser.Password);
                Assert.AreEqual(expected[i].RatingUser.AccountType, actual[i].RatingUser.AccountType);

                Assert.AreEqual(expected[i].RatedUser.Id, actual[i].RatedUser.Id);
                Assert.AreEqual(expected[i].RatedUser.FirstName, actual[i].RatedUser.FirstName);
                Assert.AreEqual(expected[i].RatedUser.LastName, actual[i].RatedUser.LastName);
                Assert.AreEqual(expected[i].RatedUser.UserName, actual[i].RatedUser.UserName);
                Assert.AreEqual(expected[i].RatedUser.PhoneNumber, actual[i].RatedUser.PhoneNumber);
                Assert.AreEqual(expected[i].RatedUser.Email, actual[i].RatedUser.Email);
                Assert.AreEqual(expected[i].RatedUser.Password, actual[i].RatedUser.Password);
                Assert.AreEqual(expected[i].RatedUser.AccountType, actual[i].RatedUser.AccountType);

                Assert.AreEqual(expected[i].Grade, actual[i].Grade);
            }
        }

        /// <summary>
        ///     Test for retrieving all existing ratings but none were found.
        /// </summary>
        [Test]
        public void GET_AllRatings_NoneFound()
        {
            List<Rating> emptyRatingList = new List<Rating>();

            var ratingServiceMock = new Mock<IRatingDataServices>();
            ratingServiceMock.Setup(x => x.GetAllRatings()).Returns(emptyRatingList);
            var loggerMock = new Mock<ILog>();

            var bidServices = new RatingServicesImplementation(ratingServiceMock.Object, loggerMock.Object);

            Assert.IsEmpty(bidServices.GetAllRatings());
        }

        /// <summary>
        ///     Test for retrieving an existing rating with the specified id.
        /// </summary>
        [Test]
        public void GET_RatingById()
        {
            Rating rating = GetSampleRating();

            var ratingServiceMock = new Mock<IRatingDataServices>();
            ratingServiceMock.Setup(x => x.GetRatingById(rating.Id)).Returns(rating);
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new RatingServicesImplementation(ratingServiceMock.Object, loggerMock.Object);

            var expected = rating;
            var actual = bidServices.GetRatingById(rating.Id);

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.DateAndTime, actual.DateAndTime);

            Assert.AreEqual(expected.Product.Id, actual.Product.Id);
            Assert.AreEqual(expected.Product.Name, actual.Product.Name);
            Assert.AreEqual(expected.Product.Description, actual.Product.Description);
            Assert.AreEqual(expected.Product.Category.Id, actual.Product.Category.Id);
            Assert.AreEqual(expected.Product.Category.Name, actual.Product.Category.Name);
            Assert.AreEqual(expected.Product.Category.ParentCategory, actual.Product.Category.ParentCategory);
            Assert.AreEqual(expected.Product.StartingPrice, actual.Product.StartingPrice);
            Assert.AreEqual(expected.Product.Currency, actual.Product.Currency);
            Assert.AreEqual(expected.Product.CreationDate, actual.Product.CreationDate);
            Assert.AreEqual(expected.Product.StartDate, actual.Product.StartDate);
            Assert.AreEqual(expected.Product.EndDate, actual.Product.EndDate);
            Assert.AreEqual(expected.Product.TerminationDate, actual.Product.TerminationDate);

            Assert.AreEqual(expected.RatingUser.Id, actual.RatingUser.Id);
            Assert.AreEqual(expected.RatingUser.FirstName, actual.RatingUser.FirstName);
            Assert.AreEqual(expected.RatingUser.LastName, actual.RatingUser.LastName);
            Assert.AreEqual(expected.RatingUser.UserName, actual.RatingUser.UserName);
            Assert.AreEqual(expected.RatingUser.PhoneNumber, actual.RatingUser.PhoneNumber);
            Assert.AreEqual(expected.RatingUser.Email, actual.RatingUser.Email);
            Assert.AreEqual(expected.RatingUser.Password, actual.RatingUser.Password);
            Assert.AreEqual(expected.RatingUser.AccountType, actual.RatingUser.AccountType);

            Assert.AreEqual(expected.RatedUser.Id, actual.RatedUser.Id);
            Assert.AreEqual(expected.RatedUser.FirstName, actual.RatedUser.FirstName);
            Assert.AreEqual(expected.RatedUser.LastName, actual.RatedUser.LastName);
            Assert.AreEqual(expected.RatedUser.UserName, actual.RatedUser.UserName);
            Assert.AreEqual(expected.RatedUser.PhoneNumber, actual.RatedUser.PhoneNumber);
            Assert.AreEqual(expected.RatedUser.Email, actual.RatedUser.Email);
            Assert.AreEqual(expected.RatedUser.Password, actual.RatedUser.Password);
            Assert.AreEqual(expected.RatedUser.AccountType, actual.RatedUser.AccountType);

            Assert.AreEqual(expected.Grade, actual.Grade);
        }

        /// <summary>
        ///     Test for retrieving an existing rating with the specified id but no such rating was found.
        /// </summary>
        [Test]
        public void GET_RatingById_NotFound()
        {
            Rating rating = GetSampleRating();
            Rating nullRating = null;

            var ratingServiceMock = new Mock<IRatingDataServices>();
            ratingServiceMock.Setup(x => x.GetRatingById(rating.Id)).Returns(nullRating);
            var loggerMock = new Mock<ILog>();

            var bidServices = new RatingServicesImplementation(ratingServiceMock.Object, loggerMock.Object);

            Assert.IsNull(bidServices.GetRatingById(rating.Id));
        }

        /// <summary>
        ///     Test for retrieving all existing ratings of a user with the specified id.
        /// </summary>
        [Test]
        public void GET_RatingsByUserId()
        {
            Rating rating = GetSampleRating();
            IList<Rating> ratings = GetSampleRatings();

            var ratingServiceMock = new Mock<IRatingDataServices>();
            ratingServiceMock.Setup(x => x.GetRatingsByUserId(rating.RatedUser.Id)).Returns(ratings);
            var loggerMock = new Mock<ILog>();

            var bidServices = new RatingServicesImplementation(ratingServiceMock.Object, loggerMock.Object);

            var expected = ratings;
            var actual = bidServices.GetRatingsByUserId(rating.RatedUser.Id);

            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Id, actual[i].Id);
                Assert.AreEqual(expected[i].DateAndTime, actual[i].DateAndTime);

                Assert.AreEqual(expected[i].Product.Id, actual[i].Product.Id);
                Assert.AreEqual(expected[i].Product.Name, actual[i].Product.Name);
                Assert.AreEqual(expected[i].Product.Description, actual[i].Product.Description);
                Assert.AreEqual(expected[i].Product.Category.Id, actual[i].Product.Category.Id);
                Assert.AreEqual(expected[i].Product.Category.Name, actual[i].Product.Category.Name);
                Assert.AreEqual(expected[i].Product.Category.ParentCategory, actual[i].Product.Category.ParentCategory);
                Assert.AreEqual(expected[i].Product.StartingPrice, actual[i].Product.StartingPrice);
                Assert.AreEqual(expected[i].Product.Currency, actual[i].Product.Currency);
                Assert.AreEqual(expected[i].Product.CreationDate, actual[i].Product.CreationDate);
                Assert.AreEqual(expected[i].Product.StartDate, actual[i].Product.StartDate);
                Assert.AreEqual(expected[i].Product.EndDate, actual[i].Product.EndDate);
                Assert.AreEqual(expected[i].Product.TerminationDate, actual[i].Product.TerminationDate);

                Assert.AreEqual(expected[i].RatingUser.Id, actual[i].RatingUser.Id);
                Assert.AreEqual(expected[i].RatingUser.FirstName, actual[i].RatingUser.FirstName);
                Assert.AreEqual(expected[i].RatingUser.LastName, actual[i].RatingUser.LastName);
                Assert.AreEqual(expected[i].RatingUser.UserName, actual[i].RatingUser.UserName);
                Assert.AreEqual(expected[i].RatingUser.PhoneNumber, actual[i].RatingUser.PhoneNumber);
                Assert.AreEqual(expected[i].RatingUser.Email, actual[i].RatingUser.Email);
                Assert.AreEqual(expected[i].RatingUser.Password, actual[i].RatingUser.Password);
                Assert.AreEqual(expected[i].RatingUser.AccountType, actual[i].RatingUser.AccountType);

                Assert.AreEqual(expected[i].RatedUser.Id, actual[i].RatedUser.Id);
                Assert.AreEqual(expected[i].RatedUser.FirstName, actual[i].RatedUser.FirstName);
                Assert.AreEqual(expected[i].RatedUser.LastName, actual[i].RatedUser.LastName);
                Assert.AreEqual(expected[i].RatedUser.UserName, actual[i].RatedUser.UserName);
                Assert.AreEqual(expected[i].RatedUser.PhoneNumber, actual[i].RatedUser.PhoneNumber);
                Assert.AreEqual(expected[i].RatedUser.Email, actual[i].RatedUser.Email);
                Assert.AreEqual(expected[i].RatedUser.Password, actual[i].RatedUser.Password);
                Assert.AreEqual(expected[i].RatedUser.AccountType, actual[i].RatedUser.AccountType);

                Assert.AreEqual(expected[i].Grade, actual[i].Grade);
            }
        }

        /// <summary>
        ///     Test for retrieving all existing ratings of a user with the specified id but no such ratings were found.
        /// </summary>
        [Test]
        public void GET_RatingsByUserId_NoneFound()
        {
            Rating rating = GetSampleRating();
            List<Rating> emptyRatingList = new List<Rating>();

            var ratingServiceMock = new Mock<IRatingDataServices>();
            ratingServiceMock.Setup(x => x.GetRatingsByUserId(rating.RatedUser.Id)).Returns(emptyRatingList);
            var loggerMock = new Mock<ILog>();

            var bidServices = new RatingServicesImplementation(ratingServiceMock.Object, loggerMock.Object);

            Assert.IsEmpty(bidServices.GetRatingsByUserId(rating.RatedUser.Id));
        }

        /// <summary>Gets a sample rating.</summary>
        /// <returns>a sample rating.</returns>
        private static Rating GetSampleRating()
        {
            return new Rating(
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
        }

        /// <summary>Gets sample ratings.</summary>
        /// <returns>a list of sample ratings.</returns>
        private static List<Rating> GetSampleRatings()
        {
            return new List<Rating>
            {
                new Rating(
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
                    8),
                new Rating(
                    new Product(
                        "Aparat foto CANNON",
                        "face poze",
                        new Category("Aparat foto", null),
                        100,
                        ECurrency.EUR,
                        new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                        DateTime.Today,
                        DateTime.Today.AddDays(1)),
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                    10),
            };
        }
    }
}
