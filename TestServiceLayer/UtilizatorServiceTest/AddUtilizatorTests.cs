using DataMapper.Interfaces;
using DomainModel.Models;
using log4net;
using Moq;
using NUnit.Framework;
using ServiceLayer.Implementation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServiceLayer.UtilizatorServiceTest
{
    internal class AddUtilizatorTests
    {
        private const string LogAddNullUser = "Attempted to add a null utilizator.";

        private const string LogAddInvalidUser = "Attempted to add an invalid utilizator.";

        /// <summary>Existing user log message.</summary>
        private const string LogAddExistingUser = "Attempted to add an already existing utilizator.";
        [Test]
        public void ValidUser()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
;

            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();
            utilizatorServiceMock.Setup(x => x.EmailAlreadyExists(utilizator.Emali)).Returns(false);
            utilizatorServiceMock.Setup(x => x.AddUtilizator(utilizator)).Returns(true);
            var utilizatorServicesImplementation = new UtilizatorServicesImplementation(utilizatorServiceMock.Object, loggerMock.Object);

            Assert.IsTrue(utilizatorServicesImplementation.AddUtilizator(utilizator));
        }
        [Test]
        public void ADD_NullUser()
        {
            Utilizator utilizator = null;

            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var utilizatorServicesImplementation = new UtilizatorServicesImplementation(utilizatorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(utilizatorServicesImplementation.AddUtilizator(utilizator));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogAddNullUser)));
        }


        [Test]
        public void ADD_InvalidUser_FirstName_Null()
        {
            Utilizator utilizator = new Utilizator(null, "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
;
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var utilizatorServicesImplementation = new UtilizatorServicesImplementation(utilizatorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(utilizatorServicesImplementation.AddUtilizator(utilizator));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidUser))));
        }

        /// <summary>
        ///     Test for adding an invalid user (a user with empty first name).
        /// </summary>
        [Test]
        public void ADD_InvalidUser_FirstName_Empty()
        {
            Utilizator utilizator = new Utilizator(string.Empty, "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
            ;
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var utilizatorServicesImplementation = new UtilizatorServicesImplementation(utilizatorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(utilizatorServicesImplementation.AddUtilizator(utilizator));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidUser))));
        }

        /// <summary>
        ///     Test for adding an invalid user (a user with first name too long).
        /// </summary>
        [Test]
        public void ADD_InvalidUser_FirstName_TooLong()
        {
            Utilizator utilizator = new Utilizator('X' + new string('x', 20), "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
            ;
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var utilizatorServicesImplementation = new UtilizatorServicesImplementation(utilizatorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(utilizatorServicesImplementation.AddUtilizator(utilizator));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidUser))));
        }

        /// <summary>
        ///     Test for adding an invalid user (a user with first name that doesn't start with an uppercase letter).
        /// </summary>
        [Test]
        public void ADD_InvalidUser_FirstName_NoUpperCaseLetter()
        {
            Utilizator utilizator = new Utilizator(new string('x', 10), "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
            ;
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var utilizatorServicesImplementation = new UtilizatorServicesImplementation(utilizatorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(utilizatorServicesImplementation.AddUtilizator(utilizator));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidUser))));
        }

        /// <summary>
        ///     Test for adding an invalid user (a user with first name that only has uppercase letters).
        /// </summary>
        [Test]
        public void ADD_InvalidUser_FirstName_NoLowerCaseLetters()
        {
            Utilizator utilizator = new Utilizator(new string('X', 10), "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
            ;
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var utilizatorServicesImplementation = new UtilizatorServicesImplementation(utilizatorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(utilizatorServicesImplementation.AddUtilizator(utilizator));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidUser))));
        }

        /// <summary>
        ///     Test for adding an invalid user (a user with first name that contains symbols).
        /// </summary>
        [Test]
        public void ADD_InvalidUser_FirstName_ContainsSymbol()
        {
            Utilizator utilizator = new Utilizator("Andrei!", "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var utilizatorServicesImplementation = new UtilizatorServicesImplementation(utilizatorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(utilizatorServicesImplementation.AddUtilizator(utilizator));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidUser))));
        }

        /// <summary>
        ///     Test for adding an invalid user (a user with first name that contains numbers).
        /// </summary>
        [Test]
        public void ADD_InvalidUser_FirstName_ContainsNumber()
        {
            Utilizator utilizator = new Utilizator("Andrei1", "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var utilizatorServicesImplementation = new UtilizatorServicesImplementation(utilizatorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(utilizatorServicesImplementation.AddUtilizator(utilizator));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidUser))));
        }

        /// <summary>
        ///     Test for adding an invalid user (a user with null last name).
        /// </summary>
        [Test]
        public void ADD_InvalidUser_LastName_Null()
        {
            Utilizator utilizator = new Utilizator("Andrei", null, "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var utilizatorServicesImplementation = new UtilizatorServicesImplementation(utilizatorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(utilizatorServicesImplementation.AddUtilizator(utilizator));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidUser))));
        }

        /// <summary>
        ///     Test for adding an invalid user (a user with empty last name).
        /// </summary>
        [Test]
        public void ADD_InvalidUser_LastName_Empty()
        {
            Utilizator utilizator = new Utilizator("Andrei", string.Empty, "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var utilizatorServicesImplementation = new UtilizatorServicesImplementation(utilizatorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(utilizatorServicesImplementation.AddUtilizator(utilizator));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidUser))));
        }

        /// <summary>
        ///     Test for adding an invalid user (a user with last name too long).
        /// </summary>
        [Test]
        public void ADD_InvalidUser_LastName_TooLong()
        {
            Utilizator utilizator = new Utilizator("Andrei", 'X' + new string('x', 20), "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var utilizatorServicesImplementation = new UtilizatorServicesImplementation(utilizatorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(utilizatorServicesImplementation.AddUtilizator(utilizator));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidUser))));
        }

        /// <summary>
        ///     Test for adding an invalid user (a user with last name that doesn't start with an uppercase letter).
        /// </summary>
        [Test]
        public void ADD_InvalidUser_LastName_NoUpperCaseLetter()
        {
            Utilizator utilizator = new Utilizator("Andrei", new string('x', 10), "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var utilizatorServicesImplementation = new UtilizatorServicesImplementation(utilizatorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(utilizatorServicesImplementation.AddUtilizator(utilizator));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidUser))));
        }

        /// <summary>
        ///     Test for adding an invalid user (a user with last name that only has uppercase letters).
        /// </summary>
        [Test]
        public void ADD_InvalidUser_LastName_NoLowerCaseLetters()
        {
            Utilizator utilizator = new Utilizator("Andrei", new string('X', 10), "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var utilizatorServicesImplementation = new UtilizatorServicesImplementation(utilizatorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(utilizatorServicesImplementation.AddUtilizator(utilizator));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidUser))));
        }

        /// <summary>
        ///     Test for adding an invalid user (a user with last name that contains symbols).
        /// </summary>
        [Test]
        public void ADD_InvalidUser_LastName_ContainsSymbol()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai!", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var utilizatorServicesImplementation = new UtilizatorServicesImplementation(utilizatorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(utilizatorServicesImplementation.AddUtilizator(utilizator));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidUser))));
        }

        /// <summary>
        ///     Test for adding an invalid user (a user with last name that contains numbers).
        /// </summary>
        [Test]
        public void ADD_InvalidUser_LastName_ContainsNumber()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai2", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var utilizatorServicesImplementation = new UtilizatorServicesImplementation(utilizatorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(utilizatorServicesImplementation.AddUtilizator(utilizator));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidUser))));
        }

        /// <summary>Test for invalid user (user with null email address).</summary>
        [Test]
        public void InvalidUser_Email_Null()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai2", null, "P@ssword123", 5000430385597, "0");
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var utilizatorServicesImplementation = new UtilizatorServicesImplementation(utilizatorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(utilizatorServicesImplementation.AddUtilizator(utilizator));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidUser))));

        }

        /// <summary>Test for invalid user (user with empty email address).</summary>
        [Test]
        public void InvalidUser_Email_Empty()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai2", string.Empty, "P@ssword123", 5000430385597, "0");
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var utilizatorServicesImplementation = new UtilizatorServicesImplementation(utilizatorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(utilizatorServicesImplementation.AddUtilizator(utilizator));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidUser))));

        }

        /// <summary>Test for invalid user (user with email address too long).</summary>
        [Test]
        public void InvalidUser_Email_TooLong()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai2", new string('x', 60), "P@ssword123", 5000430385597, "0");
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var utilizatorServicesImplementation = new UtilizatorServicesImplementation(utilizatorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(utilizatorServicesImplementation.AddUtilizator(utilizator));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidUser))));

        }

        /// <summary>Test for invalid user (user with invalid email address).</summary>
        [Test]
        public void InvalidUser_Email_Invalid()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai2", "andreifakemail.com", "P@ssword123", 5000430385597, "0");
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var utilizatorServicesImplementation = new UtilizatorServicesImplementation(utilizatorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(utilizatorServicesImplementation.AddUtilizator(utilizator));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidUser))));

        }

        /// <summary>Test for invalid user (user with null password).</summary>
        [Test]
        public void InvalidUser_Password_Null()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai2", "andrei@fakemail.com", null, 5000430385597, "0");
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var utilizatorServicesImplementation = new UtilizatorServicesImplementation(utilizatorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(utilizatorServicesImplementation.AddUtilizator(utilizator));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidUser))));

        }

        /// <summary>Test for invalid user (user with empty password).</summary>
        [Test]
        public void InvalidUser_Password_Empty()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai2", "andrei@fakemail.com", string.Empty, 5000430385597, "0");
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var utilizatorServicesImplementation = new UtilizatorServicesImplementation(utilizatorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(utilizatorServicesImplementation.AddUtilizator(utilizator));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidUser))));

        }

        /// <summary>Test for invalid user (user with password too short).</summary>
        [Test]
        public void InvalidUser_Password_TooShort()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai2", "andrei@fakemail.com", "P@", 5000430385597, "0");
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var utilizatorServicesImplementation = new UtilizatorServicesImplementation(utilizatorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(utilizatorServicesImplementation.AddUtilizator(utilizator));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidUser))));

        }

        /// <summary>Test for invalid user (user with password too long).</summary>
        [Test]
        public void InvalidUser_Password_TooLong()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai2", "andrei@fakemail.com", "P@ssword123" + new string('x', 60), 5000430385597, "0");
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var utilizatorServicesImplementation = new UtilizatorServicesImplementation(utilizatorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(utilizatorServicesImplementation.AddUtilizator(utilizator));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidUser))));

        }

        /// <summary>Test for invalid user (user with password that doesn't contain uppercase letters).</summary>
        [Test]
        public void InvalidUser_Password_MissingUpperCaseLetter()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai2", "andrei@fakemail.com", "@ssword123", 5000430385597, "0");
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var utilizatorServicesImplementation = new UtilizatorServicesImplementation(utilizatorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(utilizatorServicesImplementation.AddUtilizator(utilizator));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidUser))));

        }

        /// <summary>Test for invalid user (user with password that doesn't contain lowercase letters).</summary>
        [Test]
        public void InvalidUser_Password_MissingLowerCaseLetter()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai2", "andrei@fakemail.com", "P@JUUUUUUUUU123", 5000430385597, "0");
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var utilizatorServicesImplementation = new UtilizatorServicesImplementation(utilizatorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(utilizatorServicesImplementation.AddUtilizator(utilizator));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidUser))));

        }

        /// <summary>Test for invalid user (user with password that doesn't contain numbers).</summary>
        [Test]
        public void InvalidUser_Password_MissingNumber()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai2", "andrei@fakemail.com", "P@sswordwerwerwe", 5000430385597, "0");
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var utilizatorServicesImplementation = new UtilizatorServicesImplementation(utilizatorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(utilizatorServicesImplementation.AddUtilizator(utilizator));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidUser))));

        }

        /// <summary>Test for invalid user (user with password that doesn't contain symbols).</summary>
        [Test]
        public void InvalidUser_Password_MissingSymbol()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai2", "andrei@fakemail.com", "Pssword123", 5000430385597, "0");
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var utilizatorServicesImplementation = new UtilizatorServicesImplementation(utilizatorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(utilizatorServicesImplementation.AddUtilizator(utilizator));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidUser))));

        }

        /// <summary>Test for valid user (user with null phone number).</summary>
        [Test]
        public void NegativeCNP()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", -5000430385597, "0");
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var utilizatorServicesImplementation = new UtilizatorServicesImplementation(utilizatorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(utilizatorServicesImplementation.AddUtilizator(utilizator));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidUser))));

        }

        /// <summary>Test for invalid user (user with empty phone number).</summary>
        [Test]
        public void IncorectFirstNumberCNP()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 4000430385597, "0");
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var utilizatorServicesImplementation = new UtilizatorServicesImplementation(utilizatorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(utilizatorServicesImplementation.AddUtilizator(utilizator));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidUser))));

        }

        /// <summary>Test for invalid user (user with phone number too long).</summary>
        [Test]
        public void IncorectMonthCNP()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 5002430385597, "0");
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var utilizatorServicesImplementation = new UtilizatorServicesImplementation(utilizatorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(utilizatorServicesImplementation.AddUtilizator(utilizator));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidUser))));

        }

        /// <summary>Test for invalid user (user with invalid phone number).</summary>
        [Test]
        public void IncorectDayCNP()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 5000450385597, "0");
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var utilizatorServicesImplementation = new UtilizatorServicesImplementation(utilizatorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(utilizatorServicesImplementation.AddUtilizator(utilizator));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidUser))));

        }



        /// <summary>Test for valid user (user with null phone number).</summary>
        [Test]
        public void IncorectDayGratherThan32CNP()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 5000435385597, "0");
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var utilizatorServicesImplementation = new UtilizatorServicesImplementation(utilizatorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(utilizatorServicesImplementation.AddUtilizator(utilizator));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidUser))));

        }

        [Test]
        public void UnderAgeCNP()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 5200430385597, "0");
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var utilizatorServicesImplementation = new UtilizatorServicesImplementation(utilizatorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(utilizatorServicesImplementation.AddUtilizator(utilizator));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidUser))));

        }

        /// <summary>Test for invalid user (user with empty phone number).</summary>
        [Test]
        public void NullBonusuri()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, null);
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var utilizatorServicesImplementation = new UtilizatorServicesImplementation(utilizatorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(utilizatorServicesImplementation.AddUtilizator(utilizator));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidUser))));

        }



        /// <summary>Test for valid user (user with null phone number).</summary>
        [Test]
        public void EmptyBonusuri()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, string.Empty);
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var utilizatorServicesImplementation = new UtilizatorServicesImplementation(utilizatorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(utilizatorServicesImplementation.AddUtilizator(utilizator));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidUser))));

        }

        [Test]
        public void ExistingMail()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");

            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();
            utilizatorServiceMock.Setup(x => x.EmailAlreadyExists(utilizator.Emali)).Returns(true);
            utilizatorServiceMock.Setup(x => x.AddUtilizator(utilizator)).Returns(true);
            var utilizatorServicesImplementation = new UtilizatorServicesImplementation(utilizatorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(utilizatorServicesImplementation.AddUtilizator(utilizator));
        }

    }
}
