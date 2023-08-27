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

namespace TestServiceLayer.CentralaTelefonicaServiceTests
{
    internal class GetCentralaTelefonicaTests
    {
        [Test]
        public void GetUtilizatoriCentralaTelefonicaByEmail()
        {
            List<CentralaTelefonica> centralas = new List<CentralaTelefonica>
            {
                 new CentralaTelefonica("andrei@fakemail.com", DateTime.Today, 12, DateTime.Today, 10, DateTime.Today, 9, DateTime.Today, 2, DateTime.Today, 2,DateTime.Today, 3,DateTime.Today, 44,DateTime.Today ,12,DateTime.Today, 0),
                 new CentralaTelefonica("andrei@fakemail.com", DateTime.Today, 12, DateTime.Today, 10, DateTime.Today, 9, DateTime.Today, 2, DateTime.Today, 2,DateTime.Today, 3,DateTime.Today, 44,DateTime.Today ,12,DateTime.Today, 0),
                };
            var centralaServiceMock = new Mock<ICentralaTelefonicaDataService>();
            var loggerMock = new Mock<ILog>();
            centralaServiceMock.Setup(x => x.GetUtilizatorCentralaTelefonicaByEmail(DateTime.Today.Day, "andrei@fakemail.com")).Returns(centralas);
            var centralatServiceMockImplementation = new CentralaTelefonicaServicesImplementation(centralaServiceMock.Object, loggerMock.Object);
            var expected = centralas;
            var actual = centralatServiceMockImplementation.GetUtilizatoriCentralaTelefonicaByEmail(DateTime.Today.Day, "andrei@fakemail.com");
            Assert.AreEqual(expected.Count, actual.Count);
            Assert.AreEqual(expected[0].ClientEmail, actual[0].ClientEmail);
        }
    }
}
