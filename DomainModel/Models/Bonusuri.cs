using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models
{
    public class Bonusuri
    {
        public int Id { get; private set; }
        [Required(ErrorMessage = "[CreationDate] cannot be null.")]
        [CustomValidation(typeof(Bonusuri), "isValidStartDate", ErrorMessage = "StartDate wrong ")]
        public DateTime? StartDate { get; set; }
        [Required(ErrorMessage = "[CreationDate] cannot be null.")]
        [CustomValidation(typeof(Bonusuri), "isValidEndtDate", ErrorMessage = "ENdDate wrong ")]
        public DateTime? EndDate { get; set; }
        [Required(ErrorMessage = "[Active] cannot be null.")]
        public bool Active { get; set; }
        [Required(ErrorMessage = "[Name] cannot be null.")]
        [StringLength(maximumLength: 250, MinimumLength = 4, ErrorMessage = "[Name] must be between 1 and 250 characters.")]
        public string Name { get; set; }
        //CONVORBIRE
        [Required(ErrorMessage = "[BonusConvorbireNationala] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[BonusConvorbireNationala] cannot be negative.")]
        public int BonusConvorbireNationala { get; set; }
        [Required(ErrorMessage = "[BonusConvorbireInternationala] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[BonusConvorbireInternationala] cannot be negative.")]
        public int BonusConvorbireInternationala { get; set; }
        [Required(ErrorMessage = "[BonusConvorbireRetea] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[BonusConvorbireRetea] cannot be negative.")]
        public int BonusConvorbireRetea { get; set; }
        //SMS
        [Required(ErrorMessage = "[BonusSMSNationala] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[BonusSMSNationala] cannot be negative.")]
        public int BonusSMSNationala { get; set; }
        [Required(ErrorMessage = "[BonusSMSInternationala] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[BonusSMSInternationala] cannot be negative.")]
        public int BonusSMSInternationala { get; set; }
        [Required(ErrorMessage = "[BonusSMSRetea] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[BonusSMSRetea] cannot be negative.")]
        public int BonusSMSRetea { get; set; }
        //TRAFIC DE DATE IN MB
        [Required(ErrorMessage = "[BonusTraficDeDateNationala] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[BonusTraficDeDateNationala] cannot be negative.")]
        public int BonusTraficDeDateNationala { get; set; }
        [Required(ErrorMessage = "[BonusTraficDeDateInternationala] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[BonusTraficDeDateInternationala] cannot be negative.")]
        public int BonusTraficDeDateInternationala { get; set; }
        [Required(ErrorMessage = "[BonusTraficDeDateRetea] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[BonusTraficDeDateRetea] cannot be negative.")]
        public int BonusTraficDeDateRetea { get; set; }

        public Bonusuri(string name, DateTime startDate, DateTime endDate, bool active, int bonusConvorbireNationala, int bonusConvorbireInternationala, int bonusConvorbireRetea, int bonusSMSNationala, int bonusSMSInternationala, int bonusSMSRetea, int bonusTraficDeDateNationala, int bonusTraficDeDateInternationala, int bonusTraficDeDateRetea)
        {
            if (active)
            {
                Name = name;
                StartDate = startDate;
                EndDate = endDate;
                Active = active;
                BonusConvorbireNationala = bonusConvorbireNationala;
                BonusConvorbireInternationala = bonusConvorbireInternationala;
                BonusConvorbireRetea = bonusConvorbireRetea;
                BonusSMSNationala = bonusSMSNationala;
                BonusSMSInternationala = bonusSMSInternationala;
                BonusSMSRetea = bonusSMSRetea;
                BonusTraficDeDateNationala = bonusTraficDeDateNationala;
                BonusTraficDeDateInternationala = bonusTraficDeDateInternationala;
                BonusTraficDeDateRetea = bonusTraficDeDateRetea;
            }
            else
            {
                Name = null;
                StartDate = null;
                EndDate = null;
                Active = active;
                BonusConvorbireNationala = 0;
                BonusConvorbireInternationala = 0;
                BonusConvorbireRetea = 0;
                BonusSMSNationala = 0;
                BonusSMSInternationala = 0;
                BonusSMSRetea = 0;
                BonusTraficDeDateNationala = 0;
                BonusTraficDeDateInternationala = 0;
                BonusTraficDeDateRetea = 0;
            }
           
        }
        public Bonusuri()
        {
        }
        public static ValidationResult isValidStartDate(DateTime dateTime, ValidationContext context)
        {
            return DateTime.Today.Day > dateTime.Day + 1 ? new ValidationResult("Start date must be later than today 1 AM") : ValidationResult.Success;
        }

        public static ValidationResult isValidEndtDate(DateTime dateTime, ValidationContext context)
        {
            return DateTime.Now.Day < dateTime.Day + 2 ? new ValidationResult("End date must be later than tomorrow") : ValidationResult.Success;
        }
    }
}
