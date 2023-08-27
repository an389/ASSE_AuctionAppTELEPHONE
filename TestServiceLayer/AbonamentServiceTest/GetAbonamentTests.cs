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

namespace TestServiceLayer.AbonamentServiceTest
{
    internal class GetAbonamentTests
    {
        [Test]
        public void GetAbonamentByIdTest()
        {
            Abonament abonament = new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), 100, 100, 100, 100, 100, 100, 100, 100, 100, 1);

            var abonamentServiceMock = new Mock<IAbonamentDataServices>();
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            abonamentServiceMock.Setup(x => x.GetAbonamentById(abonament.Id)).Returns(abonament);
            var loggerMock = new Mock<ILog>();

            var abonamentServices = new AbonamentServicesImplementation(abonamentServiceMock.Object, loggerMock.Object, utilizatorServiceMock.Object);

            var expected = abonament;
            var actual = abonamentServices.GetAbonamentById(abonament.Id);

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
        }

        [Test]
        public void GetAbonamentsByUserIdTest()
        {
            List<AbonamentUser> abonaments = new List<AbonamentUser>
     {
        new AbonamentUser(null, new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), 100,100,100,100,100,100,100,100,100,1)),
        new AbonamentUser(null, new Abonament("Abonament1", 110, DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), 200,200,200,200,200,200,200,200,200,1)),
        new AbonamentUser(null, new Abonament("Abonament2", 120, DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), 300,300,300,300,300,300,300,300,300,1)),
        new AbonamentUser(null, new Abonament("Abonament3", 130, DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), 400, 400, 400, 400, 400, 400, 400, 400, 400,1)),
     };
            Abonament abonament = new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), 100, 100, 100, 100, 100, 100, 100, 100, 100, 1);
            Utilizator utilizator = new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");


            var abonamentServiceMock = new Mock<IAbonamentDataServices>();
            var utilizatorServiceMock = new Mock<IUtilizatorDataServices>();
            abonamentServiceMock.Setup(x => x.GetAbonamentsByUserId(utilizator.Id)).Returns(abonaments);
            var loggerMock = new Mock<ILog>();

            var abonamentServices = new AbonamentServicesImplementation(abonamentServiceMock.Object, loggerMock.Object, utilizatorServiceMock.Object);

            var expected = abonaments;
            var actual = abonamentServices.GetAbonamentsByUserId(utilizator.Id);

            Assert.AreEqual(expected.Count, actual.Count);

            foreach(var act in actual)
            {
                Assert.AreEqual(expected[0].Name, actual[0].Name);
            }

        }

    }
}
