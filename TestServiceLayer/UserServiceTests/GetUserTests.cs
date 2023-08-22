// <copyright file="GetUserTests.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace UserServiceTests
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using DataMapper.Interfaces;
    using DomainModel.Models;
    using log4net;
    using Moq;
    using NUnit.Framework;
    using ServiceLayer.Implementation;

    /// <summary>
    ///     Test class for
    ///         <see cref="UserServicesImplementation.GetAllUsers()"/>,
    ///         <see cref="UserServicesImplementation.GetUserById(int)"/> and
    ///         <see cref="UserServicesImplementation.GetUserByEmailAndPassword(string, string)"/>
    ///     methods.
    /// </summary>
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal class GetUserTests
    {
        /// <summary>
        ///     Test for retrieving all existing users.
        /// </summary>
        [Test]
        public void GET_AllUsers()
        {
            List<User> users = GetSampleUsers();

            var userServiceMock = new Mock<IUserDataServices>();
            userServiceMock.Setup(x => x.GetAllUsers()).Returns(users);
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            var expected = users;
            var actual = userServices.GetAllUsers();

            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Id, actual[i].Id);
                Assert.AreEqual(expected[i].FirstName, actual[i].FirstName);
                Assert.AreEqual(expected[i].LastName, actual[i].LastName);
                Assert.AreEqual(expected[i].UserName, actual[i].UserName);
                Assert.AreEqual(expected[i].PhoneNumber, actual[i].PhoneNumber);
                Assert.AreEqual(expected[i].Email, actual[i].Email);
                Assert.AreEqual(expected[i].Password, actual[i].Password);
                Assert.AreEqual(expected[i].AccountType, actual[i].AccountType);
            }
        }

        /// <summary>
        ///     Test for retrieving all existing users but none were found.
        /// </summary>
        [Test]
        public void GET_AllUsers_NoneFound()
        {
            List<User> emptyUserList = new List<User>();

            var userServiceMock = new Mock<IUserDataServices>();
            userServiceMock.Setup(x => x.GetAllUsers()).Returns(emptyUserList);
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsEmpty(userServices.GetAllUsers());
        }

        /// <summary>
        ///     Test for retrieving an existing user with the specified id.
        /// </summary>
        [Test]
        public void GET_UserById()
        {
            User user = GetSampleUser();

            var userServiceMock = new Mock<IUserDataServices>();
            userServiceMock.Setup(x => x.GetUserById(user.Id)).Returns(user);
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            var expected = user;
            var actual = userServices.GetUserById(user.Id);

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
            Assert.AreEqual(expected.UserName, actual.UserName);
            Assert.AreEqual(expected.PhoneNumber, actual.PhoneNumber);
            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.Password, actual.Password);
            Assert.AreEqual(expected.AccountType, actual.AccountType);
        }

        /// <summary>
        ///     Test for retrieving an existing user with the specified id but no such user was found.
        /// </summary>
        [Test]
        public void GET_UserById_NotFound()
        {
            User user = GetSampleUser();
            User nullUser = null;

            var userServiceMock = new Mock<IUserDataServices>();
            userServiceMock.Setup(x => x.GetUserById(user.Id)).Returns(nullUser);
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsNull(userServices.GetUserById(user.Id));
        }

        /// <summary>
        ///     Test for retrieving an existing user with the specified email address and password.
        /// </summary>
        [Test]
        public void GET_UserByEmailAndPassword()
        {
            User user = GetSampleUser();

            var userServiceMock = new Mock<IUserDataServices>();
            userServiceMock.Setup(x => x.GetUserByEmailAndPassword(user.Email, user.Password)).Returns(user);
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            var expected = user;
            var actual = userServices.GetUserByEmailAndPassword(user.Email, user.Password);

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
            Assert.AreEqual(expected.UserName, actual.UserName);
            Assert.AreEqual(expected.PhoneNumber, actual.PhoneNumber);
            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.Password, actual.Password);
            Assert.AreEqual(expected.AccountType, actual.AccountType);
        }

        /// <summary>
        ///     Test for retrieving an existing user with the specified email address and password but no such user was found.
        /// </summary>
        [Test]
        public void GET_UserByEmailAndPassword_NotFound()
        {
            User user = GetSampleUser();
            User nullUser = null;

            var userServiceMock = new Mock<IUserDataServices>();
            userServiceMock.Setup(x => x.GetUserByEmailAndPassword(user.Email, user.Password)).Returns(nullUser);
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsNull(userServices.GetUserByEmailAndPassword(user.Email, user.Password));
        }

        /// <summary>Gets a sample user.</summary>
        /// <returns>a sample user.</returns>
        private static User GetSampleUser()
        {
            return new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");
        }

        /// <summary>Gets sample users.</summary>
        /// <returns>a list of sample users.</returns>
        private static List<User> GetSampleUsers()
        {
            return new List<User>
            {
                new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123"),
                new User("Dinu", "Garbuz", "GbDinu", null, "dinu.garbuz@FakeEmail.com", "P@rola123"),
                new User("Andrei", "Costache", "Costica", null, "andrei.costrache@FakeEmail.com", "@AbCd123"),
            };
        }
    }
}
