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
    internal class MinutesSMSInternetValidation
    {
        private const string DurataConvorbireNationala = "[DurataConvorbireNationala] cannot be negative.";
        private const string DurataConvorbireInternationala = "[DurataConvorbireInternationala] cannot be negative.";
        private const string DurataConvorbireRetea = "[DurataConvorbireRetea] cannot be negative.";
        private const string SMSNationala = "[SMSNationala] cannot be negative.";
        private const string SMSInternationala = "[SMSInternationala] cannot be negative.";
        private const string SMSRetea = "[SMSRetea] cannot be negative.";
        private const string TraficDeDateNationala = "[TraficDeDateNationala] cannot be negative.";
        private const string TraficDeDateInternationala = "[TraficDeDateInternationala] cannot be negative.";
        private const string TraficDeDateRetea = "[TraficDeDateRetea] cannot be negative.";

        [Test]
        public void ValidCentralaTelefonicaValidation()
        {
            MinutesSMSInternet minutesSMSInternet = new MinutesSMSInternet(1, 1, 1, 1, 1, 1, 1, 1, 1);
            var context = new ValidationContext(minutesSMSInternet, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsTrue(Validator.TryValidateObject(minutesSMSInternet, context, results, true));
            Assert.AreEqual(0, results.Count);
        }

        [Test]
        public void InvalidNegativeNumarMinuteNationale()
        {
            MinutesSMSInternet minutesSMSInternet = new MinutesSMSInternet(-1, 1, 1, 1, 1, 1, 1, 1, 1);
            var context = new ValidationContext(minutesSMSInternet, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(minutesSMSInternet, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(DurataConvorbireNationala, results[0].ErrorMessage);
        }

        [Test]
        public void InvalidNegativeNumarMinuteInternationale()
        {
            MinutesSMSInternet minutesSMSInternet = new MinutesSMSInternet(1, -1, 1, 1, 1, 1, 1, 1, 1);
            var context = new ValidationContext(minutesSMSInternet, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(minutesSMSInternet, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(DurataConvorbireInternationala, results[0].ErrorMessage);
        }
        [Test]
        public void InvalidNegativeNumarMinuteReteaNegative()
        {
            MinutesSMSInternet minutesSMSInternet = new MinutesSMSInternet(1, 1, -1, 1, 1, 1, 1, 1, 1);
            var context = new ValidationContext(minutesSMSInternet, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(minutesSMSInternet, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(DurataConvorbireRetea, results[0].ErrorMessage);
        }
        [Test]
        public void InvalidNegativeSMSNationale()
        {
            MinutesSMSInternet minutesSMSInternet = new MinutesSMSInternet(1, 1, 1, -1, 1, 1, 1, 1, 1);
            var context = new ValidationContext(minutesSMSInternet, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(minutesSMSInternet, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(SMSNationala, results[0].ErrorMessage);
        }
        [Test]
        public void InvalidNegativeSMSInternationale()
        {
            MinutesSMSInternet minutesSMSInternet = new MinutesSMSInternet(1, 1, 1, 1, -1, 1, 1, 1, 1);
            var context = new ValidationContext(minutesSMSInternet, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(minutesSMSInternet, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(SMSInternationala, results[0].ErrorMessage);
        }
        [Test]
        public void InvalidNegativeSMSRetea()
        {
            MinutesSMSInternet minutesSMSInternet = new MinutesSMSInternet(1, 1, 1, 1, 1, -1, 1, 1, 1);
            var context = new ValidationContext(minutesSMSInternet, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(minutesSMSInternet, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(SMSRetea, results[0].ErrorMessage);
        }
        [Test]
        public void InvalidNegativeTraficDeDateNationale()
        {
            MinutesSMSInternet minutesSMSInternet = new MinutesSMSInternet(1, 1, 1, 1, 1, 1, -1, 1, 1);
            var context = new ValidationContext(minutesSMSInternet, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(minutesSMSInternet, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(TraficDeDateNationala, results[0].ErrorMessage);
        }
        [Test]
        public void InvalidNegativeTraficDeDateInternationale()
        {
            MinutesSMSInternet minutesSMSInternet = new MinutesSMSInternet(1, 1, 1, 1, 1, 1, 1, -1, 1);
            var context = new ValidationContext(minutesSMSInternet, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(minutesSMSInternet, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(TraficDeDateInternationala, results[0].ErrorMessage);
        }
        [Test]
        public void InvalidNegativeTraficDeDateRetea()
        {
            MinutesSMSInternet minutesSMSInternet = new MinutesSMSInternet(1, 1, 1, 1, 1, 1, 1, 1, -1);
            var context = new ValidationContext(minutesSMSInternet, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(minutesSMSInternet, context, results, true));
            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(TraficDeDateRetea, results[0].ErrorMessage);
        }
    }
}
