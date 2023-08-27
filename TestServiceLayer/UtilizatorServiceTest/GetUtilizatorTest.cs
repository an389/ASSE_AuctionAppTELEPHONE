using DataMapper.Interfaces;
using DomainModel.Models;
using log4net;
using Moq;
using NUnit.Framework;
using ServiceLayer.Implementation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestServiceLayer.UtilizatorServiceTest
{

    [ExcludeFromCodeCoverage]
    internal class GetUtilizatorTest
    {

        [Test]
        public void GetAllUsersValidate()
        {

            List<Utilizator> utilizators = new List<Utilizator>
            {
                new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0"),
                new Utilizator("Andrei", "Iulian", "awwndrei@fakemail.com", "P@ssword123", 5000430385597, "0"),
                new Utilizator("Andrei", "Andrei", "ndrei@fakemail.com", "P@ssword123", 5000430385597, "0"),
                new Utilizator("Andrei", "Andreiut", "aaaaaaandrei@fakemail.com","P@ssword123", 5000430385597, "1"),
                new Utilizator("Andrei", "Mihaita", "aaaazaaaaasd@fakemail.com","P@ssword123", 5000430385597, "1"),
            };

            var userServiceMock = new Mock<IUtilizatorDataServices>();
            userServiceMock.Setup(x => x.GetAllUtilizatori()).Returns(utilizators);
            var loggerMock = new Mock<ILog>();

            var userServices = new UtilizatorServicesImplementation(userServiceMock.Object, loggerMock.Object);

            var expected = utilizators;
            var actual = userServices.GetAllUsers();

            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Id, actual[i].Id);
                Assert.AreEqual(expected[i].FirstName, actual[i].FirstName);
                Assert.AreEqual(expected[i].LastName, actual[i].LastName);
                Assert.AreEqual(expected[i].Password, actual[i].Password);
                Assert.AreEqual(expected[i].CNP, actual[i].CNP);
                Assert.AreEqual(expected[i].BonusuriId, actual[i].BonusuriId);
                Assert.AreEqual(expected[i].Emali, actual[i].Emali);
            }
        }
        [Test]
        public void GET_AllUsers_NoneFound()
        {
            List<Utilizator> emptyUserList = new List<Utilizator>();

            var userServiceMock = new Mock<IUtilizatorDataServices>();
            userServiceMock.Setup(x => x.GetAllUtilizatori()).Returns(emptyUserList);
            var loggerMock = new Mock<ILog>();

            var userServices = new UtilizatorServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsEmpty(userServices.GetAllUsers());
        }

        /// <summary>
        ///     Test for retrieving an existing user with the specified id.
        /// </summary>
        [Test]
        public void GET_UserById()
        {
            Utilizator user = GetSampleUser();

            var userServiceMock = new Mock<IUtilizatorDataServices>();
            userServiceMock.Setup(x => x.GetUtilizatorById(user.Id)).Returns(user);
            var loggerMock = new Mock<ILog>();

            var userServices = new UtilizatorServicesImplementation(userServiceMock.Object, loggerMock.Object);

            var expected = user;
            var actual = userServices.GetUtilizatorById(user.Id);

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
            Assert.AreEqual(expected.Password, actual.Password);
            Assert.AreEqual(expected.CNP, actual.CNP);
            Assert.AreEqual(expected.BonusuriId, actual.BonusuriId);
            Assert.AreEqual(expected.Emali, actual.Emali);
        }

        /// <summary>
        ///     Test for retrieving an existing user with the specified id but no such user was found.
        /// </summary>
        [Test]
        public void GET_UserById_NotFound()
        {
            Utilizator user = GetSampleUser();
            Utilizator nullUser = null;

            var userServiceMock = new Mock<IUtilizatorDataServices>();
            userServiceMock.Setup(x => x.GetUtilizatorById(user.Id)).Returns(nullUser);
            var loggerMock = new Mock<ILog>();

            var userServices = new UtilizatorServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsNull(userServices.GetUtilizatorById(user.Id));
        }

        private static Utilizator GetSampleUser()
        {
            return new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0");

        }

        /// <summary>Gets sample users.</summary>
        /// <returns>a list of sample users.</returns>
        private static List<Utilizator> GetSampleUsers()
        {
            return new List<Utilizator>
            {
                new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0"),
                new Utilizator("Andrei", "Iulian", "awwndrei@fakemail.com", "P@ssword123", 5000430385597, "0"),
                new Utilizator("Andrei", "Andrei", "ndrei@fakemail.com", "P@ssword123", 5000430385597, "0"),
                new Utilizator("Andrei", "Andreiut", "aaaaaaandrei@fakemail.com","P@ssword123", 5000430385597, "1"),
                new Utilizator("Andrei", "Mihaita", "aaaazaaaaasd@fakemail.com","P@ssword123", 5000430385597, "1"),
            };
        }
    }
}
