using DomainModel.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDomainModel.ValidationTests
{
    internal class AbonamentUserValidation
    {
        [Test]
        public void ValidAbonamentUserValidationValidation()
        {
            AbonamentUser abonamentUser = new AbonamentUser(
                new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0"),
                new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(360), 100, 100, 100, 100, 100, 100, 100, 100, 100, 0));

            Assert.IsTrue(abonamentUser.Activ);
        }
    }
}
