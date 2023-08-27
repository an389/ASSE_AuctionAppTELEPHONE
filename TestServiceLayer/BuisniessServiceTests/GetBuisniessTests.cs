using DataMapper.Interfaces;
using DomainModel.Models;
using log4net;
using Moq;
using NUnit.Framework;
using ServiceLayer.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServiceLayer.BuisniessServiceTests
{
    internal class GetBuisniessTests
    {
        [Test]
        public void GetBuisniess()
        {
            Buisniess buisniess = new Buisniess(1.2, 1.2, 1.2, 5.0, 5.0, 1.5, 20);


            var buisniessServiceMock = new Mock<IBuisniessDataService>();
            buisniessServiceMock.Setup(x => x.GetBuisniess()).Returns(buisniess);
            var loggerMock = new Mock<ILog>();

            var buisniessServiceMockServices = new BuisniessServiceImplementation(buisniessServiceMock.Object, loggerMock.Object);

            var expected = buisniess;
            var actual = buisniessServiceMockServices.GetBuisniess();

            Assert.AreEqual(expected.TVA, actual.TVA);
            Assert.AreEqual(expected.ProcentRaportare, actual.ProcentRaportare);
            Assert.AreEqual(expected.XPercentForClosingSooner, actual.XPercentForClosingSooner);
        }

        [Test]
        public void GetBuisniessById()
        {
            Buisniess buisniess = new Buisniess(1.2, 1.2, 1.2, 5.0, 5.0, 1.5, 20);


            var buisniessServiceMock = new Mock<IBuisniessDataService>();
            buisniessServiceMock.Setup(x => x.GetBuisniessById(buisniess.Id)).Returns(buisniess);
            var loggerMock = new Mock<ILog>();

            var buisniessServiceMockServices = new BuisniessServiceImplementation(buisniessServiceMock.Object, loggerMock.Object);

            var expected = buisniess;
            var actual = buisniessServiceMockServices.GetBuisniessById(buisniess.Id);

            Assert.AreEqual(expected.TVA, actual.TVA);
            Assert.AreEqual(expected.ProcentRaportare, actual.ProcentRaportare);
            Assert.AreEqual(expected.XPercentForClosingSooner, actual.XPercentForClosingSooner);

        }


    }
}
