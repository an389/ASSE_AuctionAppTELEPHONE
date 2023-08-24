using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models
{
    public class CentralaTelefonica
    {

        [ExcludeFromCodeCoverage]
        public int Id { get; private set; }
        [Required(ErrorMessage = "[ClientEmail] cannot be null.")]
        [EmailAddress(ErrorMessage = "[ClientEmail] is not a valid email address.")]
        [StringLength(maximumLength: 50, MinimumLength = 5, ErrorMessage = "[ClientEmail] must have between 5 and 50 digits.")]
        public string ClientEmail { get; private set; }
        //CONVORBIRE
        [Required(ErrorMessage = "[MomentulInceperiConvorbireNationala] cannot be null.")]
        public DateTime MomentulInceperiConvorbireNationala { get; set; }
        [Required(ErrorMessage = "[DurataConvorbireNationala] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[DurataConvorbireNationala] cannot be negative.")]
        public int DurataConvorbireNationala { get; set; }

        [Required(ErrorMessage = "[MomentulInceperiConvorbireInternationala] cannot be null.")]
        public DateTime MomentulInceperiConvorbireInternationala { get; set; }
        [Required(ErrorMessage = "[DurataConvorbireInternationala] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[DurataConvorbireInternationala] cannot be negative.")]
        public int DurataConvorbireInternationala { get; set; }

        [Required(ErrorMessage = "[MomentulInceperiConvorbireRetea] cannot be null.")]
        public DateTime MomentulInceperiConvorbireRetea { get; set; }
        [Required(ErrorMessage = "[DurataConvorbireRetea] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[DurataConvorbireRetea] cannot be negative.")]
        public int DurataConvorbireRetea { get; set; }

        //SMS
        [Required(ErrorMessage = "[SMSNationalaDate] cannot be null.")]
        public DateTime SMSNationalaDate { get; set; }
        [Required(ErrorMessage = "[SMSNationala] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[SMSNationala] cannot be negative.")]
        public int SMSNationala { get; set; }

        [Required(ErrorMessage = "[SMSInternationalaDate] cannot be null.")]
        public DateTime SMSInternationalaDate { get; set; }
        [Required(ErrorMessage = "[SMSInternationala] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[SMSInternationala] cannot be negative.")]
        public int SMSInternationala { get; set; }

        [Required(ErrorMessage = "[SMSReteaDate] cannot be null.")]
        public DateTime SMSReteaDate { get; set; }
        [Required(ErrorMessage = "[SMSRetea] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[SMSRetea] cannot be negative.")]
        public int SMSRetea { get; set; }

        //TRAFIC DE DATE IN MB
        [Required(ErrorMessage = "[TraficDeDateNationalaDate] cannot be null.")]
        public DateTime TraficDeDateNationalaDate { get; set; }
        [Required(ErrorMessage = "[TraficDeDateNationala] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[TraficDeDateNationala] cannot be negative.")]
        public int TraficDeDateNationala { get; set; }

        [Required(ErrorMessage = "[TraficDeDateInternationalaDate] cannot be null.")]
        public DateTime TraficDeDateInternationalaDate { get; set; }
        [Required(ErrorMessage = "[TraficDeDateInternationala] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[TraficDeDateInternationala] cannot be negative.")]
        public int TraficDeDateInternationala { get; set; }

        [Required(ErrorMessage = "[TraficDeDateReteaDate] cannot be null.")]
        public DateTime TraficDeDateReteaDate { get; set; }
        [Required(ErrorMessage = "[TraficDeDateRetea] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[TraficDeDateRetea] cannot be negative.")]
        public int TraficDeDateRetea { get; set; }

        [ExcludeFromCodeCoverage]
        public CentralaTelefonica()
        {
        }

        public CentralaTelefonica(string clientEmail, DateTime momentulInceperiConvorbireNationala, int durataConvorbireNationala, DateTime momentulInceperiConvorbireInternationala, int durataConvorbireInternationala, DateTime momentulInceperiConvorbireRetea, int durataConvorbireRetea, DateTime sMSNationalaDate, int sMSNationala, DateTime sMSInternationalaDate, int sMSInternationala, DateTime sMSReteaDate, int sMSRetea, DateTime traficDeDateNationalaDate, int traficDeDateNationala, DateTime traficDeDateInternationalaDate, int traficDeDateInternationala, DateTime traficDeDateReteaDate, int traficDeDateRetea)
        {
            ClientEmail = clientEmail;
            MomentulInceperiConvorbireNationala = momentulInceperiConvorbireNationala;
            DurataConvorbireNationala = durataConvorbireNationala;
            MomentulInceperiConvorbireInternationala = momentulInceperiConvorbireInternationala;
            DurataConvorbireInternationala = durataConvorbireInternationala;
            MomentulInceperiConvorbireRetea = momentulInceperiConvorbireRetea;
            DurataConvorbireRetea = durataConvorbireRetea;
            SMSNationalaDate = sMSNationalaDate;
            SMSNationala = sMSNationala;
            SMSInternationalaDate = sMSInternationalaDate;
            SMSInternationala = sMSInternationala;
            SMSReteaDate = sMSReteaDate;
            SMSRetea = sMSRetea;
            TraficDeDateNationalaDate = traficDeDateNationalaDate;
            TraficDeDateNationala = traficDeDateNationala;
            TraficDeDateInternationalaDate = traficDeDateInternationalaDate;
            TraficDeDateInternationala = traficDeDateInternationala;
            TraficDeDateReteaDate = traficDeDateReteaDate;
            TraficDeDateRetea = traficDeDateRetea;
        }
    }
}
