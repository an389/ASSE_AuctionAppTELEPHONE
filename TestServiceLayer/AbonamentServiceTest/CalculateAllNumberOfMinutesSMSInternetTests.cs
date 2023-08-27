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

namespace TestServiceLayer.UserServiceTests
{
    internal class CalculateAllNumberOfMinutesSMSInternetTests
    {
        [Test]
        public void CalculateAllNumberOfMinutesSMSInternetValidation()
        {
            MinutesSMSInternet minutesSMSInterne = new MinutesSMSInternet(0, 0, 0, 0, 0, 0, 0, 0, 0);

            List<AbonamentUser> abonaments = new List<AbonamentUser> 
     {
        new AbonamentUser(null, new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), 100,100,100,100,100,100,100,100,100,1)),
        new AbonamentUser(null, new Abonament("Abonament1", 110, DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), 200,200,200,200,200,200,200,200,200,1)),
        new AbonamentUser(null, new Abonament("Abonament2", 120, DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), 300,300,300,300,300,300,300,300,300,1)),
        new AbonamentUser(null, new Abonament("Abonament3", 130, DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), 400, 400, 400, 400, 400, 400, 400, 400, 400,1)),
     };

            List<Bonusuri> bonusuri = new List<Bonusuri>
     {
         new Bonusuri("Bonus1", DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), true,      100, 100, 100, 100, 100, 100, 100, 100, 100),
         new Bonusuri("Bonus2", DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), true, 200, 200, 200, 200, 200, 200, 200, 200, 200),
         new Bonusuri("Bonus3", DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), true, 300, 300, 300, 300, 300, 300, 300, 300, 300),
         new Bonusuri("Bonus4", DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), true, 400, 400, 400, 400, 400, 400, 400, 400, 400),
         new Bonusuri("Bonus5", DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), true, 500, 500, 500, 500, 500, 500, 500, 500, 500),
     };

            var abonamentServiceMock = new Mock<IAbonamentDataServices>();
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            var loggerMock = new Mock<ILog>();

            var abonamentServiceMockImplementation = new AbonamentServicesImplementation(abonamentServiceMock.Object, loggerMock.Object, utilizatorServiceMock.Object);

            minutesSMSInterne = abonamentServiceMockImplementation.CalculateAllNumberOfMinutesSMSInternet(abonaments, bonusuri);

            Assert.AreEqual(minutesSMSInterne.DurataConvorbireRetea, 2500);
            Assert.AreEqual(minutesSMSInterne.DurataConvorbireNationala, 2500);
            Assert.AreEqual(minutesSMSInterne.DurataConvorbireInternationala, 2500);
            Assert.AreEqual(minutesSMSInterne.SMSRetea, 2500);
            Assert.AreEqual(minutesSMSInterne.SMSNationala, 2500);
            Assert.AreEqual(minutesSMSInterne.SMSInternationala, 2500);
            Assert.AreEqual(minutesSMSInterne.TraficDeDateRetea, 2500);
            Assert.AreEqual(minutesSMSInterne.TraficDeDateNationala, 2500);
            Assert.AreEqual(minutesSMSInterne.TraficDeDateInternationala, 2500);

        }


    }
}
