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

    internal class DeleteUtilizatorTest
    {
        private const string LogDeleteNonexistingUser = "Attempted to delete a nonexisting user.";
        private const string LogDeleteNullgUser = "Attempted to delete a null user.";

        [Test]
        public void DELETE_ValidUser_NonExistingUser()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
            Utilizator nullUser = null;

            var userServiceMock = new Mock<IUtilizatorDataServices>();
            userServiceMock.Setup(x => x.GetUtilizatorById(utilizator.Id)).Returns(nullUser);
            var loggerMock = new Mock<ILog>();

            var userServices = new UtilizatorServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.DeleteUtilizator(utilizator));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogDeleteNonexistingUser)));
        }

        [Test]
        public void DELETE_ValidUser()
        {
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");

            var userServiceMock = new Mock<IUtilizatorDataServices>();
            userServiceMock.Setup(x => x.GetUtilizatorById(utilizator.Id)).Returns(utilizator);
            userServiceMock.Setup(x => x.DeleteUtilizator(utilizator)).Returns(true);
            var loggerMock = new Mock<ILog>();

            var userServices = new UtilizatorServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsTrue(userServices.DeleteUtilizator(utilizator));
        }

        [Test]
        public void DELETE_NullUser()
        {
            Utilizator user = null;

            var userServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UtilizatorServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.DeleteUtilizator(user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogDeleteNullgUser)));
        }

    }
}
