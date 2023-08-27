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

namespace TestServiceLayer.BonusuriServiceTests
{
    internal class AddBonusuriTests
    {
        [Test]
        public void AddValidBonusTest()
        {
            Bonusuri bonusuri = new Bonusuri("Bonus1", DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), true, 100, 100, 100, 100, 100, 100, 100, 100, 100);


            var bonusServiceMock = new Mock<IBonusDataService>();
            var loggerMock = new Mock<ILog>();
            bonusServiceMock.Setup(x => x.NameAlreadyExist(bonusuri.Name)).Returns(false);
            bonusServiceMock.Setup(x => x.AddBonus(bonusuri)).Returns(true);
            var abonamentServiceMockImplementation = new BonusuriServiceImplementation(bonusServiceMock.Object, loggerMock.Object);

            Assert.IsTrue(abonamentServiceMockImplementation.AddBonus(bonusuri));
        }

        [Test]
        public void AddNullBonusTest()
        {
            Bonusuri bonusuri = null;

            var bonusServiceMock = new Mock<IBonusDataService>();
            var loggerMock = new Mock<ILog>();


            var abonamentServiceMockImplementation = new BonusuriServiceImplementation(bonusServiceMock.Object, loggerMock.Object);
            Assert.IsFalse(abonamentServiceMockImplementation.AddBonus(bonusuri));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add a null bonusuri.")));

        }

        [Test]
        public void NameAlreadyExistTest()
        {
            Bonusuri bonusuri = new Bonusuri("Bonus1", DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), true, 100, 100, 100, 100, 100, 100, 100, 100, 100);

            var bonusServiceMock = new Mock<IBonusDataService>();
            var loggerMock = new Mock<ILog>();

            bonusServiceMock.Setup(x => x.NameAlreadyExist(bonusuri.Name)).Returns(true);
            bonusServiceMock.Setup(x => x.AddBonus(bonusuri)).Returns(true);

            var abonamentServiceMockImplementation = new BonusuriServiceImplementation(bonusServiceMock.Object, loggerMock.Object);
            Assert.IsFalse(abonamentServiceMockImplementation.AddBonus(bonusuri));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an already existing bonusuri.")));

        }

        [Test]
        public void StartDate()
        {
            Bonusuri bonusuri = new Bonusuri("Bonus1", DateTime.Today, DateTime.Today.AddDays(365), true, 100, 100, 100, 100, 100, 100, 100, 100, 100);

            var bonusServiceMock = new Mock<IBonusDataService>();
            var loggerMock = new Mock<ILog>();

            bonusServiceMock.Setup(x => x.NameAlreadyExist(bonusuri.Name)).Returns(false);
            bonusServiceMock.Setup(x => x.AddBonus(bonusuri)).Returns(true);

            var abonamentServiceMockImplementation = new BonusuriServiceImplementation(bonusServiceMock.Object, loggerMock.Object);
            Assert.IsFalse(abonamentServiceMockImplementation.AddBonus(bonusuri));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid bonusuri. ")));

        }
        [Test]
        public void EndDate()
        {
            Bonusuri bonusuri = new Bonusuri("Bonus1", DateTime.Today.AddDays(1), DateTime.Today, true, 100, 100, 100, 100, 100, 100, 100, 100, 100);


            var bonusServiceMock = new Mock<IBonusDataService>();
            var loggerMock = new Mock<ILog>();

            bonusServiceMock.Setup(x => x.NameAlreadyExist(bonusuri.Name)).Returns(false);
            bonusServiceMock.Setup(x => x.AddBonus(bonusuri)).Returns(true);

            var abonamentServiceMockImplementation = new BonusuriServiceImplementation(bonusServiceMock.Object, loggerMock.Object);
            Assert.IsFalse(abonamentServiceMockImplementation.AddBonus(bonusuri));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid bonusuri. ")));


        }



        /// <summary>Test for invalid user (user with null first name).</summary>
        [Test]
        public void NameNull()
        {
            Bonusuri bonusuri = new Bonusuri(null, DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), true, 100, 100, 100, 100, 100, 100, 100, 100, 100);

            var bonusServiceMock = new Mock<IBonusDataService>();
            var loggerMock = new Mock<ILog>();

            bonusServiceMock.Setup(x => x.NameAlreadyExist(bonusuri.Name)).Returns(false);
            bonusServiceMock.Setup(x => x.AddBonus(bonusuri)).Returns(true);

            var abonamentServiceMockImplementation = new BonusuriServiceImplementation(bonusServiceMock.Object, loggerMock.Object);
            Assert.IsFalse(abonamentServiceMockImplementation.AddBonus(bonusuri));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid bonusuri. ")));

        }

        /// <summary>Test for invalid user (user with empty first name).</summary>
        [Test]
        public void NameEmpty()
        {
            Bonusuri bonusuri = new Bonusuri(string.Empty, DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), true, 100, 100, 100, 100, 100, 100, 100, 100, 100);

            var bonusServiceMock = new Mock<IBonusDataService>();
            var loggerMock = new Mock<ILog>();

            bonusServiceMock.Setup(x => x.NameAlreadyExist(bonusuri.Name)).Returns(false);
            bonusServiceMock.Setup(x => x.AddBonus(bonusuri)).Returns(true);

            var abonamentServiceMockImplementation = new BonusuriServiceImplementation(bonusServiceMock.Object, loggerMock.Object);
            Assert.IsFalse(abonamentServiceMockImplementation.AddBonus(bonusuri));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid bonusuri. ")));

        }

        /// <summary>Test for invalid user (user with first name too long).</summary>
        [Test]
        public void NameTooLong()
        {
            Bonusuri bonusuri = new Bonusuri('X' + new string('x', 270), DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), true, 100, 100, 100, 100, 100, 100, 100, 100, 100);

            var bonusServiceMock = new Mock<IBonusDataService>();
            var loggerMock = new Mock<ILog>();

            bonusServiceMock.Setup(x => x.NameAlreadyExist(bonusuri.Name)).Returns(false);
            bonusServiceMock.Setup(x => x.AddBonus(bonusuri)).Returns(true);

            var abonamentServiceMockImplementation = new BonusuriServiceImplementation(bonusServiceMock.Object, loggerMock.Object);
            Assert.IsFalse(abonamentServiceMockImplementation.AddBonus(bonusuri));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid bonusuri. ")));

        }
        [Test]
        public void NameTooShort()
        {
            Bonusuri bonusuri = new Bonusuri("Xx", DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), true, 100, 100, 100, 100, 100, 100, 100, 100, 100);

            var bonusServiceMock = new Mock<IBonusDataService>();
            var loggerMock = new Mock<ILog>();

            bonusServiceMock.Setup(x => x.NameAlreadyExist(bonusuri.Name)).Returns(false);
            bonusServiceMock.Setup(x => x.AddBonus(bonusuri)).Returns(true);

            var abonamentServiceMockImplementation = new BonusuriServiceImplementation(bonusServiceMock.Object, loggerMock.Object);
            Assert.IsFalse(abonamentServiceMockImplementation.AddBonus(bonusuri));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid bonusuri. ")));

        }

        [Test]
        public void NegativeNumarMinuteNationale()
        {
            Bonusuri bonusuri = new Bonusuri("Bonus1", DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), true, -100, 100, 100, 100, 100, 100, 100, 100, 100);

            var bonusServiceMock = new Mock<IBonusDataService>();
            var loggerMock = new Mock<ILog>();

            bonusServiceMock.Setup(x => x.NameAlreadyExist(bonusuri.Name)).Returns(false);
            bonusServiceMock.Setup(x => x.AddBonus(bonusuri)).Returns(true);

            var abonamentServiceMockImplementation = new BonusuriServiceImplementation(bonusServiceMock.Object, loggerMock.Object);
            Assert.IsFalse(abonamentServiceMockImplementation.AddBonus(bonusuri));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid bonusuri. ")));

        }

        [Test]
        public void NegativeNumarMinuteInternationale()
        {
            Bonusuri bonusuri = new Bonusuri("Bonus1", DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), true, 100, -100, 100, 100, 100, 100, 100, 100, 100);

            var bonusServiceMock = new Mock<IBonusDataService>();
            var loggerMock = new Mock<ILog>();

            bonusServiceMock.Setup(x => x.NameAlreadyExist(bonusuri.Name)).Returns(false);
            bonusServiceMock.Setup(x => x.AddBonus(bonusuri)).Returns(true);

            var abonamentServiceMockImplementation = new BonusuriServiceImplementation(bonusServiceMock.Object, loggerMock.Object);
            Assert.IsFalse(abonamentServiceMockImplementation.AddBonus(bonusuri));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid bonusuri. ")));

        }

        [Test]
        public void NegativeNumarMinuteReteaNegative()
        {
            Bonusuri bonusuri = new Bonusuri("Bonus1", DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), true, 100, 100, -100, 100, 100, 100, 100, 100, 100);

            var bonusServiceMock = new Mock<IBonusDataService>();
            var loggerMock = new Mock<ILog>();

            bonusServiceMock.Setup(x => x.NameAlreadyExist(bonusuri.Name)).Returns(false);
            bonusServiceMock.Setup(x => x.AddBonus(bonusuri)).Returns(true);

            var abonamentServiceMockImplementation = new BonusuriServiceImplementation(bonusServiceMock.Object, loggerMock.Object);
            Assert.IsFalse(abonamentServiceMockImplementation.AddBonus(bonusuri));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid bonusuri. ")));

        }

        [Test]
        public void NegativeSMSNationale()
        {
            Bonusuri bonusuri = new Bonusuri("Bonus1", DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), true, 100, 100, 100, -100, 100, 100, 100, 100, 100);

            var bonusServiceMock = new Mock<IBonusDataService>();
            var loggerMock = new Mock<ILog>();

            bonusServiceMock.Setup(x => x.NameAlreadyExist(bonusuri.Name)).Returns(false);
            bonusServiceMock.Setup(x => x.AddBonus(bonusuri)).Returns(true);

            var abonamentServiceMockImplementation = new BonusuriServiceImplementation(bonusServiceMock.Object, loggerMock.Object);
            Assert.IsFalse(abonamentServiceMockImplementation.AddBonus(bonusuri));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid bonusuri. ")));

        }

        [Test]
        public void NegativeSMSInternationale()
        {
            Bonusuri bonusuri = new Bonusuri("Bonus1", DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), true, 100, 100, 100, 100, -100, 100, 100, 100, 100);

            var bonusServiceMock = new Mock<IBonusDataService>();
            var loggerMock = new Mock<ILog>();

            bonusServiceMock.Setup(x => x.NameAlreadyExist(bonusuri.Name)).Returns(false);
            bonusServiceMock.Setup(x => x.AddBonus(bonusuri)).Returns(true);

            var abonamentServiceMockImplementation = new BonusuriServiceImplementation(bonusServiceMock.Object, loggerMock.Object);
            Assert.IsFalse(abonamentServiceMockImplementation.AddBonus(bonusuri));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid bonusuri. ")));

        }

        [Test]
        public void NegativeSMSRetea()
        {
            Bonusuri bonusuri = new Bonusuri("Bonus1", DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), true, 100, 100, 100, 100, 100, -100, 100, 100, 100);

            var bonusServiceMock = new Mock<IBonusDataService>();
            var loggerMock = new Mock<ILog>();

            bonusServiceMock.Setup(x => x.NameAlreadyExist(bonusuri.Name)).Returns(false);
            bonusServiceMock.Setup(x => x.AddBonus(bonusuri)).Returns(true);

            var abonamentServiceMockImplementation = new BonusuriServiceImplementation(bonusServiceMock.Object, loggerMock.Object);
            Assert.IsFalse(abonamentServiceMockImplementation.AddBonus(bonusuri));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid bonusuri. ")));

        }

        [Test]
        public void NegativeTraficDeDateNationale()
        {
            Bonusuri bonusuri = new Bonusuri("Bonus1", DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), true, 100, 100, 100, 100, 100, 100, -100, 100, 100);

            var bonusServiceMock = new Mock<IBonusDataService>();
            var loggerMock = new Mock<ILog>();

            bonusServiceMock.Setup(x => x.NameAlreadyExist(bonusuri.Name)).Returns(false);
            bonusServiceMock.Setup(x => x.AddBonus(bonusuri)).Returns(true);

            var abonamentServiceMockImplementation = new BonusuriServiceImplementation(bonusServiceMock.Object, loggerMock.Object);
            Assert.IsFalse(abonamentServiceMockImplementation.AddBonus(bonusuri));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid bonusuri. ")));

        }

        [Test]
        public void NegativeTraficDeDateInternationale()
        {
            Bonusuri bonusuri = new Bonusuri("Bonus1", DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), true, 100, 100, 100, 100, 100, 100, 100, -100, 100);

            var bonusServiceMock = new Mock<IBonusDataService>();
            var loggerMock = new Mock<ILog>();

            bonusServiceMock.Setup(x => x.NameAlreadyExist(bonusuri.Name)).Returns(false);
            bonusServiceMock.Setup(x => x.AddBonus(bonusuri)).Returns(true);

            var abonamentServiceMockImplementation = new BonusuriServiceImplementation(bonusServiceMock.Object, loggerMock.Object);
            Assert.IsFalse(abonamentServiceMockImplementation.AddBonus(bonusuri));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid bonusuri. ")));

        }

        [Test]
        public void NegativeTraficDeDateRetea()
        {
            Bonusuri bonusuri = new Bonusuri("Bonus1", DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), true, 100, 100, 100, 100, 100, 100, 100, 100, -100);
            var bonusServiceMock = new Mock<IBonusDataService>();
            var loggerMock = new Mock<ILog>();

            bonusServiceMock.Setup(x => x.NameAlreadyExist(bonusuri.Name)).Returns(false);
            bonusServiceMock.Setup(x => x.AddBonus(bonusuri)).Returns(true);

            var abonamentServiceMockImplementation = new BonusuriServiceImplementation(bonusServiceMock.Object, loggerMock.Object);
            Assert.IsFalse(abonamentServiceMockImplementation.AddBonus(bonusuri));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid bonusuri. ")));

        }
    }
}
