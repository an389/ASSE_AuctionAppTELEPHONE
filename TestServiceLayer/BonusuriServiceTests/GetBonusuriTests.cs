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

namespace TestServiceLayer.BonusuriServiceTests
{
    internal class GetBonusuriTests
    {
        [Test]
        public void GetBonusById()
        {
            Bonusuri bonusuri = new Bonusuri("Bonus1", DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), true, 100, 100, 100, 100, 100, 100, 100, 100, 100);

            var bonusuriServiceMock = new Mock<IBonusDataService>();
            bonusuriServiceMock.Setup(x => x.GetBonusById(bonusuri.Id)).Returns(bonusuri);
            var loggerMock = new Mock<ILog>();

            var abonamentServices = new BonusuriServiceImplementation(bonusuriServiceMock.Object, loggerMock.Object);

            var expected = bonusuri;
            var actual = abonamentServices.GetBonusById(bonusuri.Id);

            Assert.AreEqual(expected.Name, actual.Name);
        }
    }
}
