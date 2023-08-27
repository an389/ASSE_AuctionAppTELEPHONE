using DataMapper.Interfaces;
using DomainModel.Models;
using log4net;
using Moq;
using NUnit.Framework;
using ServiceLayer.Implementation;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServiceLayer.FacturaServiceTests
{
    [ExcludeFromCodeCoverage]
    internal class CalculareFacturiInFunctieDeLuna
    {
        [Test]
        public void CalculareFacturiInFunctieDeLunaTest()
        {

            List<Utilizator> utilizatorsExpected = new List<Utilizator>
            {
                new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0"),
            };

            List<AbonamentUser> abonamentsExpected = new List<AbonamentUser>
            {
               new AbonamentUser(null, new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), 100,100,100,100,100,100,100,100,100,1)),
            };

            Buisniess buisniessesExpected = new Buisniess(1.2, 1.2, 1.2, 5.0, 5.0, 1.5, 20);
 
            List<Bonusuri> bonusurisExpected = new List<Bonusuri>{
                new Bonusuri("Bonus1", DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), true, 100, 100, 100, 100, 100, 100, 100, 100, 100),
                    };

            List<CentralaTelefonica> centralaTelefonicasExpected = new List<CentralaTelefonica>
            {
                 new CentralaTelefonica("andrei@fakemail.com", DateTime.Today, 12, DateTime.Today, 10, DateTime.Today, 9, DateTime.Today, 2, DateTime.Today, 2,DateTime.Today, 3,DateTime.Today, 44,DateTime.Today ,12,DateTime.Today, 0),
            };

            int lastMonth = DateTime.Now.Month;
            var loggerMock = new Mock<ILog>();

            var utilizatordataServiceMock = new Mock<IUtilizatorDataServices>();
            var utilizatorServiceMock = new Mock<IUtilizatorServices>();
            utilizatorServiceMock.Setup(x => x.GetAllUsers()).Returns(utilizatorsExpected);
            var utilizatorServicesImplementationMock = new UtilizatorServicesImplementation(utilizatordataServiceMock.Object, loggerMock.Object);

            var abonamentDataServiceMock = new Mock<IAbonamentDataServices>();
            var abonamentServiceMock = new Mock<IAbonamentServices>();
            abonamentDataServiceMock.Setup(x => x.GetAbonamentsByUserId(utilizatorsExpected[0].Id)).Returns(abonamentsExpected);
            MinutesSMSInternet minutesSMSInternet = new MinutesSMSInternet(0,0,0,0,0,0,0,0,0);
            abonamentServiceMock.Setup(x => x.CalculateAllNumberOfMinutesSMSInternet(abonamentsExpected, bonusurisExpected)).Returns(minutesSMSInternet);
            var abonamentServiceImplementationMock = new AbonamentServicesImplementation(abonamentDataServiceMock.Object, loggerMock.Object, utilizatordataServiceMock.Object);

            var centralaServiceMock = new Mock<ICentralaTelefonicaDataService>();
            centralaServiceMock.Setup(x => x.GetUtilizatorCentralaTelefonicaByEmail(DateTime.Now.Month, "andrei@fakemail.com")).Returns(centralaTelefonicasExpected);
            var centralatServiceMockImplementationMock = new CentralaTelefonicaServicesImplementation(centralaServiceMock.Object, loggerMock.Object);

            var buisniessServiceMock = new Mock<IBuisniessDataService>();
            buisniessServiceMock.Setup(x => x.GetBuisniess()).Returns(buisniessesExpected);
            buisniessServiceMock.Setup(x => x.GetBuisniessById(buisniessesExpected.Id)).Returns(buisniessesExpected);
            var buisniessServiceImplementationMock = new BuisniessServiceImplementation(buisniessServiceMock.Object, loggerMock.Object);

            var bonusuriServiceMock = new Mock<IBonusDataService>();
            bonusuriServiceMock.Setup(x => x.GetBonusById(bonusurisExpected[0].Id)).Returns(bonusurisExpected[0]);
            var bonusuriServiceImplementationMock = new BonusuriServiceImplementation(bonusuriServiceMock.Object, loggerMock.Object);

            var facturiServiceMock = new Mock<IFacturaDataService>();
            var facturiServiceImplementationMock = new FacturaServicesImplementation(facturiServiceMock.Object, loggerMock.Object);

            

           

        }
   }

}
