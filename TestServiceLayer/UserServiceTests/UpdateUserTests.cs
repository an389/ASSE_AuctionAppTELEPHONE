// <copyright file="UpdateUserTests.cs" company="Transilvania University of Brasov">
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
    ///     Test class for <see cref="UserServicesImplementation.UpdateUser(User)"/> method.
    /// </summary>
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal class UpdateUserTests
    {
        /// <summary>Null user log message.</summary>
        private const string LogUpdateNullUser = "Attempted to update a null user.";

        /// <summary>Invalid user log message.</summary>
        private const string LogUpdateInvalidUser = "Attempted to update an invalid user.";

        /// <summary>Non-existing user log message.</summary>
        private const string LogUpdateNonexistingUser = "Attempted to update a nonexisting user.";

        /// <summary>
        ///     Test for updating a null user.
        /// </summary>
        [Test]
        public void UPDATE_NullUser()
        {
            User user = null;

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogUpdateNullUser)));
        }

        /// <summary>
        ///     Test for updating an invalid user (a user with null first name).
        /// </summary>
        [Test]
        public void UPDATE_InvalidUser_FirstName_Null()
        {
            User user = new User(null, "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidUser))));
        }

        /// <summary>
        ///     Test for updating an invalid user (a user with empty first name).
        /// </summary>
        [Test]
        public void UPDATE_InvalidUser_FirstName_Empty()
        {
            User user = new User(string.Empty, "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidUser))));
        }

        /// <summary>
        ///     Test for updating an invalid user (a user with first name too long).
        /// </summary>
        [Test]
        public void UPDATE_InvalidUser_FirstName_TooLong()
        {
            User user = new User(new string('x', 20), "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidUser))));
        }

        /// <summary>
        ///     Test for updating an invalid user (a user with first name that doesn't start with an uppercase letter).
        /// </summary>
        [Test]
        public void UPDATE_InvalidUser_FirstName_NoUpperCaseLetter()
        {
            User user = new User(new string('x', 10), "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidUser))));
        }

        /// <summary>
        ///     Test for updating an invalid user (a user with first name that only has uppercase letters).
        /// </summary>
        [Test]
        public void UPDATE_InvalidUser_FirstName_NoLowerCaseLetters()
        {
            User user = new User(new string('X', 10), "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidUser))));
        }

        /// <summary>
        ///     Test for updating an invalid user (a user with first name that contains symbols).
        /// </summary>
        [Test]
        public void UPDATE_InvalidUser_FirstName_ContainsSymbol()
        {
            User user = new User("Adr!an", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidUser))));
        }

        /// <summary>
        ///     Test for updating an invalid user (a user with first name that contains numbers).
        /// </summary>
        [Test]
        public void UPDATE_InvalidUser_FirstName_ContainsNumber()
        {
            User user = new User("Adr1an", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidUser))));
        }

        /// <summary>
        ///     Test for updating an invalid user (a user with null last name).
        /// </summary>
        [Test]
        public void UPDATE_InvalidUser_LastName_Null()
        {
            User user = new User("Adrian", null, "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidUser))));
        }

        /// <summary>
        ///     Test for updating an invalid user (a user with empty last name).
        /// </summary>
        [Test]
        public void UPDATE_InvalidUser_LastName_Empty()
        {
            User user = new User("Adrian", string.Empty, "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidUser))));
        }

        /// <summary>
        ///     Test for updating an invalid user (a user with last name too long).
        /// </summary>
        [Test]
        public void UPDATE_InvalidUser_LastName_TooLong()
        {
            User user = new User("Adrian", new string('x', 20), "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidUser))));
        }

        /// <summary>
        ///     Test for updating an invalid user (a user with last name that doesn't start with an uppercase letter).
        /// </summary>
        [Test]
        public void UPDATE_InvalidUser_LastName_NoUpperCaseLetter()
        {
            User user = new User("Adrian", new string('x', 10), "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidUser))));
        }

        /// <summary>
        ///     Test for updating an invalid user (a user with last name that only has uppercase letters).
        /// </summary>
        [Test]
        public void UPDATE_InvalidUser_LastName_NoLowerCaseLetters()
        {
            User user = new User("Adrian", new string('X', 10), "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidUser))));
        }

        /// <summary>
        ///     Test for updating an invalid user (a user with last name that contains symbols).
        /// </summary>
        [Test]
        public void UPDATE_InvalidUser_LastName_ContainsSymbol()
        {
            User user = new User("Adrian", "Mate!", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidUser))));
        }

        /// <summary>
        ///     Test for updating an invalid user (a user with last name that contains numbers).
        /// </summary>
        [Test]
        public void UPDATE_InvalidUser_LastName_ContainsNumber()
        {
            User user = new User("Adrian", "Mat3i", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidUser))));
        }

        /// <summary>
        ///     Test for updating an invalid user (a user with null username).
        /// </summary>
        [Test]
        public void UPDATE_InvalidUser_UserName_Null()
        {
            User user = new User("Adrian", "Matei", null, "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidUser))));
        }

        /// <summary>
        ///     Test for updating an invalid user (a user with empty username).
        /// </summary>
        [Test]
        public void UPDATE_InvalidUser_UserName_Empty()
        {
            User user = new User("Adrian", "Matei", string.Empty, "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidUser))));
        }

        /// <summary>
        ///     Test for updating an invalid user (a user with username too long).
        /// </summary>
        [Test]
        public void UPDATE_InvalidUser_UserName_TooLong()
        {
            User user = new User("Adrian", "Matei", new string('x', 50), "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidUser))));
        }

        /// <summary>
        ///     Test for updating a valid user (a user with null phone number).
        /// </summary>
        [Test]
        public void UPDATE_ValidUser_PhoneNumber_Null()
        {
            User user = new User("Adrian", "Matei", "AdiMatei20", null, "adrian.matei@FakeEmail.com", "P@ssword123");

            var userServiceMock = new Mock<IUserDataServices>();
            userServiceMock.Setup(x => x.GetUserById(user.Id)).Returns(user);
            userServiceMock.Setup(x => x.UpdateUser(user)).Returns(true);
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsTrue(userServices.UpdateUser(user));
        }

        /// <summary>
        ///     Test for updating an invalid user (a user with empty phone number).
        /// </summary>
        [Test]
        public void UPDATE_InvalidUser_PhoneNumber_Empty()
        {
            User user = new User("Adrian", "Matei", "AdiMatei20", string.Empty, "adrian.matei@FakeEmail.com", "P@ssword123");

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidUser))));
        }

        /// <summary>
        ///     Test for updating an invalid user (a user with phone number too long).
        /// </summary>
        [Test]
        public void UPDATE_InvalidUser_PhoneNumber_TooLong()
        {
            User user = new User("Adrian", "Matei", "AdiMatei20", new string('x', 20), "adrian.matei@FakeEmail.com", "P@ssword123");

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidUser))));
        }

        /// <summary>
        ///     Test for updating an invalid user (a user with phone number invalid).
        /// </summary>
        [Test]
        public void UPDATE_InvalidUser_PhoneNumber_Invalid()
        {
            User user = new User("Adrian", "Matei", "AdiMatei20", "abcde", "adrian.matei@FakeEmail.com", "P@ssword123");

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidUser))));
        }

        /// <summary>
        ///     Test for updating an invalid user (a user with null email address).
        /// </summary>
        [Test]
        public void UPDATE_InvalidUser_Email_Null()
        {
            User user = new User("Adrian", "Matei", "AdiMatei20", "0123456789", null, "P@ssword123");

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidUser))));
        }

        /// <summary>
        ///     Test for updating an invalid user (a user with empty email address).
        /// </summary>
        [Test]
        public void UPDATE_InvalidUser_Email_Empty()
        {
            User user = new User("Adrian", "Matei", "AdiMatei20", "0123456789", string.Empty, "P@ssword123");

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidUser))));
        }

        /// <summary>
        ///     Test for updating an invalid user (a user with email address too long).
        /// </summary>
        [Test]
        public void UPDATE_InvalidUser_Email_TooLong()
        {
            User user = new User("Adrian", "Matei", "AdiMatei20", "0123456789", new string('x', 60), "P@ssword123");

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidUser))));
        }

        /// <summary>
        ///     Test for updating an invalid user (a user with invalid email address).
        /// </summary>
        [Test]
        public void UPDATE_InvalidUser_Email_Invalid()
        {
            User user = new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.mateiFakeEmail.com", "P@ssword123");

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidUser))));
        }

        /// <summary>
        ///     Test for updating an invalid user (a user with null password).
        /// </summary>
        [Test]
        public void UPDATE_InvalidUser_Password_Null()
        {
            User user = new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", null);

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidUser))));
        }

        /// <summary>
        ///     Test for updating an invalid user (a user with empty password).
        /// </summary>
        [Test]
        public void UPDATE_InvalidUser_Password_Empty()
        {
            User user = new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", string.Empty);

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidUser))));
        }

        /// <summary>
        ///     Test for updating an invalid user (a user with password too short).
        /// </summary>
        [Test]
        public void UPDATE_InvalidUser_Password_TooShort()
        {
            User user = new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "A#a1");

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidUser))));
        }

        /// <summary>
        ///     Test for updating an invalid user (a user with password too long).
        /// </summary>
        [Test]
        public void UPDATE_InvalidUser_Password_TooLong()
        {
            User user = new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "A#a1" + new string('x', 20));

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidUser))));
        }

        /// <summary>
        ///     Test for updating an invalid user (a user with password that doesn't contain uppercase letters).
        /// </summary>
        [Test]
        public void UPDATE_InvalidUser_Password_MissingUppercaseLetter()
        {
            User user = new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "p@ssword123");

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidUser))));
        }

        /// <summary>
        ///     Test for updating an invalid user (a user with password that doesn't contain lowercase letters).
        /// </summary>
        [Test]
        public void UPDATE_InvalidUser_Password_MissingLowercaseLetter()
        {
            User user = new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@SSWORD123");

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidUser))));
        }

        /// <summary>
        ///     Test for updating an invalid user (a user with password that doesn't contain numbers).
        /// </summary>
        [Test]
        public void UPDATE_InvalidUser_Password_MissingNumber()
        {
            User user = new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword");

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidUser))));
        }

        /// <summary>
        ///     Test for updating an invalid user (a user with password that doesn't contain symbols).
        /// </summary>
        [Test]
        public void UPDATE_InvalidUser_Password_MissingSymbol()
        {
            User user = new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "Password123");

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidUser))));
        }

        /// <summary>
        ///     Test for updating a non-existing user.
        /// </summary>
        [Test]
        public void UPDATE_NonExistingUser()
        {
            User user = new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");
            User nullUser = null;

            var userServiceMock = new Mock<IUserDataServices>();
            userServiceMock.Setup(x => x.GetUserById(user.Id)).Returns(nullUser);
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogUpdateNonexistingUser)));
        }

        /// <summary>
        ///     Test for updating a valid user.
        /// </summary>
        [Test]
        public void UPDATE_ValidUser()
        {
            User user = new User("Adrian", "Matei", "AdiMatei20", "0123456789", "adrian.matei@FakeEmail.com", "P@ssword123");

            var userServiceMock = new Mock<IUserDataServices>();
            userServiceMock.Setup(x => x.GetUserById(user.Id)).Returns(user);
            userServiceMock.Setup(x => x.UpdateUser(user)).Returns(true);
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsTrue(userServices.UpdateUser(user));
        }
    }
}
