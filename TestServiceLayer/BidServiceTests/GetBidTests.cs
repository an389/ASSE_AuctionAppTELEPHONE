// <copyright file="GetBidTests.cs" company="Transilvania University of Brasov">
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
    ///     Test class for
    ///         <see cref="BidServicesImplementation.GetAllBids()"/>,
    ///         <see cref="BidServicesImplementation.GetBidById(int)"/> and
    ///         <see cref="BidServicesImplementation.GetBidsByProductId(int)"/>
    ///     methods.
    /// </summary>
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal class GetBidTests
    {
        /// <summary>
        ///     Test for retrieving all existing bids.
        /// </summary>
        [Test]
        public void GET_AllBids()
        {
            IList<Bid> bids = GetSampleBids();

            var bidServiceMock = new Mock<IBidDataServices>();
            bidServiceMock.Setup(x => x.GetAllBids()).Returns(bids);
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            var expected = bids;
            var actual = bidServices.GetAllBids();

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

                Assert.AreEqual(expected[i].Buyer.Id, actual[i].Buyer.Id);
                Assert.AreEqual(expected[i].Buyer.FirstName, actual[i].Buyer.FirstName);
                Assert.AreEqual(expected[i].Buyer.LastName, actual[i].Buyer.LastName);
                Assert.AreEqual(expected[i].Buyer.UserName, actual[i].Buyer.UserName);
                Assert.AreEqual(expected[i].Buyer.PhoneNumber, actual[i].Buyer.PhoneNumber);
                Assert.AreEqual(expected[i].Buyer.Email, actual[i].Buyer.Email);
                Assert.AreEqual(expected[i].Buyer.Password, actual[i].Buyer.Password);
                Assert.AreEqual(expected[i].Buyer.AccountType, actual[i].Buyer.AccountType);

                Assert.AreEqual(expected[i].Amount, actual[i].Amount);
                Assert.AreEqual(expected[i].Currency, actual[i].Currency);
            }
        }

        /// <summary>
        ///     Test for retrieving all existing bids but none were found.
        /// </summary>
        [Test]
        public void GET_AllRatings_NoneFound()
        {
            List<Bid> emptyBidList = new List<Bid>();

            var bidServiceMock = new Mock<IBidDataServices>();
            bidServiceMock.Setup(x => x.GetAllBids()).Returns(emptyBidList);
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsEmpty(bidServices.GetAllBids());
        }

        /// <summary>
        ///     Test for retrieving an existing bid with the specified id.
        /// </summary>
        [Test]
        public void GET_BidById()
        {
            Bid bid = GetSampleBid();

            var bidServiceMock = new Mock<IBidDataServices>();
            bidServiceMock.Setup(x => x.GetBidById(bid.Id)).Returns(bid);
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            var expected = bid;
            var actual = bidServices.GetBidById(bid.Id);

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

            Assert.AreEqual(expected.Buyer.Id, actual.Buyer.Id);
            Assert.AreEqual(expected.Buyer.FirstName, actual.Buyer.FirstName);
            Assert.AreEqual(expected.Buyer.LastName, actual.Buyer.LastName);
            Assert.AreEqual(expected.Buyer.UserName, actual.Buyer.UserName);
            Assert.AreEqual(expected.Buyer.PhoneNumber, actual.Buyer.PhoneNumber);
            Assert.AreEqual(expected.Buyer.Email, actual.Buyer.Email);
            Assert.AreEqual(expected.Buyer.Password, actual.Buyer.Password);
            Assert.AreEqual(expected.Buyer.AccountType, actual.Buyer.AccountType);

            Assert.AreEqual(expected.Amount, actual.Amount);
            Assert.AreEqual(expected.Currency, actual.Currency);
        }

        /// <summary>
        ///     Test for retrieving an existing bid with the specified id but no such bid was found.
        /// </summary>
        [Test]
        public void GET_BidById_NotFound()
        {
            Bid bid = GetSampleBid();
            Bid nullBid = null;

            var bidServiceMock = new Mock<IBidDataServices>();
            bidServiceMock.Setup(x => x.GetBidById(bid.Id)).Returns(nullBid);
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsNull(bidServices.GetBidById(bid.Id));
        }

        /// <summary>
        ///     Test for retrieving all existing bids on the product with the specified id.
        /// </summary>
        [Test]
        public void GET_BidsByProductId()
        {
            Bid bid = GetSampleBid();
            IList<Bid> bids = GetSampleBids();

            var bidServiceMock = new Mock<IBidDataServices>();
            bidServiceMock.Setup(x => x.GetBidsByProductId(bid.Product.Id)).Returns(bids);
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            var expected = bids;
            var actual = bidServices.GetBidsByProductId(bid.Product.Id);

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

                Assert.AreEqual(expected[i].Buyer.Id, actual[i].Buyer.Id);
                Assert.AreEqual(expected[i].Buyer.FirstName, actual[i].Buyer.FirstName);
                Assert.AreEqual(expected[i].Buyer.LastName, actual[i].Buyer.LastName);
                Assert.AreEqual(expected[i].Buyer.UserName, actual[i].Buyer.UserName);
                Assert.AreEqual(expected[i].Buyer.PhoneNumber, actual[i].Buyer.PhoneNumber);
                Assert.AreEqual(expected[i].Buyer.Email, actual[i].Buyer.Email);
                Assert.AreEqual(expected[i].Buyer.Password, actual[i].Buyer.Password);
                Assert.AreEqual(expected[i].Buyer.AccountType, actual[i].Buyer.AccountType);

                Assert.AreEqual(expected[i].Amount, actual[i].Amount);
                Assert.AreEqual(expected[i].Currency, actual[i].Currency);
            }
        }

        /// <summary>
        ///     Test for retrieving all existing bids on the product with the specified id but no such bids were found.
        /// </summary>
        [Test]
        public void GET_BidsByProductId_NoneFound()
        {
            Bid bid = GetSampleBid();
            IList<Bid> emptyBidList = new List<Bid>();

            var bidServiceMock = new Mock<IBidDataServices>();
            bidServiceMock.Setup(x => x.GetBidsByProductId(bid.Product.Id)).Returns(emptyBidList);
            var userScoreAndLimitsServiceMock = new Mock<IUserScoreAndLimitsDataServices>();
            var loggerMock = new Mock<ILog>();

            var bidServices = new BidServicesImplementation(bidServiceMock.Object, userScoreAndLimitsServiceMock.Object, loggerMock.Object);

            Assert.IsEmpty(bidServices.GetBidsByProductId(bid.Product.Id));
        }

        /// <summary>Gets a sample bid.</summary>
        /// <returns>a sample bid.</returns>
        private static Bid GetSampleBid()
        {
            return new Bid(
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
        }

        /// <summary>Gets sample bids.</summary>
        /// <returns>a list of sample bids.</returns>
        private static List<Bid> GetSampleBids()
        {
            return new List<Bid>
            {
                new Bid(
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
                    1000,
                    ECurrency.EUR),
            };
        }
    }
}
