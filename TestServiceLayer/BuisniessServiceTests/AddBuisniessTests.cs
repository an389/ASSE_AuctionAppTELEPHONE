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

namespace TestServiceLayer.BuisniessServiceTests
{
    internal class AddBuisniessTests
    {
        [Test]
        public void BuisniessValid()
        {
            Buisniess buisniess = new Buisniess(1.2, 1.2, 1.2, 5.0, 5.0, 1.5, 20);

            var buisniessServiceMock = new Mock<IBuisniessDataService>();
            var loggerMock = new Mock<ILog>();
            buisniessServiceMock.Setup(x => x.AddBuisniess(buisniess)).Returns(true);
            var buisniessServiceMockImplementation = new BuisniessServiceImplementation(buisniessServiceMock.Object, loggerMock.Object);

            Assert.IsTrue(buisniessServiceMockImplementation.AddBuisniess(buisniess));
        }

        [Test]
        public void InvalidNegativeTVA()
        {
            Buisniess buisniess = new Buisniess(-1.2, 1.2, 1.2, 5.0, 5.0, 1.5, 20);

            var buisniessServiceMock = new Mock<IBuisniessDataService>();
            var loggerMock = new Mock<ILog>();
            buisniessServiceMock.Setup(x => x.AddBuisniess(buisniess)).Returns(true);
            var buisniessServiceMockImplementation = new BuisniessServiceImplementation(buisniessServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(buisniessServiceMockImplementation.AddBuisniess(buisniess));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid buisniess.")));

        }

        [Test]
        public void InvalidNegativeXPercentForClosingSooner()
        {
            Buisniess buisniess = new Buisniess(1.2, -1.2, 1.2, 5.0, 5.0, 1.5, 20);

            var buisniessServiceMock = new Mock<IBuisniessDataService>();
            var loggerMock = new Mock<ILog>();
            buisniessServiceMock.Setup(x => x.AddBuisniess(buisniess)).Returns(true);
            var buisniessServiceMockImplementation = new BuisniessServiceImplementation(buisniessServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(buisniessServiceMockImplementation.AddBuisniess(buisniess));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid buisniess.")));
        }
        [Test]
        public void InvalidNegativeProcentRaportareMinute()
        {
            Buisniess buisniess = new Buisniess(1.2, 1.2, -1.2, 5.0, 5.0, 1.5, 20);

            var buisniessServiceMock = new Mock<IBuisniessDataService>();
            var loggerMock = new Mock<ILog>();
            buisniessServiceMock.Setup(x => x.AddBuisniess(buisniess)).Returns(true);
            var buisniessServiceMockImplementation = new BuisniessServiceImplementation(buisniessServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(buisniessServiceMockImplementation.AddBuisniess(buisniess));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid buisniess.")));
        }
        [Test]
        public void InvalidNegativeCursValutarEUR()
        {
            Buisniess buisniess = new Buisniess(1.2, 1.2, 1.2, -5.0, 5.0, 1.5, 20);

            var buisniessServiceMock = new Mock<IBuisniessDataService>();
            var loggerMock = new Mock<ILog>();
            buisniessServiceMock.Setup(x => x.AddBuisniess(buisniess)).Returns(true);
            var buisniessServiceMockImplementation = new BuisniessServiceImplementation(buisniessServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(buisniessServiceMockImplementation.AddBuisniess(buisniess));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid buisniess.")));
        }
        [Test]
        public void InvalidNegativeCursValutarUSD()
        {
            Buisniess buisniess = new Buisniess(1.2, 1.2, 1.2, 5.0, -5.0, 1.5, 20);

            var buisniessServiceMock = new Mock<IBuisniessDataService>();
            var loggerMock = new Mock<ILog>();
            buisniessServiceMock.Setup(x => x.AddBuisniess(buisniess)).Returns(true);
            var buisniessServiceMockImplementation = new BuisniessServiceImplementation(buisniessServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(buisniessServiceMockImplementation.AddBuisniess(buisniess));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid buisniess.")));
        }
        [Test]
        public void InvalidNegativeProcentDepasireValoriAbonament()
        {
            Buisniess buisniess = new Buisniess(1.2, 1.2, 1.2, 5.0, 5.0, -1.5, 20);

            var buisniessServiceMock = new Mock<IBuisniessDataService>();
            var loggerMock = new Mock<ILog>();
            buisniessServiceMock.Setup(x => x.AddBuisniess(buisniess)).Returns(true);
            var buisniessServiceMockImplementation = new BuisniessServiceImplementation(buisniessServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(buisniessServiceMockImplementation.AddBuisniess(buisniess));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid buisniess.")));
        }
        [Test]
        public void InvalidNegativeProcentRaportare()
        {
            Buisniess buisniess = new Buisniess(1.2, 1.2, 1.2, 5.0, 5.0, 1.5, -20);

            var buisniessServiceMock = new Mock<IBuisniessDataService>();
            var loggerMock = new Mock<ILog>();
            buisniessServiceMock.Setup(x => x.AddBuisniess(buisniess)).Returns(true);
            var buisniessServiceMockImplementation = new BuisniessServiceImplementation(buisniessServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(buisniessServiceMockImplementation.AddBuisniess(buisniess));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add an invalid buisniess.")));
        }

        [Test]
        public void nullBuisniess()
        {
            Buisniess buisniess = null;

            var buisniessServiceMock = new Mock<IBuisniessDataService>();
            var loggerMock = new Mock<ILog>();
            buisniessServiceMock.Setup(x => x.AddBuisniess(buisniess)).Returns(true);
            var buisniessServiceMockImplementation = new BuisniessServiceImplementation(buisniessServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(buisniessServiceMockImplementation.AddBuisniess(buisniess));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == "Attempted to add a null buisniess.")));
        }
    }
}
