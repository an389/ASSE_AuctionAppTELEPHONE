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
    internal class AbonamentValidation
    {
        private const string LogNullName = "[Name] cannot be null.";

        private const string LogTooLongName = "[Name] must be between 1 and 250 characters.";

        private const string LogNullPret = "[Pret] cannot be null.";

        private const string LogNegativePret = "[Pret] cannot be negative.";

        private const string LogStartDateNull = "[StartDate] cannot be null.";

        private const string LogStartDateLaterThanToday= "Start date must be later than today 1 AM";

        private const string LogEndDateNull = "[EndDate] cannot be null.";

        private const string LogEndDateLaterThanToday = "End date must be later than tomorrow";

        private const string LogActivNull = "[Activ] cannot be null.";

        private const string NumarMinuteNationaleNull = "[NumarMinuteNationale] cannot be null.";
        private const string NumarMinuteNationaleNegative = "[NumarMinuteNationale] cannot be negative.";

        private const string NumarMinuteInternationaleNull = "[NumarMinuteInternationale] cannot be null.";
        private const string NumarMinuteInternationaleNegative = "[NumarMinuteInternationale] cannot be negative.";

        private const string NumarMinuteReteaNull = "[NumarMinuteRetea] cannot be null.";
        private const string NumarMinuteReteaNegative = "[NumarMinuteRetea] cannot be negative.";
     
        private const string SMSNationaleNull = "[SMSNationale] cannot be null.";
        private const string SMSNationaleNegative = "[SMSNationale] cannot be negative.";

        private const string SMSInternationaleNull = "[SMSInternationale] cannot be null.";
        private const string SMSInternationaleNegative = "[SMSInternationale] cannot be negative.";

        private const string SMSReteaNull = "[SMSRetea] cannot be null.";
        private const string SMSReteaNegative = "[SMSRetea] cannot be negative.";

        private const string TraficDeDateNationaleNull = "[TraficDeDateNationale] cannot be null.";
        private const string TraficDeDateNationaleNegative = "[TraficDeDateNationale] cannot be negative.";

        private const string TraficDeDateInternationaleNull = "[TraficDeDateInternationale] cannot be null.";
        private const string TraficDeDateInternationaleNegative = "[TraficDeDateInternationale] cannot be negative.";

        private const string TraficDeDateReteaNull = "[TraficDeDateRetea] cannot be null.";
        private const string TraficDeDateReteaNegative = "[TraficDeDateRetea] cannot be negative.";

        private const string BuissniesIDNegativ = "[BuissniesID] cannot be negative.";

        [Test]
        public void ValidAbonament()
        {
            Abonament abonament = new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(360), 100, 100, 100, 100, 100, 100, 100, 100, 100, 0);

            var context = new ValidationContext(abonament, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsTrue(Validator.TryValidateObject(abonament, context, results, true));
            Assert.AreEqual(0, results.Count);
        }

        [Test]
        public void InvalidUser_EmptyAbonament()
        {
            Abonament abonament = new Abonament();

            var context = new ValidationContext(abonament, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(abonament, context, results, true));
            Assert.AreEqual(3, results.Count);
            Assert.AreEqual(LogNullName, results[0].ErrorMessage);
            Assert.AreEqual(LogStartDateLaterThanToday, results[1].ErrorMessage);
        }

        [Test]
        public void InvalidUser_Name_Null()
        {
            Abonament abonament = new Abonament(null, 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(360), 100, 100, 100, 100, 100, 100, 100, 100, 100, 0);

            var context = new ValidationContext(abonament, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(abonament, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNullName, results[0].ErrorMessage);
        }

        [Test]
        public void InvalidAbonament_TooLongName()
        {
            Abonament abonament = new Abonament(new string('x', 255), 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(360), 100, 100, 100, 100, 100, 100, 100, 100, 100, 0) ;

            var context = new ValidationContext(abonament, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(abonament, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogTooLongName, results[0].ErrorMessage);
        }

        [Test]
        public void InvalidAbonamentNegativePret()
        {
            Abonament abonament = new Abonament("Abonament0", -100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(360), 100, 100, 100, 100, 100, 100, 100, 100, 100, 0);

            var context = new ValidationContext(abonament, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(abonament, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogNegativePret, results[0].ErrorMessage);
        }

        [Test]
        public void InvalidStartDate()
        {
            Abonament abonament = new Abonament("Abonament0", 100, DateTime.Today, DateTime.Today.AddDays(365), 100, 100, 100, 100, 100, 100, 100, 100, 100, 0);

            var context = new ValidationContext(abonament, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(abonament, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogStartDateLaterThanToday, results[0].ErrorMessage);
        }

        [Test]
        public void InvalidEndtDate()
        {
            Abonament abonament = new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today, 100, 100, 100, 100, 100, 100, 100, 100, 100, 0);

            var context = new ValidationContext(abonament, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(abonament, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(LogEndDateLaterThanToday, results[0].ErrorMessage);
        }

        [Test]
        public void NegativeNumarMinuteNationale()
        {
            Abonament abonament = new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(360), -100, 100, 100, 100, 100, 100, 100, 100, 100, 0);

            var context = new ValidationContext(abonament, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(abonament, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(NumarMinuteNationaleNegative, results[0].ErrorMessage);
        }

        [Test]
        public void NegativeNumarMinuteInternationale()
        {
            Abonament abonament = new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(360), 100, -100, 100, 100, 100, 100, 100, 100, 100, 0);

            var context = new ValidationContext(abonament, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(abonament, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(NumarMinuteInternationaleNegative, results[0].ErrorMessage);
        }
        [Test]
        public void NegativeNumarMinuteReteaNegative()
        {
            Abonament abonament = new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(360), 100, 100, -100, 100, 100, 100, 100, 100, 100, 0);

            var context = new ValidationContext(abonament, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(abonament, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(NumarMinuteReteaNegative, results[0].ErrorMessage);
        }
        [Test]
        public void NegativeSMSNationale()
        {
            Abonament abonament = new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(360), 100, 100, 100, -100, 100, 100, 100, 100, 100, 0);

            var context = new ValidationContext(abonament, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(abonament, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(SMSNationaleNegative, results[0].ErrorMessage);
        }
        [Test]
        public void NegativeSMSInternationale()
        {
            Abonament abonament = new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(360), 100, 100, 100, 100, -100, 100, 100, 100, 100, 0);

            var context = new ValidationContext(abonament, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(abonament, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(SMSInternationaleNegative, results[0].ErrorMessage);
        }
        [Test]
        public void NegativeSMSRetea()
        {
            Abonament abonament = new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(360), 100, 100, 100, 100, 100, -100, 100, 100, 100, 0);

            var context = new ValidationContext(abonament, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(abonament, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(SMSReteaNegative, results[0].ErrorMessage);
        }
        [Test]
        public void NegativeTraficDeDateNationale()
        {
            Abonament abonament = new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(360), 100, 100, 100, 100, 100, 100, -100, 100, 100, 0);

            var context = new ValidationContext(abonament, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(abonament, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(TraficDeDateNationaleNegative, results[0].ErrorMessage);
        }
        [Test]
        public void NegativeTraficDeDateInternationale()
        {
            Abonament abonament = new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(360), 100, 100, 100, 100, 100, 100, 100, -100, 100, 0);

            var context = new ValidationContext(abonament, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(abonament, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(TraficDeDateInternationaleNegative, results[0].ErrorMessage);
        }
        [Test]
        public void NegativeTraficDeDateRetea()
        {
            Abonament abonament = new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(360), 100, 100, 100, 100, 100, 100, 100, 100, -100, 0);

            var context = new ValidationContext(abonament, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(abonament, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(TraficDeDateReteaNegative, results[0].ErrorMessage);
        }
        [Test]
        public void NegativBuissniesID()
        {
            Abonament abonament = new Abonament("Abonament0", 100, DateTime.Today.AddDays(1), DateTime.Today.AddDays(360), 100, 100, 100, 100, 100, 100, 100, 100, 100, -1);

            var context = new ValidationContext(abonament, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(abonament, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(BuissniesIDNegativ, results[0].ErrorMessage);
        }
    }
}
