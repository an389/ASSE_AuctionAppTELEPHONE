using DataMapper.Interfaces;
using DomainModel.Models;
using log4net;
using Moq;
using NUnit.Framework;
using ServiceLayer.Implementation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServiceLayer.UtilizatorServiceTest
{
    [ExcludeFromCodeCoverage]
    internal class UpdateUtilizatorTest
    {
        [Test]
        public void UPDATE_NullUserAbonamentUserCloseAbonamentSooner()
        {
            Utilizator user = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");


            string userEmail = user.Emali;
            string abonament = null;

            var userServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UtilizatorServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UserCloseAbonamentSooner(userEmail, abonament));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to update a null utilizator.")));
        }

        [Test]
        public void UPDATE_NullUserEmailUserCloseAbonamentSooner()
        {
            Utilizator user = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");


            string userEmail = null;
            string abonament = "1";

            var userServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UtilizatorServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UserCloseAbonamentSooner(userEmail, abonament));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to update a null utilizator.")));
        }

        [Test]
        public void UPDATE_NonExistingUserUserCloseAbonamentSooner()
        {
            Utilizator user = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");

            string userEmail = user.Emali;
            string abonament = "1";

            var userServiceMock = new Mock<IUtilizatorDataServices>();
            userServiceMock.Setup(x => x.EmailAlreadyExists(userEmail)).Returns(false);
            var loggerMock = new Mock<ILog>();

            var userServices = new UtilizatorServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UserCloseAbonamentSooner(userEmail, abonament));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Cant find the user!")));
        }

        [Test]
        public void UPDATE_UserCloseAbonamentSoonerValidUser()
        {
            Utilizator user = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");

            string userEmail = user.Emali;
            string abonament = "1";

            var userServiceMock = new Mock<IUtilizatorDataServices>();
            userServiceMock.Setup(x => x.EmailAlreadyExists(userEmail)).Returns(true);
            userServiceMock.Setup(x => x.CloseAbonamentForUser(userEmail, abonament)).Returns(true);

            var loggerMock = new Mock<ILog>();

            var userServices = new UtilizatorServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsTrue(userServices.UserCloseAbonamentSooner(userEmail, abonament));
        }
    }
}

