// <copyright file="DeleteUserTests.cs" company="Transilvania University of Brasov">
// Matei Adrian
// </copyright>

namespace UserServiceTests
{
    using System.Diagnostics.CodeAnalysis;
    using DataMapper.Interfaces;
    using DomainModel.Models;
    using log4net;
    using Moq;
    using NUnit.Framework;
    using ServiceLayer.Implementation;

    /// <summary>
    ///     Test class for <see cref="UserServicesImplementation.DeleteUser(User)"/> method.
    /// </summary>
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal class DeleteUserTests
    {
        /// <summary>Null user log message.</summary>
        private const string LogDeleteNullUser = "Attempted to delete a null user.";

        /// <summary>Existing user log message.</summary>
        private const string LogDeleteNonexistingUser = "Attempted to delete a nonexisting user.";

        /// <summary>
        ///     Test for deleting a null user.
        /// </summary>
        [Test]
        public void DELETE_NullUser()
        {
            User user = null;

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.DeleteUser(user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogDeleteNullUser)));
        }

        /// <summary>
        ///     Test for deleting a non-existing user.
        /// </summary>
        [Test]
        public void DELETE_ValidUser_NonExistingUser()
        {
            User user = new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");
            User nullUser = null;

            var userServiceMock = new Mock<IUserDataServices>();
            userServiceMock.Setup(x => x.GetUserById(user.Id)).Returns(nullUser);
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.DeleteUser(user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogDeleteNonexistingUser)));
        }

        /// <summary>
        ///     Test for deleting a user.
        /// </summary>
        [Test]
        public void DELETE_ValidUser()
        {
            User user = new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");

            var userServiceMock = new Mock<IUserDataServices>();
            userServiceMock.Setup(x => x.GetUserById(user.Id)).Returns(user);
            userServiceMock.Setup(x => x.DeleteUser(user)).Returns(true);
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsTrue(userServices.DeleteUser(user));
        }
    }
}
