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
    internal class BuisniessValidation
    {
        private const string LogNegativeTVA = "[TVA] cannot be negative.";
        private const string LogNegativeXPercentForClosingSooner = "[XPercentForClosingSooner] cannot be negative.";
        private const string LogNegativeProcentRaportareMinute = "[ProcentRaportareMinute] cannot be negative.";
        private const string LogNegativeCursValutarEUR = "[CursValutarEUR] cannot be negative.";
        private const string LogNegativeCursValutarUSD = "[CursValutarUSD] cannot be negative.";
        private const string LogNegativeProcentDepasireValoriAbonament = "[ProcentDepasireValoriAbonament] cannot be negative.";
        private const string LogNegativeProcentRaportare = "[ProcentDepasireValoriAbonament] cannot be negative.";

        [Test]
        public void BuisniessValid()
        {
            Buisniess buisniess = new Buisniess(1.2, 1.2, 1.2, 5.0, 5.0, 1.5, 20);

            var context = new ValidationContext(buisniess, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsTrue(Validator.TryValidateObject(buisniess, context, results, true));
            Assert.AreEqual(0, results.Count);
        }

        [Test]
        public void InvalidNegativeTVA()
        {
            Buisniess buisniess = new Buisniess(-1.2, 1.2, 1.2, 5.0, 5.0, 1.5, 20);

            var context = new ValidationContext(buisniess, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(buisniess, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNegativeTVA, results[0].ErrorMessage);
        }

        [Test]
        public void InvalidNegativeXPercentForClosingSooner()
        {
            Buisniess buisniess = new Buisniess(1.2, -1.2, 1.2, 5.0, 5.0, 1.5, 20);

            var context = new ValidationContext(buisniess, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(buisniess, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNegativeXPercentForClosingSooner, results[0].ErrorMessage);
        }
        [Test]
        public void InvalidNegativeProcentRaportareMinute()
        {
            Buisniess buisniess = new Buisniess(1.2, 1.2, -1.2, 5.0, 5.0, 1.5, 20);

            var context = new ValidationContext(buisniess, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(buisniess, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNegativeProcentRaportareMinute, results[0].ErrorMessage);
        }
        [Test]
        public void InvalidNegativeCursValutarEUR()
        {
            Buisniess buisniess = new Buisniess(1.2, 1.2, 1.2, -5.0, 5.0, 1.5, 20);

            var context = new ValidationContext(buisniess, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(buisniess, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNegativeCursValutarEUR, results[0].ErrorMessage);
        }
        [Test]
        public void InvalidNegativeCursValutarUSD()
        {
            Buisniess buisniess = new Buisniess(1.2, 1.2, 1.2, 5.0, -5.0, 1.5, 20);

            var context = new ValidationContext(buisniess, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(buisniess, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNegativeCursValutarUSD, results[0].ErrorMessage);
        }
        [Test]
        public void InvalidNegativeProcentDepasireValoriAbonament()
        {
            Buisniess buisniess = new Buisniess(1.2, 1.2, 1.2, 5.0, 5.0, -1.5, 20);

            var context = new ValidationContext(buisniess, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(buisniess, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNegativeProcentDepasireValoriAbonament, results[0].ErrorMessage);
        }
        [Test]
        public void InvalidNegativeProcentRaportare()
        {
            Buisniess buisniess = new Buisniess(1.2, 1.2, 1.2, 5.0, 5.0, 1.5, -20);

            var context = new ValidationContext(buisniess, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(buisniess, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNegativeProcentRaportare, results[0].ErrorMessage);
        }
    }
}
