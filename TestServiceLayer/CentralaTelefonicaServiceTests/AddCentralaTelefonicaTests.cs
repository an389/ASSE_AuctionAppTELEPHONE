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

namespace TestServiceLayer.CentralaTelefonicaServiceTests
{
    internal class AddCentralaTelefonicaTests
    {
        [Test]
        public void ValidCentralaTelefonicaValidation()
        {
            CentralaTelefonica centrala = new CentralaTelefonica("andrei@fakemail.com", DateTime.Today, 12, DateTime.Today, 10, DateTime.Today, 9, DateTime.Today, 2, DateTime.Today, 2, DateTime.Today, 3, DateTime.Today, 44, DateTime.Today, 12, DateTime.Today, 0);

            var centralaServiceMock = new Mock<ICentralaTelefonicaDataService>();
            var loggerMock = new Mock<ILog>();
            centralaServiceMock.Setup(x => x.AddCentralaTelefonica(centrala)).Returns(true);
            var centralatServiceMockImplementation = new CentralaTelefonicaServicesImplementation(centralaServiceMock.Object, loggerMock.Object);

            Assert.IsTrue(centralatServiceMockImplementation.AddCentralaTelefonica(centrala));
        }
        [Test]
        public void InvalidCentralaTelefonica_Email_Null()
        {
            CentralaTelefonica centrala = new CentralaTelefonica(null, DateTime.Today, 12, DateTime.Today, 10, DateTime.Today, 9, DateTime.Today, 2, DateTime.Today, 2, DateTime.Today, 3, DateTime.Today, 44, DateTime.Today, 12, DateTime.Today, 0);

            var centralaServiceMock = new Mock<ICentralaTelefonicaDataService>();
            var loggerMock = new Mock<ILog>();
            centralaServiceMock.Setup(x => x.AddCentralaTelefonica(centrala)).Returns(true);
            var centralatServiceMockImplementation = new CentralaTelefonicaServicesImplementation(centralaServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(centralatServiceMockImplementation.AddCentralaTelefonica(centrala));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid centralaTelefonica.")));

        }

        [Test]
        public void InvalidCentralaTelefonica_Null()
        {
            CentralaTelefonica centrala = null;

            var centralaServiceMock = new Mock<ICentralaTelefonicaDataService>();
            var loggerMock = new Mock<ILog>();
            centralaServiceMock.Setup(x => x.AddCentralaTelefonica(centrala)).Returns(true);
            var centralatServiceMockImplementation = new CentralaTelefonicaServicesImplementation(centralaServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(centralatServiceMockImplementation.AddCentralaTelefonica(centrala));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add a null centralaTelefonica.")));

        }

        /// <summary>Test for invalid user (user with empty email address).</summary>
        [Test]
        public void InvalidCentralaTelefonica_Email_Empty()
        {
            CentralaTelefonica centrala = new CentralaTelefonica(string.Empty, DateTime.Today, 12, DateTime.Today, 10, DateTime.Today, 9, DateTime.Today, 2, DateTime.Today, 2, DateTime.Today, 3, DateTime.Today, 44, DateTime.Today, 12, DateTime.Today, 0);

            var centralaServiceMock = new Mock<ICentralaTelefonicaDataService>();
            var loggerMock = new Mock<ILog>();
            centralaServiceMock.Setup(x => x.AddCentralaTelefonica(centrala)).Returns(true);
            var centralatServiceMockImplementation = new CentralaTelefonicaServicesImplementation(centralaServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(centralatServiceMockImplementation.AddCentralaTelefonica(centrala));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid centralaTelefonica.")));

        }

        /// <summary>Test for invalid user (user with email address too long).</summary>
        [Test]
        public void InvalidCentralaTelefonica_Email_TooLong()
        {
            CentralaTelefonica centrala = new CentralaTelefonica(new string('x', 30) + '@' + new string('x', 30), DateTime.Today, 12, DateTime.Today, 10, DateTime.Today, 9, DateTime.Today, 2, DateTime.Today, 2, DateTime.Today, 3, DateTime.Today, 44, DateTime.Today, 12, DateTime.Today, 0);

            var centralaServiceMock = new Mock<ICentralaTelefonicaDataService>();
            var loggerMock = new Mock<ILog>();
            centralaServiceMock.Setup(x => x.AddCentralaTelefonica(centrala)).Returns(true);
            var centralatServiceMockImplementation = new CentralaTelefonicaServicesImplementation(centralaServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(centralatServiceMockImplementation.AddCentralaTelefonica(centrala));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid centralaTelefonica.")));

        }

        /// <summary>Test for invalid user (user with invalid email address).</summary>
        [Test]
        public void InvalidCentralaTelefonica_Email_Invalid()
        {
            CentralaTelefonica centrala = new CentralaTelefonica("andreifakemail.com", DateTime.Today, 12, DateTime.Today, 10, DateTime.Today, 9, DateTime.Today, 2, DateTime.Today, 2, DateTime.Today, 3, DateTime.Today, 44, DateTime.Today, 12, DateTime.Today, 0);

            var centralaServiceMock = new Mock<ICentralaTelefonicaDataService>();
            var loggerMock = new Mock<ILog>();
            centralaServiceMock.Setup(x => x.AddCentralaTelefonica(centrala)).Returns(true);
            var centralatServiceMockImplementation = new CentralaTelefonicaServicesImplementation(centralaServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(centralatServiceMockImplementation.AddCentralaTelefonica(centrala));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid centralaTelefonica.")));

        }



        [Test]
        public void InvalidNegativeNumarMinuteNationale()
        {
            CentralaTelefonica centrala = new CentralaTelefonica("andrei@fakemail.com", DateTime.Today, -12, DateTime.Today, 10, DateTime.Today, 9, DateTime.Today, 2, DateTime.Today, 2, DateTime.Today, 3, DateTime.Today, 44, DateTime.Today, 12, DateTime.Today, 0);

            var centralaServiceMock = new Mock<ICentralaTelefonicaDataService>();
            var loggerMock = new Mock<ILog>();
            centralaServiceMock.Setup(x => x.AddCentralaTelefonica(centrala)).Returns(true);
            var centralatServiceMockImplementation = new CentralaTelefonicaServicesImplementation(centralaServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(centralatServiceMockImplementation.AddCentralaTelefonica(centrala));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid centralaTelefonica.")));

        }

        [Test]
        public void InvalidNegativeNumarMinuteInternationale()
        {
            CentralaTelefonica centrala = new CentralaTelefonica("andrei@fakemail.com", DateTime.Today, 12, DateTime.Today, -10, DateTime.Today, 9, DateTime.Today, 2, DateTime.Today, 2, DateTime.Today, 3, DateTime.Today, 44, DateTime.Today, 12, DateTime.Today, 0);

            var centralaServiceMock = new Mock<ICentralaTelefonicaDataService>();
            var loggerMock = new Mock<ILog>();
            centralaServiceMock.Setup(x => x.AddCentralaTelefonica(centrala)).Returns(true);
            var centralatServiceMockImplementation = new CentralaTelefonicaServicesImplementation(centralaServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(centralatServiceMockImplementation.AddCentralaTelefonica(centrala));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid centralaTelefonica.")));

        }
        [Test]
        public void InvalidNegativeNumarMinuteReteaNegative()
        {
            CentralaTelefonica centrala = new CentralaTelefonica("andrei@fakemail.com", DateTime.Today, 12, DateTime.Today, 10, DateTime.Today, -9, DateTime.Today, 2, DateTime.Today, 2, DateTime.Today, 3, DateTime.Today, 44, DateTime.Today, 12, DateTime.Today, 0);
            var centralaServiceMock = new Mock<ICentralaTelefonicaDataService>();
            var loggerMock = new Mock<ILog>();
            centralaServiceMock.Setup(x => x.AddCentralaTelefonica(centrala)).Returns(true);
            var centralatServiceMockImplementation = new CentralaTelefonicaServicesImplementation(centralaServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(centralatServiceMockImplementation.AddCentralaTelefonica(centrala));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid centralaTelefonica.")));

        }
        [Test]
        public void InvalidNegativeSMSNationale()
        {
            CentralaTelefonica centrala = new CentralaTelefonica("andrei@fakemail.com", DateTime.Today, 12, DateTime.Today, 10, DateTime.Today, 9, DateTime.Today, -2, DateTime.Today, 2, DateTime.Today, 3, DateTime.Today, 44, DateTime.Today, 12, DateTime.Today, 0);

            var centralaServiceMock = new Mock<ICentralaTelefonicaDataService>();
            var loggerMock = new Mock<ILog>();
            centralaServiceMock.Setup(x => x.AddCentralaTelefonica(centrala)).Returns(true);
            var centralatServiceMockImplementation = new CentralaTelefonicaServicesImplementation(centralaServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(centralatServiceMockImplementation.AddCentralaTelefonica(centrala));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid centralaTelefonica.")));

        }
        [Test]
        public void InvalidNegativeSMSInternationale()
        {
            CentralaTelefonica centrala = new CentralaTelefonica("andrei@fakemail.com", DateTime.Today, 12, DateTime.Today, 10, DateTime.Today, 9, DateTime.Today, 2, DateTime.Today, -2, DateTime.Today, 3, DateTime.Today, 44, DateTime.Today, 12, DateTime.Today, 0);

            var centralaServiceMock = new Mock<ICentralaTelefonicaDataService>();
            var loggerMock = new Mock<ILog>();
            centralaServiceMock.Setup(x => x.AddCentralaTelefonica(centrala)).Returns(true);
            var centralatServiceMockImplementation = new CentralaTelefonicaServicesImplementation(centralaServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(centralatServiceMockImplementation.AddCentralaTelefonica(centrala));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid centralaTelefonica.")));

        }
        [Test]
        public void InvalidNegativeSMSRetea()
        {
            CentralaTelefonica centrala = new CentralaTelefonica("andrei@fakemail.com", DateTime.Today, 12, DateTime.Today, 10, DateTime.Today, 9, DateTime.Today, 2, DateTime.Today, 2, DateTime.Today, -3, DateTime.Today, 44, DateTime.Today, 12, DateTime.Today, 0);

            var centralaServiceMock = new Mock<ICentralaTelefonicaDataService>();
            var loggerMock = new Mock<ILog>();
            centralaServiceMock.Setup(x => x.AddCentralaTelefonica(centrala)).Returns(true);
            var centralatServiceMockImplementation = new CentralaTelefonicaServicesImplementation(centralaServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(centralatServiceMockImplementation.AddCentralaTelefonica(centrala));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid centralaTelefonica.")));

        }
        [Test]
        public void InvalidNegativeTraficDeDateNationale()
        {
            CentralaTelefonica centrala = new CentralaTelefonica("andrei@fakemail.com", DateTime.Today, 12, DateTime.Today, 10, DateTime.Today, 9, DateTime.Today, 2, DateTime.Today, 2, DateTime.Today, 3, DateTime.Today, -44, DateTime.Today, 12, DateTime.Today, 0);

            var centralaServiceMock = new Mock<ICentralaTelefonicaDataService>();
            var loggerMock = new Mock<ILog>();
            centralaServiceMock.Setup(x => x.AddCentralaTelefonica(centrala)).Returns(true);
            var centralatServiceMockImplementation = new CentralaTelefonicaServicesImplementation(centralaServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(centralatServiceMockImplementation.AddCentralaTelefonica(centrala));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid centralaTelefonica.")));

        }
        [Test]
        public void InvalidNegativeTraficDeDateInternationale()
        {
            CentralaTelefonica centrala = new CentralaTelefonica("andrei@fakemail.com", DateTime.Today, 12, DateTime.Today, 10, DateTime.Today, 9, DateTime.Today, 2, DateTime.Today, 2, DateTime.Today, 3, DateTime.Today, 44, DateTime.Today, -12, DateTime.Today, 0);

            var centralaServiceMock = new Mock<ICentralaTelefonicaDataService>();
            var loggerMock = new Mock<ILog>();
            centralaServiceMock.Setup(x => x.AddCentralaTelefonica(centrala)).Returns(true);
            var centralatServiceMockImplementation = new CentralaTelefonicaServicesImplementation(centralaServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(centralatServiceMockImplementation.AddCentralaTelefonica(centrala));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid centralaTelefonica.")));

        }
        [Test]
        public void InvalidNegativeTraficDeDateRetea()
        {
            CentralaTelefonica centrala = new CentralaTelefonica("andrei@fakemail.com", DateTime.Today, 12, DateTime.Today, 10, DateTime.Today, 9, DateTime.Today, 2, DateTime.Today, 2, DateTime.Today, 3, DateTime.Today, 44, DateTime.Today, 12, DateTime.Today, -22);

            var centralaServiceMock = new Mock<ICentralaTelefonicaDataService>();
            var loggerMock = new Mock<ILog>();
            centralaServiceMock.Setup(x => x.AddCentralaTelefonica(centrala)).Returns(true);
            var centralatServiceMockImplementation = new CentralaTelefonicaServicesImplementation(centralaServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(centralatServiceMockImplementation.AddCentralaTelefonica(centrala));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid centralaTelefonica.")));

        }
    }

}
