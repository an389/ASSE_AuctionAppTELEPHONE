using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models
{
    [ExcludeFromCodeCoverage]
    public class MinutesSMSInternet
    {
        //CONVORBIRE
        public DateTime MomentulInceperiConvorbireNationala { get; set; }
        [Range(0.0, double.MaxValue, ErrorMessage = "[DurataConvorbireNationala] cannot be negative.")]
        public int DurataConvorbireNationala { get; set; }
        public DateTime MomentulInceperiConvorbireInternationala { get; set; }
        [Range(0.0, double.MaxValue, ErrorMessage = "[DurataConvorbireInternationala] cannot be negative.")]
        public int DurataConvorbireInternationala { get; set; }
        public DateTime MomentulInceperiConvorbireRetea { get; set; }
        [Range(0.0, double.MaxValue, ErrorMessage = "[DurataConvorbireRetea] cannot be negative.")]
        public int DurataConvorbireRetea { get; set; }

        //SMS
        [Range(0.0, double.MaxValue, ErrorMessage = "[SMSNationala] cannot be negative.")]
        public int SMSNationala { get; set; }
        [Range(0.0, double.MaxValue, ErrorMessage = "[SMSInternationala] cannot be negative.")]
        public int SMSInternationala { get; set; }
        [Range(0.0, double.MaxValue, ErrorMessage = "[SMSRetea] cannot be negative.")]
        public int SMSRetea { get; set; }

        //TRAFIC DE DATE IN MB
        [Range(0.0, double.MaxValue, ErrorMessage = "[TraficDeDateNationala] cannot be negative.")]
        public int TraficDeDateNationala { get; set; }
        [Range(0.0, double.MaxValue, ErrorMessage = "[TraficDeDateInternationala] cannot be negative.")]
        public int TraficDeDateInternationala { get; set; }
        [Range(0.0, double.MaxValue, ErrorMessage = "[TraficDeDateRetea] cannot be negative.")]
        public int TraficDeDateRetea { get; set; }

        public MinutesSMSInternet(int durataConvorbireNationala, int durataConvorbireInternationala, int durataConvorbireRetea, int sMSNationala, int sMSInternationala, int sMSRetea, int traficDeDateNationala, int traficDeDateInternationala, int traficDeDateRetea)
        {
            DurataConvorbireNationala = durataConvorbireNationala;
            DurataConvorbireInternationala = durataConvorbireInternationala;
            DurataConvorbireRetea = durataConvorbireRetea;
            SMSNationala = sMSNationala;
            SMSInternationala = sMSInternationala;
            SMSRetea = sMSRetea;
            TraficDeDateNationala = traficDeDateNationala;
            TraficDeDateInternationala = traficDeDateInternationala;
            TraficDeDateRetea = traficDeDateRetea;
        }
    }
}
