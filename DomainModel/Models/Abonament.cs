using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models
{
    public class Abonament
    {
        public int Id { get; private set; }
        [Required(ErrorMessage = "[Name] cannot be null.")]
        [StringLength(maximumLength: 250, MinimumLength = 4, ErrorMessage = "[Name] must be between 1 and 250 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "[Pret] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[Pret] cannot be negative.")]
        public int Pret { get; set; }
        [Required(ErrorMessage = "[CreationDate] cannot be null.")]
        [CustomValidation(typeof(Abonament), "isValidStartDate", ErrorMessage = "StartDate wrong ")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "[CreationDate] cannot be null.")]
        [CustomValidation(typeof(Abonament), "isValidEndtDate", ErrorMessage = "EndDate wrong ")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "[Activ] cannot be null.")]
        public bool Activ { get; set; }
        [Required(ErrorMessage = "[NumarMinuteNationale] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[NumarMinuteNationale] cannot be negative.")]
        public int NumarMinuteNationale { get; set; }
        [Required(ErrorMessage = "[NumarMinuteInternationale] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[NumarMinuteInternationale] cannot be negative.")]
        public int NumarMinuteInternationale { get; set; }
        [Required(ErrorMessage = "[NumarMinuteRetea] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[NumarMinuteRetea] cannot be negative.")]
        public int NumarMinuteRetea { get; set; }
        [Required(ErrorMessage = "[SMSNationale] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[SMSNationale] cannot be negative.")]
        public int SMSNationale { get; set; }
        [Required(ErrorMessage = "[SMSInternationale] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[SMSInternationale] cannot be negative.")]
        public int SMSInternationale { get; set; }
        [Required(ErrorMessage = "[SMSRetea] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[SMSRetea] cannot be negative.")]
        public int SMSRetea { get; set; }
        [Required(ErrorMessage = "[TraficDeDateNationale] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[TraficDeDateNationale] cannot be negative.")]
        public int TraficDeDateNationale { get; set; }
        [Required(ErrorMessage = "[TraficDeDateInternationale] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[TraficDeDateInternationale] cannot be negative.")]
        public int TraficDeDateInternationale { get; set; }
        [Required(ErrorMessage = "[TraficDeDateRetea] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[TraficDeDateRetea] cannot be negative.")]
        public int TraficDeDateRetea { get; set; }
        [Required(ErrorMessage = "[BuissniesID] cannot be null.")]
        public int BuissniesID { get; set; }
        public Abonament(string name, int pret, DateTime startDate, DateTime endDate, int numarMinuteNationale, int numarMinuteInternationale, int numarMinuteRetea, int sMSNationale, int sMSInternationale, int sMSRetea, int traficDeDateNationale, int traficDeDateInternationale, int traficDeDateRetea, int buissniesID)
        {
            Name = name;
            Pret = pret;
            StartDate = startDate;
            EndDate = endDate;
            NumarMinuteNationale = numarMinuteNationale;
            NumarMinuteInternationale = numarMinuteInternationale;
            NumarMinuteRetea = numarMinuteRetea;
            SMSNationale = sMSNationale;
            SMSInternationale = sMSInternationale;
            SMSRetea = sMSRetea;
            TraficDeDateNationale = traficDeDateNationale;
            TraficDeDateInternationale = traficDeDateInternationale;
            TraficDeDateRetea = traficDeDateRetea;
            Activ = true;
            BuissniesID = buissniesID;
        }
        public Abonament()
        {
        }
    
        public static ValidationResult isValidStartDate(DateTime dateTime, ValidationContext context)
        {
            return DateTime.Today.Day > dateTime.Day + 1 ? new ValidationResult("Start date must be later than today 1 AM") : ValidationResult.Success;
        }

        public static ValidationResult isValidEndtDate(DateTime dateTime, ValidationContext context)
        {
            return DateTime.Now.Day < dateTime.Day + 2 ? new ValidationResult("Start date must be later than tomorrow") : ValidationResult.Success;
        }
    }
}
