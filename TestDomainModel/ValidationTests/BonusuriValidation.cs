using DomainModel.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDomainModel.ValidationTests
{
    [ExcludeFromCodeCoverage]
    internal class BonusuriValidation
    {
        private const string LogStartDate = "Start date must be later than today 1 AM";
        private const string LogEndDate = "End date must be later than tomorrow";
        private const string LogNameNull = "[Name] cannot be null.";
        private const string LogNameInvalid = "[Name] must be between 1 and 250 characters.";
        private const string LogNegativeBonusConvorbireNationala = "[BonusConvorbireNationala] cannot be negative.";
        private const string LogNegativeBonusConvorbireInternationala = "[BonusConvorbireInternationala] cannot be negative.";
        private const string LogNegativeBonusConvorbireRetea = "[BonusConvorbireRetea] cannot be negative.";
        private const string LogNegativeBonusSMSNationala = "[BonusSMSNationala] cannot be negative.";
        private const string LogNegativeBonusSMSInternationala = "[BonusSMSInternationala] cannot be negative.";
        private const string LogNegativeBonusSMSRetea = "[BonusSMSRetea] cannot be negative.";
        private const string LogNegativeBonusTraficDeDateNationala = "[BonusTraficDeDateNationala] cannot be negative.";
        private const string LogNegativeBonusTraficDeDateInternationala = "[BonusTraficDeDateInternationala] cannot be negative.";
        private const string LogNegativeBonusTraficDeDateRetea = "[BonusTraficDeDateRetea] cannot be negative.";

        [Test]
        public void ValidAbonamentActiv()
        {
            Bonusuri bonusuri = new Bonusuri("Bonus1", DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), true, 100, 100, 100, 100, 100, 100, 100, 100, 100);

            var context = new ValidationContext(bonusuri, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsTrue(Validator.TryValidateObject(bonusuri, context, results, true));
            Assert.AreEqual(0, results.Count);
        }

        [Test]
        public void ValidAbonamentInActiv()
        {
            Bonusuri bonusuri = new Bonusuri("Bonus1", DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), false, 100, 100, 100, 100, 100, 100, 100, 100, 100);

            var context = new ValidationContext(bonusuri, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bonusuri, context, results, true));
            Assert.AreEqual(3, results.Count);
    
        }

        [Test]
        public void StartDate()
        {
            Bonusuri bonusuri = new Bonusuri("Bonus1", DateTime.Today, DateTime.Today.AddDays(365), true, 100, 100, 100, 100, 100, 100, 100, 100, 100);

            var context = new ValidationContext(bonusuri, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bonusuri, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogStartDate, results[0].ErrorMessage);
        }
        [Test]
        public void EndDate()
        {
            Bonusuri bonusuri = new Bonusuri("Bonus1", DateTime.Today.AddDays(1), DateTime.Today, true, 100, 100, 100, 100, 100, 100, 100, 100, 100);

            var context = new ValidationContext(bonusuri, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bonusuri, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogEndDate, results[0].ErrorMessage);
        }

        [Test]
        public void Active()
        {
            Bonusuri bonusuri = new Bonusuri("Bonus1", DateTime.Today.AddDays(1), DateTime.Today, true, 100, 100, 100, 100, 100, 100, 100, 100, 100);

            Assert.IsTrue(bonusuri.Active);
        }

        /// <summary>Test for invalid user (user with null first name).</summary>
        [Test]
        public void NameNull()
        {
            Bonusuri bonusuri = new Bonusuri(null, DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), true, 100, 100, 100, 100, 100, 100, 100, 100, 100);

            var context = new ValidationContext(bonusuri, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bonusuri, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNameNull, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with empty first name).</summary>
        [Test]
        public void NameEmpty()
        {
            Bonusuri bonusuri = new Bonusuri(string.Empty, DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), true, 100, 100, 100, 100, 100, 100, 100, 100, 100);

            var context = new ValidationContext(bonusuri, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bonusuri, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNameNull, results[0].ErrorMessage);
        }

        /// <summary>Test for invalid user (user with first name too long).</summary>
        [Test]
        public void NameTooLong()
        {
            Bonusuri bonusuri = new Bonusuri('X' + new string('x', 270), DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), true, 100, 100, 100, 100, 100, 100, 100, 100, 100);

            var context = new ValidationContext(bonusuri, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bonusuri, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNameInvalid, results[0].ErrorMessage);
        } 
        [Test]
        public void NameTooShort()
        {
            Bonusuri bonusuri = new Bonusuri("Xx", DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), true, 100, 100, 100, 100, 100, 100, 100, 100, 100);

            var context = new ValidationContext(bonusuri, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(bonusuri, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNameInvalid, results[0].ErrorMessage);
        }

        [Test]
        public void NegativeNumarMinuteNationale()
        {
            Bonusuri bonusuri = new Bonusuri("Bonus1", DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), true, -100, 100, 100, 100, 100, 100, 100, 100, 100);

            var context = new ValidationContext(bonusuri, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            Assert.IsFalse(Validator.TryValidateObject(bonusuri, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNegativeBonusConvorbireNationala, results[0].ErrorMessage);
        }

        [Test]
        public void NegativeNumarMinuteInternationale()
        {
            Bonusuri bonusuri = new Bonusuri("Bonus1", DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), true, 100, -100, 100, 100, 100, 100, 100, 100, 100);

            var context = new ValidationContext(bonusuri, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            Assert.IsFalse(Validator.TryValidateObject(bonusuri, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNegativeBonusConvorbireInternationala, results[0].ErrorMessage);
        }
        [Test]
        public void NegativeNumarMinuteReteaNegative()
        {
            Bonusuri bonusuri = new Bonusuri("Bonus1", DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), true, 100, 100, -100, 100, 100, 100, 100, 100, 100);

            var context = new ValidationContext(bonusuri, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            Assert.IsFalse(Validator.TryValidateObject(bonusuri, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNegativeBonusConvorbireRetea, results[0].ErrorMessage);
        }
        [Test]
        public void NegativeSMSNationale()
        {
            Bonusuri bonusuri = new Bonusuri("Bonus1", DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), true, 100, 100, 100, -100, 100, 100, 100, 100, 100);

            var context = new ValidationContext(bonusuri, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            Assert.IsFalse(Validator.TryValidateObject(bonusuri, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNegativeBonusSMSNationala, results[0].ErrorMessage);
        }
        [Test]
        public void NegativeSMSInternationale()
        {
            Bonusuri bonusuri = new Bonusuri("Bonus1", DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), true, 100, 100, 100, 100, -100, 100, 100, 100, 100);

            var context = new ValidationContext(bonusuri, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            Assert.IsFalse(Validator.TryValidateObject(bonusuri, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNegativeBonusSMSInternationala, results[0].ErrorMessage);
        }
        [Test]
        public void NegativeSMSRetea()
        {
            Bonusuri bonusuri = new Bonusuri("Bonus1", DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), true, 100, 100, 100, 100, 100, -100, 100, 100, 100);

            var context = new ValidationContext(bonusuri, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            Assert.IsFalse(Validator.TryValidateObject(bonusuri, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNegativeBonusSMSRetea, results[0].ErrorMessage);
        }
        [Test]
        public void NegativeTraficDeDateNationale()
        {
            Bonusuri bonusuri = new Bonusuri("Bonus1", DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), true, 100, 100, 100, 100, 100, 100, -100, 100, 100);

            var context = new ValidationContext(bonusuri, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            Assert.IsFalse(Validator.TryValidateObject(bonusuri, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNegativeBonusTraficDeDateNationala, results[0].ErrorMessage);
        }
        [Test]
        public void NegativeTraficDeDateInternationale()
        {
            Bonusuri bonusuri = new Bonusuri("Bonus1", DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), true, 100, 100, 100, 100, 100, 100, 100, -100, 100);

            var context = new ValidationContext(bonusuri, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            Assert.IsFalse(Validator.TryValidateObject(bonusuri, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNegativeBonusTraficDeDateInternationala, results[0].ErrorMessage);
        }
        [Test]
        public void NegativeTraficDeDateRetea()
        {
            Bonusuri bonusuri = new Bonusuri("Bonus1", DateTime.Today.AddDays(1), DateTime.Today.AddDays(365), true, 100, 100, 100, 100, 100, 100, 100, 100, -100);

            var context = new ValidationContext(bonusuri, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            Assert.IsFalse(Validator.TryValidateObject(bonusuri, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNegativeBonusTraficDeDateRetea, results[0].ErrorMessage);
        }
    }
}
