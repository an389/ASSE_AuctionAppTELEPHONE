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

namespace TestServiceLayer.AbonamentServiceTest
{
    internal class AddAbonamentTests
    {
        private const string LogInvalidAbonament = "Attempted to add an invalid abonament.";
        [Test]
        public void ValidAbonament()
        {
            Abonament abonament = new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), 100, 100, 100, 100, 100, 100, 100, 100, 100, 1);

            var abonamentServiceMock = new Mock<IAbonamentDataServices>();
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();
            abonamentServiceMock.Setup(x => x.NameAlreadyExist(abonament.Name)).Returns(false);
            abonamentServiceMock.Setup(x => x.AddAbonament(abonament)).Returns(true);
            var abonamentServiceMockImplementation = new AbonamentServicesImplementation(abonamentServiceMock.Object, loggerMock.Object, utilizatorServiceMock.Object);

            Assert.IsTrue(abonamentServiceMockImplementation.AddAbonament(abonament));
        }


        [Test]
        public void NullAbonament()
        {
            Abonament abonament = null;

            var abonamentServiceMock = new Mock<IAbonamentDataServices>();
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();
            var abonamentServiceMockImplementation = new AbonamentServicesImplementation(abonamentServiceMock.Object, loggerMock.Object, utilizatorServiceMock.Object);

            Assert.IsFalse(abonamentServiceMockImplementation.AddAbonament(abonament));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add a null abonament.")));

        }



        [Test]
        public void InvalidUser_EmptyAbonament()
        {
            Abonament abonament = new Abonament();

            var abonamentServiceMock = new Mock<IAbonamentDataServices>();
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();
            var abonamentServiceMockImplementation = new AbonamentServicesImplementation(abonamentServiceMock.Object, loggerMock.Object, utilizatorServiceMock.Object);

            Assert.IsFalse(abonamentServiceMockImplementation.AddAbonament(abonament));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogInvalidAbonament)));
        }

        [Test]
        public void InvalidUser_Name_Null()
        {
            Abonament abonament = new Abonament(null, 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(360), 100, 100, 100, 100, 100, 100, 100, 100, 100, 0);


            var abonamentServiceMock = new Mock<IAbonamentDataServices>();
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();
            var abonamentServiceMockImplementation = new AbonamentServicesImplementation(abonamentServiceMock.Object, loggerMock.Object, utilizatorServiceMock.Object);

            Assert.IsFalse(abonamentServiceMockImplementation.AddAbonament(abonament));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogInvalidAbonament)));
        }

        [Test]
        public void InvalidAbonament_TooLongName()
        {
            Abonament abonament = new Abonament(new string('x', 255), 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(360), 100, 100, 100, 100, 100, 100, 100, 100, 100, 0);

            var abonamentServiceMock = new Mock<IAbonamentDataServices>();
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();
            var abonamentServiceMockImplementation = new AbonamentServicesImplementation(abonamentServiceMock.Object, loggerMock.Object, utilizatorServiceMock.Object);

            Assert.IsFalse(abonamentServiceMockImplementation.AddAbonament(abonament));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogInvalidAbonament)));
        }

        [Test]
        public void InvalidAbonamentNegativePret()
        {
            Abonament abonament = new Abonament("Abonament0", -100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(360), 100, 100, 100, 100, 100, 100, 100, 100, 100, 0);
            var abonamentServiceMock = new Mock<IAbonamentDataServices>();
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();
            var abonamentServiceMockImplementation = new AbonamentServicesImplementation(abonamentServiceMock.Object, loggerMock.Object, utilizatorServiceMock.Object);

            Assert.IsFalse(abonamentServiceMockImplementation.AddAbonament(abonament));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogInvalidAbonament)));
        }

        [Test]
        public void InvalidStartDate()
        {
            Abonament abonament = new Abonament("Abonament0", 100, DateTime.Today, DateTime.Today.AddDays(365), 100, 100, 100, 100, 100, 100, 100, 100, 100, 0);

            var abonamentServiceMock = new Mock<IAbonamentDataServices>();
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();
            var abonamentServiceMockImplementation = new AbonamentServicesImplementation(abonamentServiceMock.Object, loggerMock.Object, utilizatorServiceMock.Object);

            Assert.IsFalse(abonamentServiceMockImplementation.AddAbonament(abonament));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogInvalidAbonament)));
        }

        [Test]
        public void InvalidEndtDate()
        {
            Abonament abonament = new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today, 100, 100, 100, 100, 100, 100, 100, 100, 100, 0);

            var abonamentServiceMock = new Mock<IAbonamentDataServices>();
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();
            var abonamentServiceMockImplementation = new AbonamentServicesImplementation(abonamentServiceMock.Object, loggerMock.Object, utilizatorServiceMock.Object);

            Assert.IsFalse(abonamentServiceMockImplementation.AddAbonament(abonament));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogInvalidAbonament)));
        }

        [Test]
        public void NegativeNumarMinuteNationale()
        {
            Abonament abonament = new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(360), -100, 100, 100, 100, 100, 100, 100, 100, 100, 0);

            var abonamentServiceMock = new Mock<IAbonamentDataServices>();
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();
            var abonamentServiceMockImplementation = new AbonamentServicesImplementation(abonamentServiceMock.Object, loggerMock.Object, utilizatorServiceMock.Object);

            Assert.IsFalse(abonamentServiceMockImplementation.AddAbonament(abonament));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogInvalidAbonament)));
        }

        [Test]
        public void NegativeNumarMinuteInternationale()
        {
            Abonament abonament = new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(360), 100, -100, 100, 100, 100, 100, 100, 100, 100, 0);

            var abonamentServiceMock = new Mock<IAbonamentDataServices>();
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();
            var abonamentServiceMockImplementation = new AbonamentServicesImplementation(abonamentServiceMock.Object, loggerMock.Object, utilizatorServiceMock.Object);

            Assert.IsFalse(abonamentServiceMockImplementation.AddAbonament(abonament));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogInvalidAbonament)));
        }
        [Test]
        public void NegativeNumarMinuteReteaNegative()
        {
            Abonament abonament = new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(360), 100, 100, -100, 100, 100, 100, 100, 100, 100, 0);

            var abonamentServiceMock = new Mock<IAbonamentDataServices>();
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();
            var abonamentServiceMockImplementation = new AbonamentServicesImplementation(abonamentServiceMock.Object, loggerMock.Object, utilizatorServiceMock.Object);

            Assert.IsFalse(abonamentServiceMockImplementation.AddAbonament(abonament));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogInvalidAbonament)));
        }
        [Test]
        public void NegativeSMSNationale()
        {
            Abonament abonament = new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(360), 100, 100, 100, -100, 100, 100, 100, 100, 100, 0);

            var abonamentServiceMock = new Mock<IAbonamentDataServices>();
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();
            var abonamentServiceMockImplementation = new AbonamentServicesImplementation(abonamentServiceMock.Object, loggerMock.Object, utilizatorServiceMock.Object);

            Assert.IsFalse(abonamentServiceMockImplementation.AddAbonament(abonament));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogInvalidAbonament)));
        }
        [Test]
        public void NegativeSMSInternationale()
        {
            Abonament abonament = new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(360), 100, 100, 100, 100, -100, 100, 100, 100, 100, 0);

            var abonamentServiceMock = new Mock<IAbonamentDataServices>();
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();
            var abonamentServiceMockImplementation = new AbonamentServicesImplementation(abonamentServiceMock.Object, loggerMock.Object, utilizatorServiceMock.Object);

            Assert.IsFalse(abonamentServiceMockImplementation.AddAbonament(abonament));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogInvalidAbonament)));
        }
        [Test]
        public void NegativeSMSRetea()
        {
            Abonament abonament = new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(360), 100, 100, 100, 100, 100, -100, 100, 100, 100, 0);

            var abonamentServiceMock = new Mock<IAbonamentDataServices>();
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();
            var abonamentServiceMockImplementation = new AbonamentServicesImplementation(abonamentServiceMock.Object, loggerMock.Object, utilizatorServiceMock.Object);

            Assert.IsFalse(abonamentServiceMockImplementation.AddAbonament(abonament));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogInvalidAbonament)));
        }
        [Test]
        public void NegativeTraficDeDateNationale()
        {
            Abonament abonament = new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(360), 100, 100, 100, 100, 100, 100, -100, 100, 100, 0);

            var abonamentServiceMock = new Mock<IAbonamentDataServices>();
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();
            var abonamentServiceMockImplementation = new AbonamentServicesImplementation(abonamentServiceMock.Object, loggerMock.Object, utilizatorServiceMock.Object);

            Assert.IsFalse(abonamentServiceMockImplementation.AddAbonament(abonament));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogInvalidAbonament)));
        }
        [Test]
        public void NegativeTraficDeDateInternationale()
        {
            Abonament abonament = new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(360), 100, 100, 100, 100, 100, 100, 100, -100, 100, 0);

            var abonamentServiceMock = new Mock<IAbonamentDataServices>();
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();
            var abonamentServiceMockImplementation = new AbonamentServicesImplementation(abonamentServiceMock.Object, loggerMock.Object, utilizatorServiceMock.Object);

            Assert.IsFalse(abonamentServiceMockImplementation.AddAbonament(abonament));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogInvalidAbonament)));
        }
        [Test]
        public void NegativeTraficDeDateRetea()
        {
            Abonament abonament = new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(360), 100, 100, 100, 100, 100, 100, 100, 100, -100, 0);

            var abonamentServiceMock = new Mock<IAbonamentDataServices>();
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();
            var abonamentServiceMockImplementation = new AbonamentServicesImplementation(abonamentServiceMock.Object, loggerMock.Object, utilizatorServiceMock.Object);

            Assert.IsFalse(abonamentServiceMockImplementation.AddAbonament(abonament));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogInvalidAbonament)));
        }
        [Test]
        public void NegativBuissniesID()
        {
            Abonament abonament = new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(360), 100, 100, 100, 100, 100, 100, 100, 100, 100, -1);

            var abonamentServiceMock = new Mock<IAbonamentDataServices>();
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();
            var abonamentServiceMockImplementation = new AbonamentServicesImplementation(abonamentServiceMock.Object, loggerMock.Object, utilizatorServiceMock.Object);

            Assert.IsFalse(abonamentServiceMockImplementation.AddAbonament(abonament));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogInvalidAbonament)));
        }
        [Test]
        public void ExistingAbonament()
        {
            Abonament abonament = new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), 100, 100, 100, 100, 100, 100, 100, 100, 100, 1);


            var abonamentServiceMock = new Mock<IAbonamentDataServices>();
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();
            abonamentServiceMock.Setup(x => x.NameAlreadyExist(abonament.Name)).Returns(true);
            abonamentServiceMock.Setup(x => x.AddAbonament(abonament)).Returns(true);
            var abonamentServiceMockImplementation = new AbonamentServicesImplementation(abonamentServiceMock.Object, loggerMock.Object, utilizatorServiceMock.Object);

            Assert.IsFalse(abonamentServiceMockImplementation.AddAbonament(abonament));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an already existing abonament.")));
        }

        [Test]
        public void NulldEmailAddAbonamentForUtilizatorTest()
        {
            Abonament abonament = new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), 100, 100, 100, 100, 100, 100, 100, 100, 100, 1);
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
            string userEmail = null;
            string abonamentName = abonament.Name;

            var abonamentServiceMock = new Mock<IAbonamentDataServices>();
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var abonamentServiceMockImplementation = new AbonamentServicesImplementation(abonamentServiceMock.Object, loggerMock.Object, utilizatorServiceMock.Object);

            Assert.IsFalse(abonamentServiceMockImplementation.AddAbonamentForUtilizator(userEmail, abonamentName));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an abonament to a null User Email.")));
        }

        [Test]
        public void EmptydEmailAddAbonamentForUtilizatorTest()
        {
            Abonament abonament = new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), 100, 100, 100, 100, 100, 100, 100, 100, 100, 1);
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
            string userEmail = string.Empty;
            string abonamentName = abonament.Name;

            var abonamentServiceMock = new Mock<IAbonamentDataServices>();
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();
            var abonamentServiceMockImplementation = new AbonamentServicesImplementation(abonamentServiceMock.Object, loggerMock.Object, utilizatorServiceMock.Object);

            Assert.IsFalse(abonamentServiceMockImplementation.AddAbonamentForUtilizator(userEmail, abonamentName));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an abonament to a null User Email.")));
        }

        [Test]
        public void NullAbonamentNameAddAbonamentForUtilizatorTest()
        {
            Abonament abonament = new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), 100, 100, 100, 100, 100, 100, 100, 100, 100, 1);
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
            string userEmail = utilizator.Emali;
            string abonamentName = null;

            var abonamentServiceMock = new Mock<IAbonamentDataServices>();
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var abonamentServiceMockImplementation = new AbonamentServicesImplementation(abonamentServiceMock.Object, loggerMock.Object, utilizatorServiceMock.Object);

            Assert.IsFalse(abonamentServiceMockImplementation.AddAbonamentForUtilizator(userEmail, abonamentName));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add a null abonament to an User")));
        }

        [Test]
        public void EmptyAbonamentNameAddAbonamentForUtilizatorTest()
        {
            Abonament abonament = new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), 100, 100, 100, 100, 100, 100, 100, 100, 100, 1);
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
            string userEmail = utilizator.Emali;
            string abonamentName = null;

            var abonamentServiceMock = new Mock<IAbonamentDataServices>();
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();
            var abonamentServiceMockImplementation = new AbonamentServicesImplementation(abonamentServiceMock.Object, loggerMock.Object, utilizatorServiceMock.Object);

            Assert.IsFalse(abonamentServiceMockImplementation.AddAbonamentForUtilizator(userEmail, abonamentName));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add a null abonament to an User")));
        }

        [Test]
        public void EmailAlreadyExistsAddAbonamentForUtilizatorTest()
        {
            Abonament abonament = new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), 100, 100, 100, 100, 100, 100, 100, 100, 100, 1);
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
            string userEmail = utilizator.Emali;
            string abonamentName = abonament.Name;

            var abonamentServiceMock = new Mock<IAbonamentDataServices>();
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            utilizatorServiceMock.Setup(x => x.EmailAlreadyExists(userEmail)).Returns(false);
            var loggerMock = new Mock<ILog>();
            var abonamentServiceMockImplementation = new AbonamentServicesImplementation(abonamentServiceMock.Object, loggerMock.Object, utilizatorServiceMock.Object);

            Assert.IsFalse(abonamentServiceMockImplementation.AddAbonamentForUtilizator(userEmail, abonamentName));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an abonament to inexisting user.")));
        }

        [Test]
        public void FindAbonamentByNameAddAbonamentForUtilizatorTest()
        {
            Abonament abonament = new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), 100, 100, 100, 100, 100, 100, 100, 100, 100, 1);
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
            string userEmail = utilizator.Emali;
            string abonamentName = abonament.Name;

            var abonamentServiceMock = new Mock<IAbonamentDataServices>();
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            utilizatorServiceMock.Setup(x => x.EmailAlreadyExists(userEmail)).Returns(true);
            abonamentServiceMock.Setup(x => x.FindAbonamentByName(abonamentName)).Returns(false);
            var loggerMock = new Mock<ILog>();
            var abonamentServiceMockImplementation = new AbonamentServicesImplementation(abonamentServiceMock.Object, loggerMock.Object, utilizatorServiceMock.Object);

            Assert.IsFalse(abonamentServiceMockImplementation.AddAbonamentForUtilizator(userEmail, abonamentName));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add inexisting abonament to the user.")));
        }

        [Test]
        public void FalseAddAbonamentForUtilizatorTest()
        {
            Abonament abonament = new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), 100, 100, 100, 100, 100, 100, 100, 100, 100, 1);
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");
            string userEmail = utilizator.Emali;
            string abonamentName = abonament.Name;

            var abonamentServiceMock = new Mock<IAbonamentDataServices>();
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            utilizatorServiceMock.Setup(x => x.EmailAlreadyExists(userEmail)).Returns(true);
            abonamentServiceMock.Setup(x => x.FindAbonamentByName(abonamentName)).Returns(true);
            var abonamentDataService = new Mock<IAbonamentDataServices>();
            abonamentDataService.Setup(x => x.AddAbonamentForUtilizator(userEmail, abonamentName)).Returns(false);
            var loggerMock = new Mock<ILog>();
            var abonamentServiceMockImplementation = new AbonamentServicesImplementation(abonamentServiceMock.Object, loggerMock.Object, utilizatorServiceMock.Object);

            Assert.IsFalse(abonamentServiceMockImplementation.AddAbonamentForUtilizator(userEmail, abonamentName));
        }

      
    }
}
