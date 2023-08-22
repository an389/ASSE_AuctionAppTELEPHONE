// <copyright file="AddBidTests.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace BidServiceTests
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
    ///     Test class for <see cref="BidServicesImplementation.AddBid(Bid)"/> method.
    /// </summary>
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal class AddBidTests
    {
        /// <summary>Null bid log message.</summary>
        private const string LogAddNullBid = "Attempted to add a null bid.";

        /// <summary>Invalid bid log message.</summary>
        private const string LogAddInvalidBid = "Attempted to add an invalid bid.";

        /// <summary>Too many active bids log message.</summary>
        private const string LogAddTooManyBids = "Attempted to bid over limit.";

        /// <summary>Inactive auction bid log message.</summary>
        private const string LogAddEarlyOrLateBid = "Attempted to bid when the auction didn't start or has already ended.";

        /// <summary>Self bid log message.</summary>
        private const string LogAddSelfBid = "Attempted to bid on an owned product.";

        /// <summary>Wrong currency bid log message.</summary>
        private const string LogAddWrongCurrencyBid = "Attempted to bid with different currency.";

        /// <summary>Not enough money or user has top bid log message.</summary>
        private const string LogAddNotEnoughOrUserIsAlreadyOnTopBid = "Attempted to bid with not enough money or user already has the winning bid.";

        /// <summary>
        ///     Test for adding a null bid.
        /// </summary>
        [Test]
        public void ADD_NullBid()
        {
            Bid bid = null;

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogAddNullBid)));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with null product).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_NullProduct()
        {
            Bid bid = new Bid(
                null,
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with null product name).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_Name_Null()
        {
            Bid bid = new Bid(
                new Product(
                    null,
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with empty product name).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_Name_Empty()
        {
            Bid bid = new Bid(
                new Product(
                    string.Empty,
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with product name too long).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_Name_TooLong()
        {
            Bid bid = new Bid(
                new Product(
                    new string('x', 251),
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with null product description).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_Description_Null()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    null,
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with empty product description).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_Description_Empty()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    string.Empty,
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with product description too long).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_Description_TooLong()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    new string('x', 750),
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with null product category).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_NullCategory()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    null,
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with null product category name).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_InvalidCategory_Name_Null()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category(null, null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with empty product category name).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_InvalidCategory_Name_Empty()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category(string.Empty, null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with product category name too long).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_InvalidCategory_Name_TooLong()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category(new string('x', 125), null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with product starting price negative).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_StartingPrice_Negative()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    -1,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with null product seller).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_NullSeller()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    null,
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with null product seller first name).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_InvalidSeller_FirstName_Null()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User(null, "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with empty product seller first name).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_InvalidSeller_FirstName_Empty()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User(string.Empty, "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with product seller first name too long).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_InvalidSeller_FirstName_TooLong()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User('X' + new string('x', 16), "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with product seller first name that doesn't start with an uppercase letter).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_InvalidSeller_FirstName_NoUpperCaseLetter()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User(new string('x', 10), "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with product seller first name that doesn't contain lowercase letters).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_InvalidSeller_FirstName_NoLowerCaseLetters()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User(new string('X', 10), "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with product seller first name that contains symbols).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_InvalidSeller_FirstName_ContainsSymbol()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("D!nu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with product seller first name that contains numbers).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_InvalidSeller_FirstName_ContainsNumber()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("D1nu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with null product seller last name).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_InvalidSeller_LastName_Null()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", null, "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with empty product seller last name).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_InvalidSeller_LastName_Empty()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", string.Empty, "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with product seller last name too long).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_InvalidSeller_LastName_TooLong()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", 'X' + new string('x', 16), "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with product seller first name that doesn't start with an uppercase letter).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_InvalidSeller_LastName_NoUpperCaseLetter()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", new string('x', 10), "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with product seller first name that doesn't contain lowercase letters).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_InvalidSeller_LastName_NoLowerCaseLetters()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", new string('X', 10), "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with product seller first name that contains symbols).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_InvalidSeller_LastName_ContainsSymbol()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "G@rbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with product seller first name that contains numbers).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_InvalidSeller_LastName_ContainsNumber()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "G4rbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with null product seller username).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_InvalidSeller_UserName_Null()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", null, "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with empty product seller username).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_InvalidSeller_UserName_Empty()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", string.Empty, "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with product seller username too long).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_InvalidSeller_UserName_TooLong()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", new string('x', 31), "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding a valid bid (bid with null product seller phone number).
        /// </summary>
        [Test]
        public void ADD_ValidBid_ValidProduct_ValidSeller_PhoneNumber_Null()
        {
            string nullPhoneNumber = null;

            var sellerMock = new Mock<User>("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123");
            sellerMock.SetupGet(x => x.Id).Returns(3748);
            sellerMock.SetupGet(x => x.FirstName).Returns("Dinu");
            sellerMock.SetupGet(x => x.LastName).Returns("Garbuz");
            sellerMock.SetupGet(x => x.UserName).Returns("GbDinu");
            sellerMock.SetupGet(x => x.PhoneNumber).Returns(nullPhoneNumber);
            sellerMock.SetupGet(x => x.Email).Returns("dinu.garbuz@FakeEmail.com");
            sellerMock.SetupGet(x => x.Password).Returns("P@rola123");

            var productMock = new Mock<Product>("Aparat foto CANNON", "face poze", new Category("Aparat foto", null), 100, ECurrency.EUR, new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"), DateTime.Today.AddDays(-5), DateTime.Today.AddDays(5));
            productMock.SetupGet(x => x.Id).Returns(1);
            productMock.SetupGet(x => x.Name).Returns("Aparat foto CANNON");
            productMock.SetupGet(x => x.Description).Returns("face poze");
            productMock.SetupGet(x => x.Category).Returns(new Category("Aparat foto", null));
            productMock.SetupGet(x => x.StartingPrice).Returns(100);
            productMock.SetupGet(x => x.Currency).Returns(ECurrency.EUR);
            productMock.SetupGet(x => x.Seller).Returns(sellerMock.Object);
            productMock.SetupGet(x => x.CreationDate).Returns(DateTime.Today.AddDays(-30));
            productMock.SetupGet(x => x.StartDate).Returns(DateTime.Today.AddDays(-5));
            productMock.SetupGet(x => x.EndDate).Returns(DateTime.Today.AddDays(5));
            productMock.SetupGet(x => x.TerminationDate).Returns(DateTime.Today.AddDays(5));

            var buyer1Mock = new Mock<User>("Andrei", "Costache", "Costica", "8888888888", "andrei.costrache@FakeEmail.com", "@AbCd123");
            buyer1Mock.SetupGet(x => x.Id).Returns(3);
            buyer1Mock.SetupGet(x => x.FirstName).Returns("Andrei");
            buyer1Mock.SetupGet(x => x.LastName).Returns("Costache");
            buyer1Mock.SetupGet(x => x.UserName).Returns("Costica");
            buyer1Mock.SetupGet(x => x.PhoneNumber).Returns("8888888888");
            buyer1Mock.SetupGet(x => x.Email).Returns("andrei.costrache@FakeEmail.com");
            buyer1Mock.SetupGet(x => x.Password).Returns("@AbCd123");

            var buyer2Mock = new Mock<User>("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");
            buyer2Mock.SetupGet(x => x.Id).Returns(2594);
            buyer2Mock.SetupGet(x => x.FirstName).Returns("Adrian");
            buyer2Mock.SetupGet(x => x.LastName).Returns("Matei");
            buyer2Mock.SetupGet(x => x.UserName).Returns("AdiMatei20");
            buyer2Mock.SetupGet(x => x.PhoneNumber).Returns("0123456789");
            buyer2Mock.SetupGet(x => x.Email).Returns("adrian.matei@FakeEmail.com");
            buyer2Mock.SetupGet(x => x.Password).Returns("P@ssword123");

            var bid1Mock = new Mock<Bid>(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Andrei", "Costache", "Costica", "8888888888", "andrei.costrache@FakeEmail.com", "@AbCd123"),
                150,
                ECurrency.EUR);

            bid1Mock.SetupGet(x => x.Id).Returns(73);
            bid1Mock.SetupGet(x => x.DateAndTime).Returns(DateTime.Now);
            bid1Mock.SetupGet(x => x.Product).Returns(productMock.Object);
            bid1Mock.SetupGet(x => x.Buyer).Returns(buyer1Mock.Object);
            bid1Mock.SetupGet(x => x.Amount).Returns(150);
            bid1Mock.SetupGet(x => x.Currency).Returns(ECurrency.EUR);

            var bid2Mock = new Mock<Bid>(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                200,
                ECurrency.EUR);

            bid2Mock.SetupGet(x => x.Id).Returns(74);
            bid2Mock.SetupGet(x => x.DateAndTime).Returns(DateTime.Now);
            bid2Mock.SetupGet(x => x.Product).Returns(productMock.Object);
            bid2Mock.SetupGet(x => x.Buyer).Returns(buyer2Mock.Object);
            bid2Mock.SetupGet(x => x.Amount).Returns(200);
            bid2Mock.SetupGet(x => x.Currency).Returns(ECurrency.EUR);

            Bid bid1 = bid1Mock.Object;
            Bid bid2 = bid2Mock.Object;

            var bidServiceMock = new Mock<IBidDataServices>();
            bidServiceMock.Setup(x => x.GetNoOfActiveBidsByUserId(bid2.Buyer.Id)).Returns(5);
            bidServiceMock.Setup(x => x.GetBidsByProductId(bid2.Product.Id)).Returns(new List<Bid>() { bid1 });
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            userScoreAndLimitsServiceMock.Setup(x => x.GetUserLimitByUserId(bid2.Buyer.Id)).Returns(10);
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid2));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with empty product seller phone number).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_InvalidSeller_PhoneNumber_Empty()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", string.Empty, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with product seller phone number too long).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_InvalidSeller_PhoneNumber_TooLong()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", new string('8', 16), "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with invalid product seller phone number).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_InvalidSeller_PhoneNumber_Invalid()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "abc", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with null product seller email address).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_InvalidSeller_Email_Null()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", null, "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            bidServiceMock.Setup(x => x.GetBidsByProductId(bid.Id)).Returns(new List<Bid>());
            bidServiceMock.Setup(x => x.AddBid(bid)).Returns(true);
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with empty product seller email address).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_InvalidSeller_Email_Empty()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", string.Empty, "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with product seller email address too long).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_InvalidSeller_Email_TooLong()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", new string('x', 30) + '@' + new string('x', 30), "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with invalid product seller email address).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_InvalidSeller_Email_Invalid()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuzFakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with null product seller password).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_InvalidSeller_Password_Null()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", null),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with empty product seller password).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_InvalidSeller_Password_Empty()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", string.Empty),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with product seller password too short).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_InvalidSeller_Password_TooShort()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "A#a1"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with product seller password too long).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_InvalidSeller_Password_TooLong()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "A#a1" + new string('x', 20)),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with product seller password that doesn't contain uppercase letters).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_InvalidSeller_Password_MissingUpperCaseLetter()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "p@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with product seller password that doesn't contain lowercase letters).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_InvalidSeller_Password_MissingLowerCaseLetter()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@ROLA123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with product seller password that doesn't contain numbers).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_InvalidSeller_Password_MissingNumber()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rolaP@rola"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with product seller password that doesn't contain symbols).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_InvalidSeller_Password_MissingSymbol()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "Parola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with product auction end date before auction start date).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidProduct_EndDate_BeforeStartDate()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with null buyer).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_NullBuyer()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                null,
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with null buyer first name).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidBuyer_FirstName_Null()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User(null, "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with empty buyer first name).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidBuyer_FirstName_Empty()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User(string.Empty, "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with buyer first name too long).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidBuyer_FirstName_TooLong()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User('X' + new string('x', 16), "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with buyer first name that doesn't start with an uppercase letter).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidBuyer_FirstName_NoUpperCaseLetter()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User(new string('x', 10), "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with buyer first name that only has uppercase letters).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidBuyer_FirstName_NoLowerCaseLetters()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User(new string('X', 10), "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with buyer first name that contains symbols).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidBuyer_FirstName_ContainsSymbol()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adr!an", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with buyer first name that contains numbers).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidBuyer_FirstName_ContainsNumber()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adr1an", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with null buyer last name).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidBuyer_LastName_Null()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", null, "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with empty buyer last name).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidBuyer_LastName_Empty()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", string.Empty, "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with buyer last name too long).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidBuyer_LastName_TooLong()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", 'X' + new string('x', 16), "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with buyer last name that doesn't start with an uppercase letter).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidBuyer_LastName_NoUpperCaseLetter()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", new string('x', 10), "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with buyer last name that only has uppercase letters).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidBuyer_LastName_NoLowerCaseLetters()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", new string('X', 10), "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with buyer last name that contains symbols).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidBuyer_LastName_ContainsSymbol()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Mate!", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with buyer last name that contains numbers).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidBuyer_LastName_ContainsNumber()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Mat3i", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with null buyer username).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidBuyer_UserName_Null()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", null, "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with empty buyer username).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidBuyer_UserName_Empty()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", string.Empty, "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with buyer username too long).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidBuyer_UserName_TooLong()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", new string('x', 31), "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding a valid bid (bid with null buyer phone number).
        /// </summary>
        [Test]
        public void ADD_ValidBid_ValidBuyer_PhoneNumber_Null()
        {
            string nullPhoneNumber = null;

            var sellerMock = new Mock<User>("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123");
            sellerMock.SetupGet(x => x.Id).Returns(3748);
            sellerMock.SetupGet(x => x.FirstName).Returns("Dinu");
            sellerMock.SetupGet(x => x.LastName).Returns("Garbuz");
            sellerMock.SetupGet(x => x.UserName).Returns("GbDinu");
            sellerMock.SetupGet(x => x.PhoneNumber).Returns("9876543210");
            sellerMock.SetupGet(x => x.Email).Returns("dinu.garbuz@FakeEmail.com");
            sellerMock.SetupGet(x => x.Password).Returns("P@rola123");

            var productMock = new Mock<Product>("Aparat foto CANNON", "face poze", new Category("Aparat foto", null), 100, ECurrency.EUR, new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"), DateTime.Today.AddDays(-5), DateTime.Today.AddDays(5));
            productMock.SetupGet(x => x.Id).Returns(1);
            productMock.SetupGet(x => x.Name).Returns("Aparat foto CANNON");
            productMock.SetupGet(x => x.Description).Returns("face poze");
            productMock.SetupGet(x => x.Category).Returns(new Category("Aparat foto", null));
            productMock.SetupGet(x => x.StartingPrice).Returns(100);
            productMock.SetupGet(x => x.Currency).Returns(ECurrency.EUR);
            productMock.SetupGet(x => x.Seller).Returns(sellerMock.Object);
            productMock.SetupGet(x => x.CreationDate).Returns(DateTime.Today.AddDays(-30));
            productMock.SetupGet(x => x.StartDate).Returns(DateTime.Today.AddDays(-5));
            productMock.SetupGet(x => x.EndDate).Returns(DateTime.Today.AddDays(5));
            productMock.SetupGet(x => x.TerminationDate).Returns(DateTime.Today.AddDays(5));

            var buyer1Mock = new Mock<User>("Andrei", "Costache", "Costica", "8888888888", "andrei.costrache@FakeEmail.com", "@AbCd123");
            buyer1Mock.SetupGet(x => x.Id).Returns(3);
            buyer1Mock.SetupGet(x => x.FirstName).Returns("Andrei");
            buyer1Mock.SetupGet(x => x.LastName).Returns("Costache");
            buyer1Mock.SetupGet(x => x.UserName).Returns("Costica");
            buyer1Mock.SetupGet(x => x.PhoneNumber).Returns("8888888888");
            buyer1Mock.SetupGet(x => x.Email).Returns("andrei.costrache@FakeEmail.com");
            buyer1Mock.SetupGet(x => x.Password).Returns("@AbCd123");

            var buyer2Mock = new Mock<User>("Adrian", "Matei", "AdiMatei20", null, "adrian.matei@FakeEmail.com", "P@ssword123");
            buyer2Mock.SetupGet(x => x.Id).Returns(2594);
            buyer2Mock.SetupGet(x => x.FirstName).Returns("Adrian");
            buyer2Mock.SetupGet(x => x.LastName).Returns("Matei");
            buyer2Mock.SetupGet(x => x.UserName).Returns("AdiMatei20");
            buyer2Mock.SetupGet(x => x.PhoneNumber).Returns(nullPhoneNumber);
            buyer2Mock.SetupGet(x => x.Email).Returns("adrian.matei@FakeEmail.com");
            buyer2Mock.SetupGet(x => x.Password).Returns("P@ssword123");

            var bid1Mock = new Mock<Bid>(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Andrei", "Costache", "Costica", "8888888888", "andrei.costrache@FakeEmail.com", "@AbCd123"),
                150,
                ECurrency.EUR);

            bid1Mock.SetupGet(x => x.Id).Returns(73);
            bid1Mock.SetupGet(x => x.DateAndTime).Returns(DateTime.Now);
            bid1Mock.SetupGet(x => x.Product).Returns(productMock.Object);
            bid1Mock.SetupGet(x => x.Buyer).Returns(buyer1Mock.Object);
            bid1Mock.SetupGet(x => x.Amount).Returns(150);
            bid1Mock.SetupGet(x => x.Currency).Returns(ECurrency.EUR);

            var bid2Mock = new Mock<Bid>(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                200,
                ECurrency.EUR);

            bid2Mock.SetupGet(x => x.Id).Returns(74);
            bid2Mock.SetupGet(x => x.DateAndTime).Returns(DateTime.Now);
            bid2Mock.SetupGet(x => x.Product).Returns(productMock.Object);
            bid2Mock.SetupGet(x => x.Buyer).Returns(buyer2Mock.Object);
            bid2Mock.SetupGet(x => x.Amount).Returns(200);
            bid2Mock.SetupGet(x => x.Currency).Returns(ECurrency.EUR);

            Bid bid1 = bid1Mock.Object;
            Bid bid2 = bid2Mock.Object;

            var bidServiceMock = new Mock<IBidDataServices>();
            bidServiceMock.Setup(x => x.GetNoOfActiveBidsByUserId(bid2.Buyer.Id)).Returns(5);
            bidServiceMock.Setup(x => x.GetBidsByProductId(bid2.Product.Id)).Returns(new List<Bid>() { bid1 });
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            userScoreAndLimitsServiceMock.Setup(x => x.GetUserLimitByUserId(bid2.Buyer.Id)).Returns(10);
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid2));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with empty buyer phone number).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidBuyer_PhoneNumber_Empty()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", string.Empty, "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with buyer phone number too long).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidBuyer_PhoneNumber_TooLong()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", new string('8', 16), "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with invalid buyer phone number).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidBuyer_PhoneNumber_Invalid()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "abc", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with null buyer email address).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidBuyer_Email_Null()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", null, "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            bidServiceMock.Setup(x => x.GetBidsByProductId(bid.Id)).Returns(new List<Bid>());
            bidServiceMock.Setup(x => x.AddBid(bid)).Returns(true);
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with empty buyer email address).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidBuyer_Email_Empty()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", string.Empty, "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with buyer email address too long).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidBuyer_Email_TooLong()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", new string('x', 30) + '@' + new string('x', 30), "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with invalid buyer email address).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidBuyer_Email_Invalid()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.mateiFakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with null buyer password).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidBuyer_Password_Null()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", null),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with empty buyer password).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidBuyer_Password_Empty()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", string.Empty),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with buyer password too short).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidBuyer_Password_TooShort()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "A#a1"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with buyer password too long).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidBuyer_Password_TooLong()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "A#a1" + new string('x', 20)),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with buyer password that doesn't contain uppercase letters).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidBuyer_Password_MissingUpperCaseLetter()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "p@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with buyer password that doesn't contain lowercase letters).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidBuyer_Password_MissingLowerCaseLetter()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@SSWORD123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with buyer password that doesn't contain numbers).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidBuyer_Password_MissingNumber()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with buyer password that doesn't contain symbols).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_InvalidBuyer_Password_MissingSymbol()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "Password123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding an invalid bid (bid with negative amount).
        /// </summary>
        [Test]
        public void ADD_InvalidBid_Amount_Negative()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                -10,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBid))));
        }

        /// <summary>
        ///     Test for adding a valid bid when user already reached the limit of active bids.
        /// </summary>
        [Test]
        public void ADD_ValidBid_TooManyBids()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            bidServiceMock.Setup(x => x.GetNoOfActiveBidsByUserId(bid.Buyer.Id)).Returns(10);
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            userScoreAndLimitsServiceMock.Setup(x => x.GetUserLimitByUserId(bid.Buyer.Id)).Returns(10);
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogAddTooManyBids)));
        }

        /// <summary>
        ///     Test for adding a valid bid before auction started.
        /// </summary>
        [Test]
        public void ADD_ValidBid_AuctionHasntStarted()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(5),
                    DateTime.Today.AddDays(10)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            bidServiceMock.Setup(x => x.GetNoOfActiveBidsByUserId(bid.Buyer.Id)).Returns(5);
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            userScoreAndLimitsServiceMock.Setup(x => x.GetUserLimitByUserId(bid.Buyer.Id)).Returns(10);
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogAddEarlyOrLateBid)));
        }

        /// <summary>
        ///     Test for adding a valid bid after auction ended.
        /// </summary>
        [Test]
        public void ADD_ValidBid_AuctionAlreadyEnded()
        {
            Bid bid = new Bid(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-10),
                    DateTime.Today.AddDays(-5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            var bidServiceMock = new Mock<IBidDataServices>();
            bidServiceMock.Setup(x => x.GetNoOfActiveBidsByUserId(bid.Buyer.Id)).Returns(5);
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            userScoreAndLimitsServiceMock.Setup(x => x.GetUserLimitByUserId(bid.Buyer.Id)).Returns(10);
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogAddEarlyOrLateBid)));
        }

        /// <summary>
        ///     Test for adding a valid bid but user bid on his own product.
        /// </summary>
        [Test]
        public void ADD_ValidBid_BuyerIsAlsoSeller()
        {
            var sellerMock = new Mock<User>("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123");
            sellerMock.SetupGet(x => x.Id).Returns(3748);
            sellerMock.SetupGet(x => x.FirstName).Returns("Dinu");
            sellerMock.SetupGet(x => x.LastName).Returns("Garbuz");
            sellerMock.SetupGet(x => x.UserName).Returns("GbDinu");
            sellerMock.SetupGet(x => x.PhoneNumber).Returns("9876543210");
            sellerMock.SetupGet(x => x.Email).Returns("dinu.garbuz@FakeEmail.com");
            sellerMock.SetupGet(x => x.Password).Returns("P@rola123");

            var productMock = new Mock<Product>("Aparat foto CANNON", "face poze", new Category("Aparat foto", null), 100, ECurrency.EUR, new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"), DateTime.Today.AddDays(-5), DateTime.Today.AddDays(5));
            productMock.SetupGet(x => x.Id).Returns(1);
            productMock.SetupGet(x => x.Name).Returns("Aparat foto CANNON");
            productMock.SetupGet(x => x.Description).Returns("face poze");
            productMock.SetupGet(x => x.Category).Returns(new Category("Aparat foto", null));
            productMock.SetupGet(x => x.StartingPrice).Returns(100);
            productMock.SetupGet(x => x.Currency).Returns(ECurrency.EUR);
            productMock.SetupGet(x => x.Seller).Returns(sellerMock.Object);
            productMock.SetupGet(x => x.CreationDate).Returns(DateTime.Today.AddDays(-30));
            productMock.SetupGet(x => x.StartDate).Returns(DateTime.Today.AddDays(-5));
            productMock.SetupGet(x => x.EndDate).Returns(DateTime.Today.AddDays(5));
            productMock.SetupGet(x => x.TerminationDate).Returns(DateTime.Today.AddDays(5));

            var buyerMock = sellerMock;

            var bidMock = new Mock<Bid>(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            bidMock.SetupGet(x => x.Id).Returns(73);
            bidMock.SetupGet(x => x.DateAndTime).Returns(DateTime.Now);
            bidMock.SetupGet(x => x.Product).Returns(productMock.Object);
            bidMock.SetupGet(x => x.Buyer).Returns(buyerMock.Object);
            bidMock.SetupGet(x => x.Amount).Returns(1000);
            bidMock.SetupGet(x => x.Currency).Returns(ECurrency.EUR);

            Bid bid = bidMock.Object;

            var bidServiceMock = new Mock<IBidDataServices>();
            bidServiceMock.Setup(x => x.GetNoOfActiveBidsByUserId(bid.Buyer.Id)).Returns(5);
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            userScoreAndLimitsServiceMock.Setup(x => x.GetUserLimitByUserId(bid.Buyer.Id)).Returns(10);
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogAddSelfBid)));
        }

        /// <summary>
        ///     Test for adding a valid bid but with wrong currency.
        /// </summary>
        [Test]
        public void ADD_ValidBid_WrongCurrency()
        {
            var sellerMock = new Mock<User>("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123");
            sellerMock.SetupGet(x => x.Id).Returns(3748);
            sellerMock.SetupGet(x => x.FirstName).Returns("Dinu");
            sellerMock.SetupGet(x => x.LastName).Returns("Garbuz");
            sellerMock.SetupGet(x => x.UserName).Returns("GbDinu");
            sellerMock.SetupGet(x => x.PhoneNumber).Returns("9876543210");
            sellerMock.SetupGet(x => x.Email).Returns("dinu.garbuz@FakeEmail.com");
            sellerMock.SetupGet(x => x.Password).Returns("P@rola123");

            var productMock = new Mock<Product>("Aparat foto CANNON", "face poze", new Category("Aparat foto", null), 100, ECurrency.EUR, new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"), DateTime.Today.AddDays(-5), DateTime.Today.AddDays(5));
            productMock.SetupGet(x => x.Id).Returns(1);
            productMock.SetupGet(x => x.Name).Returns("Aparat foto CANNON");
            productMock.SetupGet(x => x.Description).Returns("face poze");
            productMock.SetupGet(x => x.Category).Returns(new Category("Aparat foto", null));
            productMock.SetupGet(x => x.StartingPrice).Returns(100);
            productMock.SetupGet(x => x.Currency).Returns(ECurrency.EUR);
            productMock.SetupGet(x => x.Seller).Returns(sellerMock.Object);
            productMock.SetupGet(x => x.CreationDate).Returns(DateTime.Today.AddDays(-30));
            productMock.SetupGet(x => x.StartDate).Returns(DateTime.Today.AddDays(-5));
            productMock.SetupGet(x => x.EndDate).Returns(DateTime.Today.AddDays(5));
            productMock.SetupGet(x => x.TerminationDate).Returns(DateTime.Today.AddDays(5));

            var buyerMock = new Mock<User>("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");
            buyerMock.SetupGet(x => x.Id).Returns(2594);
            buyerMock.SetupGet(x => x.FirstName).Returns("Adrian");
            buyerMock.SetupGet(x => x.LastName).Returns("Matei");
            buyerMock.SetupGet(x => x.UserName).Returns("AdiMatei20");
            buyerMock.SetupGet(x => x.PhoneNumber).Returns("0123456789");
            buyerMock.SetupGet(x => x.Email).Returns("adrian.matei@FakeEmail.com");
            buyerMock.SetupGet(x => x.Password).Returns("P@ssword123");

            var bidMock = new Mock<Bid>(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            bidMock.SetupGet(x => x.Id).Returns(73);
            bidMock.SetupGet(x => x.DateAndTime).Returns(DateTime.Now);
            bidMock.SetupGet(x => x.Product).Returns(productMock.Object);
            bidMock.SetupGet(x => x.Buyer).Returns(buyerMock.Object);
            bidMock.SetupGet(x => x.Amount).Returns(1000);
            bidMock.SetupGet(x => x.Currency).Returns(ECurrency.JPY);

            Bid bid = bidMock.Object;

            var bidServiceMock = new Mock<IBidDataServices>();
            bidServiceMock.Setup(x => x.GetNoOfActiveBidsByUserId(bid.Buyer.Id)).Returns(5);
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            userScoreAndLimitsServiceMock.Setup(x => x.GetUserLimitByUserId(bid.Buyer.Id)).Returns(10);
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogAddWrongCurrencyBid)));
        }

        /// <summary>
        ///     Test for adding a valid bid but with less than starting price.
        /// </summary>
        [Test]
        public void ADD_ValidBid_FirstBid_LessThanStartingPrice()
        {
            var sellerMock = new Mock<User>("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123");
            sellerMock.SetupGet(x => x.Id).Returns(3748);
            sellerMock.SetupGet(x => x.FirstName).Returns("Dinu");
            sellerMock.SetupGet(x => x.LastName).Returns("Garbuz");
            sellerMock.SetupGet(x => x.UserName).Returns("GbDinu");
            sellerMock.SetupGet(x => x.PhoneNumber).Returns("9876543210");
            sellerMock.SetupGet(x => x.Email).Returns("dinu.garbuz@FakeEmail.com");
            sellerMock.SetupGet(x => x.Password).Returns("P@rola123");

            var productMock = new Mock<Product>("Aparat foto CANNON", "face poze", new Category("Aparat foto", null), 100, ECurrency.EUR, new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"), DateTime.Today.AddDays(-5), DateTime.Today.AddDays(5));
            productMock.SetupGet(x => x.Id).Returns(1);
            productMock.SetupGet(x => x.Name).Returns("Aparat foto CANNON");
            productMock.SetupGet(x => x.Description).Returns("face poze");
            productMock.SetupGet(x => x.Category).Returns(new Category("Aparat foto", null));
            productMock.SetupGet(x => x.StartingPrice).Returns(100);
            productMock.SetupGet(x => x.Currency).Returns(ECurrency.EUR);
            productMock.SetupGet(x => x.Seller).Returns(sellerMock.Object);
            productMock.SetupGet(x => x.CreationDate).Returns(DateTime.Today.AddDays(-30));
            productMock.SetupGet(x => x.StartDate).Returns(DateTime.Today.AddDays(-5));
            productMock.SetupGet(x => x.EndDate).Returns(DateTime.Today.AddDays(5));
            productMock.SetupGet(x => x.TerminationDate).Returns(DateTime.Today.AddDays(5));

            var buyerMock = new Mock<User>("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");
            buyerMock.SetupGet(x => x.Id).Returns(2594);
            buyerMock.SetupGet(x => x.FirstName).Returns("Adrian");
            buyerMock.SetupGet(x => x.LastName).Returns("Matei");
            buyerMock.SetupGet(x => x.UserName).Returns("AdiMatei20");
            buyerMock.SetupGet(x => x.PhoneNumber).Returns("0123456789");
            buyerMock.SetupGet(x => x.Email).Returns("adrian.matei@FakeEmail.com");
            buyerMock.SetupGet(x => x.Password).Returns("P@ssword123");

            var bidMock = new Mock<Bid>(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            bidMock.SetupGet(x => x.Id).Returns(73);
            bidMock.SetupGet(x => x.DateAndTime).Returns(DateTime.Now);
            bidMock.SetupGet(x => x.Product).Returns(productMock.Object);
            bidMock.SetupGet(x => x.Buyer).Returns(buyerMock.Object);
            bidMock.SetupGet(x => x.Amount).Returns(99);
            bidMock.SetupGet(x => x.Currency).Returns(ECurrency.EUR);

            Bid bid = bidMock.Object;

            var bidServiceMock = new Mock<IBidDataServices>();
            bidServiceMock.Setup(x => x.GetNoOfActiveBidsByUserId(bid.Buyer.Id)).Returns(5);
            bidServiceMock.Setup(x => x.GetBidsByProductId(bid.Product.Id)).Returns(new List<Bid>());
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            userScoreAndLimitsServiceMock.Setup(x => x.GetUserLimitByUserId(bid.Buyer.Id)).Returns(10);
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogAddNotEnoughOrUserIsAlreadyOnTopBid)));
        }

        /// <summary>
        ///     Test for adding a valid bid but with less than last bid.
        /// </summary>
        [Test]
        public void ADD_ValidBid_LessThanLastBid()
        {
            var sellerMock = new Mock<User>("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123");
            sellerMock.SetupGet(x => x.Id).Returns(3748);
            sellerMock.SetupGet(x => x.FirstName).Returns("Dinu");
            sellerMock.SetupGet(x => x.LastName).Returns("Garbuz");
            sellerMock.SetupGet(x => x.UserName).Returns("GbDinu");
            sellerMock.SetupGet(x => x.PhoneNumber).Returns("9876543210");
            sellerMock.SetupGet(x => x.Email).Returns("dinu.garbuz@FakeEmail.com");
            sellerMock.SetupGet(x => x.Password).Returns("P@rola123");

            var productMock = new Mock<Product>("Aparat foto CANNON", "face poze", new Category("Aparat foto", null), 100, ECurrency.EUR, new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"), DateTime.Today.AddDays(-5), DateTime.Today.AddDays(5));
            productMock.SetupGet(x => x.Id).Returns(1);
            productMock.SetupGet(x => x.Name).Returns("Aparat foto CANNON");
            productMock.SetupGet(x => x.Description).Returns("face poze");
            productMock.SetupGet(x => x.Category).Returns(new Category("Aparat foto", null));
            productMock.SetupGet(x => x.StartingPrice).Returns(100);
            productMock.SetupGet(x => x.Currency).Returns(ECurrency.EUR);
            productMock.SetupGet(x => x.Seller).Returns(sellerMock.Object);
            productMock.SetupGet(x => x.CreationDate).Returns(DateTime.Today.AddDays(-30));
            productMock.SetupGet(x => x.StartDate).Returns(DateTime.Today.AddDays(-5));
            productMock.SetupGet(x => x.EndDate).Returns(DateTime.Today.AddDays(5));
            productMock.SetupGet(x => x.TerminationDate).Returns(DateTime.Today.AddDays(5));

            var buyer1Mock = new Mock<User>("Andrei", "Costache", "Costica", "8888888888", "andrei.costrache@FakeEmail.com", "@AbCd123");
            buyer1Mock.SetupGet(x => x.Id).Returns(3);
            buyer1Mock.SetupGet(x => x.FirstName).Returns("Andrei");
            buyer1Mock.SetupGet(x => x.LastName).Returns("Costache");
            buyer1Mock.SetupGet(x => x.UserName).Returns("Costica");
            buyer1Mock.SetupGet(x => x.PhoneNumber).Returns("8888888888");
            buyer1Mock.SetupGet(x => x.Email).Returns("andrei.costrache@FakeEmail.com");
            buyer1Mock.SetupGet(x => x.Password).Returns("@AbCd123");

            var buyer2Mock = new Mock<User>("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");
            buyer2Mock.SetupGet(x => x.Id).Returns(2594);
            buyer2Mock.SetupGet(x => x.FirstName).Returns("Adrian");
            buyer2Mock.SetupGet(x => x.LastName).Returns("Matei");
            buyer2Mock.SetupGet(x => x.UserName).Returns("AdiMatei20");
            buyer2Mock.SetupGet(x => x.PhoneNumber).Returns("0123456789");
            buyer2Mock.SetupGet(x => x.Email).Returns("adrian.matei@FakeEmail.com");
            buyer2Mock.SetupGet(x => x.Password).Returns("P@ssword123");

            var bid1Mock = new Mock<Bid>(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Andrei", "Costache", "Costica", "8888888888", "andrei.costrache@FakeEmail.com", "@AbCd123"),
                150,
                ECurrency.EUR);

            bid1Mock.SetupGet(x => x.Id).Returns(73);
            bid1Mock.SetupGet(x => x.DateAndTime).Returns(DateTime.Now);
            bid1Mock.SetupGet(x => x.Product).Returns(productMock.Object);
            bid1Mock.SetupGet(x => x.Buyer).Returns(buyer1Mock.Object);
            bid1Mock.SetupGet(x => x.Amount).Returns(150);
            bid1Mock.SetupGet(x => x.Currency).Returns(ECurrency.EUR);

            var bid2Mock = new Mock<Bid>(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                149,
                ECurrency.EUR);

            bid2Mock.SetupGet(x => x.Id).Returns(74);
            bid2Mock.SetupGet(x => x.DateAndTime).Returns(DateTime.Now);
            bid2Mock.SetupGet(x => x.Product).Returns(productMock.Object);
            bid2Mock.SetupGet(x => x.Buyer).Returns(buyer2Mock.Object);
            bid2Mock.SetupGet(x => x.Amount).Returns(149);
            bid2Mock.SetupGet(x => x.Currency).Returns(ECurrency.EUR);

            Bid bid1 = bid1Mock.Object;
            Bid bid2 = bid2Mock.Object;

            var bidServiceMock = new Mock<IBidDataServices>();
            bidServiceMock.Setup(x => x.GetNoOfActiveBidsByUserId(bid2.Buyer.Id)).Returns(5);
            bidServiceMock.Setup(x => x.GetBidsByProductId(bid2.Product.Id)).Returns(new List<Bid>() { bid1 });
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            userScoreAndLimitsServiceMock.Setup(x => x.GetUserLimitByUserId(bid2.Buyer.Id)).Returns(10);
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid2));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogAddNotEnoughOrUserIsAlreadyOnTopBid)));
        }

        /// <summary>
        ///     Test for adding a valid bid but user tried out-bidding himself.
        /// </summary>
        [Test]
        public void ADD_ValidBid_UserIsLastBidder()
        {
            var sellerMock = new Mock<User>("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123");
            sellerMock.SetupGet(x => x.Id).Returns(3748);
            sellerMock.SetupGet(x => x.FirstName).Returns("Dinu");
            sellerMock.SetupGet(x => x.LastName).Returns("Garbuz");
            sellerMock.SetupGet(x => x.UserName).Returns("GbDinu");
            sellerMock.SetupGet(x => x.PhoneNumber).Returns("9876543210");
            sellerMock.SetupGet(x => x.Email).Returns("dinu.garbuz@FakeEmail.com");
            sellerMock.SetupGet(x => x.Password).Returns("P@rola123");

            var productMock = new Mock<Product>("Aparat foto CANNON", "face poze", new Category("Aparat foto", null), 100, ECurrency.EUR, new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"), DateTime.Today.AddDays(-5), DateTime.Today.AddDays(5));
            productMock.SetupGet(x => x.Id).Returns(1);
            productMock.SetupGet(x => x.Name).Returns("Aparat foto CANNON");
            productMock.SetupGet(x => x.Description).Returns("face poze");
            productMock.SetupGet(x => x.Category).Returns(new Category("Aparat foto", null));
            productMock.SetupGet(x => x.StartingPrice).Returns(100);
            productMock.SetupGet(x => x.Currency).Returns(ECurrency.EUR);
            productMock.SetupGet(x => x.Seller).Returns(sellerMock.Object);
            productMock.SetupGet(x => x.CreationDate).Returns(DateTime.Today.AddDays(-30));
            productMock.SetupGet(x => x.StartDate).Returns(DateTime.Today.AddDays(-5));
            productMock.SetupGet(x => x.EndDate).Returns(DateTime.Today.AddDays(5));
            productMock.SetupGet(x => x.TerminationDate).Returns(DateTime.Today.AddDays(5));

            var buyer1Mock = new Mock<User>("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");
            buyer1Mock.SetupGet(x => x.Id).Returns(2594);
            buyer1Mock.SetupGet(x => x.FirstName).Returns("Adrian");
            buyer1Mock.SetupGet(x => x.LastName).Returns("Matei");
            buyer1Mock.SetupGet(x => x.UserName).Returns("AdiMatei20");
            buyer1Mock.SetupGet(x => x.PhoneNumber).Returns("0123456789");
            buyer1Mock.SetupGet(x => x.Email).Returns("adrian.matei@FakeEmail.com");
            buyer1Mock.SetupGet(x => x.Password).Returns("P@ssword123");
            var buyer2Mock = buyer1Mock;

            var bid1Mock = new Mock<Bid>(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1000,
                ECurrency.EUR);

            bid1Mock.SetupGet(x => x.Id).Returns(73);
            bid1Mock.SetupGet(x => x.DateAndTime).Returns(DateTime.Now);
            bid1Mock.SetupGet(x => x.Product).Returns(productMock.Object);
            bid1Mock.SetupGet(x => x.Buyer).Returns(buyer1Mock.Object);
            bid1Mock.SetupGet(x => x.Amount).Returns(1000);
            bid1Mock.SetupGet(x => x.Currency).Returns(ECurrency.EUR);

            var bid2Mock = new Mock<Bid>(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                1500,
                ECurrency.EUR);

            bid2Mock.SetupGet(x => x.Id).Returns(74);
            bid2Mock.SetupGet(x => x.DateAndTime).Returns(DateTime.Now);
            bid2Mock.SetupGet(x => x.Product).Returns(productMock.Object);
            bid2Mock.SetupGet(x => x.Buyer).Returns(buyer1Mock.Object);
            bid2Mock.SetupGet(x => x.Amount).Returns(1500);
            bid2Mock.SetupGet(x => x.Currency).Returns(ECurrency.EUR);

            Bid bid1 = bid1Mock.Object;
            Bid bid2 = bid2Mock.Object;

            var bidServiceMock = new Mock<IBidDataServices>();
            bidServiceMock.Setup(x => x.GetNoOfActiveBidsByUserId(bid2.Buyer.Id)).Returns(5);
            bidServiceMock.Setup(x => x.GetBidsByProductId(bid2.Product.Id)).Returns(new List<Bid>() { bid1 });
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            userScoreAndLimitsServiceMock.Setup(x => x.GetUserLimitByUserId(bid2.Buyer.Id)).Returns(10);
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bidServices.AddBid(bid2));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogAddNotEnoughOrUserIsAlreadyOnTopBid)));
        }

        /// <summary>
        ///     Test for adding a valid bid (first ever bid).
        /// </summary>
        [Test]
        public void ADD_ValidBid_FirstBid()
        {
            var sellerMock = new Mock<User>("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123");
            sellerMock.SetupGet(x => x.Id).Returns(3748);
            sellerMock.SetupGet(x => x.FirstName).Returns("Dinu");
            sellerMock.SetupGet(x => x.LastName).Returns("Garbuz");
            sellerMock.SetupGet(x => x.UserName).Returns("GbDinu");
            sellerMock.SetupGet(x => x.PhoneNumber).Returns("9876543210");
            sellerMock.SetupGet(x => x.Email).Returns("dinu.garbuz@FakeEmail.com");
            sellerMock.SetupGet(x => x.Password).Returns("P@rola123");

            var productMock = new Mock<Product>("Aparat foto CANNON", "face poze", new Category("Aparat foto", null), 100, ECurrency.EUR, new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"), DateTime.Today.AddDays(-5), DateTime.Today.AddDays(5));
            productMock.SetupGet(x => x.Id).Returns(1);
            productMock.SetupGet(x => x.Name).Returns("Aparat foto CANNON");
            productMock.SetupGet(x => x.Description).Returns("face poze");
            productMock.SetupGet(x => x.Category).Returns(new Category("Aparat foto", null));
            productMock.SetupGet(x => x.StartingPrice).Returns(100);
            productMock.SetupGet(x => x.Currency).Returns(ECurrency.EUR);
            productMock.SetupGet(x => x.Seller).Returns(sellerMock.Object);
            productMock.SetupGet(x => x.CreationDate).Returns(DateTime.Today.AddDays(-30));
            productMock.SetupGet(x => x.StartDate).Returns(DateTime.Today.AddDays(-5));
            productMock.SetupGet(x => x.EndDate).Returns(DateTime.Today.AddDays(5));
            productMock.SetupGet(x => x.TerminationDate).Returns(DateTime.Today.AddDays(5));

            var buyerMock = new Mock<User>("Andrei", "Costache", "Costica", "8888888888", "andrei.costrache@FakeEmail.com", "@AbCd123");
            buyerMock.SetupGet(x => x.Id).Returns(3);
            buyerMock.SetupGet(x => x.FirstName).Returns("Andrei");
            buyerMock.SetupGet(x => x.LastName).Returns("Costache");
            buyerMock.SetupGet(x => x.UserName).Returns("Costica");
            buyerMock.SetupGet(x => x.PhoneNumber).Returns("8888888888");
            buyerMock.SetupGet(x => x.Email).Returns("andrei.costrache@FakeEmail.com");
            buyerMock.SetupGet(x => x.Password).Returns("@AbCd123");

            var bid1Mock = new Mock<Bid>(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Andrei", "Costache", "Costica", "8888888888", "andrei.costrache@FakeEmail.com", "@AbCd123"),
                150,
                ECurrency.EUR);

            bid1Mock.SetupGet(x => x.Id).Returns(73);
            bid1Mock.SetupGet(x => x.DateAndTime).Returns(DateTime.Now);
            bid1Mock.SetupGet(x => x.Product).Returns(productMock.Object);
            bid1Mock.SetupGet(x => x.Buyer).Returns(buyerMock.Object);
            bid1Mock.SetupGet(x => x.Amount).Returns(150);
            bid1Mock.SetupGet(x => x.Currency).Returns(ECurrency.EUR);

            Bid bid = bid1Mock.Object;

            var bidServiceMock = new Mock<IBidDataServices>();
            bidServiceMock.Setup(x => x.GetNoOfActiveBidsByUserId(bid.Buyer.Id)).Returns(5);
            bidServiceMock.Setup(x => x.GetBidsByProductId(bid.Product.Id)).Returns(new List<Bid>());
            bidServiceMock.Setup(x => x.AddBid(bid)).Returns(true);
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            userScoreAndLimitsServiceMock.Setup(x => x.GetUserLimitByUserId(bid.Buyer.Id)).Returns(10);
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsTrue(bidServices.AddBid(bid));
        }

        /// <summary>
        ///     Test for adding a valid bid.
        /// </summary>
        [Test]
        public void ADD_ValidBid()
        {
            var sellerMock = new Mock<User>("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123");
            sellerMock.SetupGet(x => x.Id).Returns(3748);
            sellerMock.SetupGet(x => x.FirstName).Returns("Dinu");
            sellerMock.SetupGet(x => x.LastName).Returns("Garbuz");
            sellerMock.SetupGet(x => x.UserName).Returns("GbDinu");
            sellerMock.SetupGet(x => x.PhoneNumber).Returns("9876543210");
            sellerMock.SetupGet(x => x.Email).Returns("dinu.garbuz@FakeEmail.com");
            sellerMock.SetupGet(x => x.Password).Returns("P@rola123");

            var productMock = new Mock<Product>("Aparat foto CANNON", "face poze", new Category("Aparat foto", null), 100, ECurrency.EUR, new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"), DateTime.Today.AddDays(-5), DateTime.Today.AddDays(5));
            productMock.SetupGet(x => x.Id).Returns(1);
            productMock.SetupGet(x => x.Name).Returns("Aparat foto CANNON");
            productMock.SetupGet(x => x.Description).Returns("face poze");
            productMock.SetupGet(x => x.Category).Returns(new Category("Aparat foto", null));
            productMock.SetupGet(x => x.StartingPrice).Returns(100);
            productMock.SetupGet(x => x.Currency).Returns(ECurrency.EUR);
            productMock.SetupGet(x => x.Seller).Returns(sellerMock.Object);
            productMock.SetupGet(x => x.CreationDate).Returns(DateTime.Today.AddDays(-30));
            productMock.SetupGet(x => x.StartDate).Returns(DateTime.Today.AddDays(-5));
            productMock.SetupGet(x => x.EndDate).Returns(DateTime.Today.AddDays(5));
            productMock.SetupGet(x => x.TerminationDate).Returns(DateTime.Today.AddDays(5));

            var buyer1Mock = new Mock<User>("Andrei", "Costache", "Costica", "8888888888", "andrei.costrache@FakeEmail.com", "@AbCd123");
            buyer1Mock.SetupGet(x => x.Id).Returns(3);
            buyer1Mock.SetupGet(x => x.FirstName).Returns("Andrei");
            buyer1Mock.SetupGet(x => x.LastName).Returns("Costache");
            buyer1Mock.SetupGet(x => x.UserName).Returns("Costica");
            buyer1Mock.SetupGet(x => x.PhoneNumber).Returns("8888888888");
            buyer1Mock.SetupGet(x => x.Email).Returns("andrei.costrache@FakeEmail.com");
            buyer1Mock.SetupGet(x => x.Password).Returns("@AbCd123");

            var buyer2Mock = new Mock<User>("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");
            buyer2Mock.SetupGet(x => x.Id).Returns(2594);
            buyer2Mock.SetupGet(x => x.FirstName).Returns("Adrian");
            buyer2Mock.SetupGet(x => x.LastName).Returns("Matei");
            buyer2Mock.SetupGet(x => x.UserName).Returns("AdiMatei20");
            buyer2Mock.SetupGet(x => x.PhoneNumber).Returns("0123456789");
            buyer2Mock.SetupGet(x => x.Email).Returns("adrian.matei@FakeEmail.com");
            buyer2Mock.SetupGet(x => x.Password).Returns("P@ssword123");

            var bid1Mock = new Mock<Bid>(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Andrei", "Costache", "Costica", "8888888888", "andrei.costrache@FakeEmail.com", "@AbCd123"),
                150,
                ECurrency.EUR);

            bid1Mock.SetupGet(x => x.Id).Returns(73);
            bid1Mock.SetupGet(x => x.DateAndTime).Returns(DateTime.Now);
            bid1Mock.SetupGet(x => x.Product).Returns(productMock.Object);
            bid1Mock.SetupGet(x => x.Buyer).Returns(buyer1Mock.Object);
            bid1Mock.SetupGet(x => x.Amount).Returns(150);
            bid1Mock.SetupGet(x => x.Currency).Returns(ECurrency.EUR);

            var bid2Mock = new Mock<Bid>(
                new Product(
                    "Aparat foto CANNON",
                    "face poze",
                    new Category("Aparat foto", null),
                    100,
                    ECurrency.EUR,
                    new User("Dinu", "Garbuz", "GbDinu", "9876543210", "dinu.garbuz@FakeEmail.com", "P@rola123"),
                    DateTime.Today.AddDays(-5),
                    DateTime.Today.AddDays(5)),
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                200,
                ECurrency.EUR);

            bid2Mock.SetupGet(x => x.Id).Returns(74);
            bid2Mock.SetupGet(x => x.DateAndTime).Returns(DateTime.Now);
            bid2Mock.SetupGet(x => x.Product).Returns(productMock.Object);
            bid2Mock.SetupGet(x => x.Buyer).Returns(buyer2Mock.Object);
            bid2Mock.SetupGet(x => x.Amount).Returns(200);
            bid2Mock.SetupGet(x => x.Currency).Returns(ECurrency.EUR);

            Bid bid1 = bid1Mock.Object;
            Bid bid2 = bid2Mock.Object;

            var bidServiceMock = new Mock<IBidDataServices>();
            bidServiceMock.Setup(x => x.GetNoOfActiveBidsByUserId(bid2.Buyer.Id)).Returns(5);
            bidServiceMock.Setup(x => x.GetBidsByProductId(bid2.Product.Id)).Returns(new List<Bid>() { bid1 });
            bidServiceMock.Setup(x => x.AddBid(bid2)).Returns(true);
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            userScoreAndLimitsServiceMock.Setup(x => x.GetUserLimitByUserId(bid2.Buyer.Id)).Returns(10);
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsTrue(bidServices.AddBid(bid2));
        }
    }
}
