using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models
{
    public class Factura
    {
      

        public int Id { get; private set; }
        public string ClientEmail { get; set; }
        [Required(ErrorMessage = "[TVA] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[TVA] cannot be negative.")]
        public double TVA { get; set; }
        [Required(ErrorMessage = "[PretTotal] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[PretTotal] cannot be negative.")]
        public double PretTotal { get; set; }
        [Required(ErrorMessage = "[Achitat] cannot be null.")]
        public bool Achitat { get; set; }

        //CONVORBIRE
        public DateTime MomentulInceperiConvorbireNationala { get; set; }
        [Required(ErrorMessage = "[DurataConvorbireNationala] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[DurataConvorbireNationala] cannot be negative.")]
        public int DurataConvorbireNationala { get; set; }
        [Required(ErrorMessage = "[PretConvorbireNationala] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[PretConvorbireNationala] cannot be negative.")]
        public int PretConvorbireNationala { get; set; }
        [Required(ErrorMessage = "[MomentulInceperiConvorbireInternationala] cannot be null.")]
        public DateTime MomentulInceperiConvorbireInternationala { get; set; }
        [Required(ErrorMessage = "[DurataConvorbireInternationala] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[DurataConvorbireInternationala] cannot be negative.")]
        public int DurataConvorbireInternationala { get; set; }
        [Required(ErrorMessage = "[PretConvorbireInternationala] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[PretConvorbireInternationala] cannot be negative.")]
        public int PretConvorbireInternationala { get; set; }
        [Required(ErrorMessage = "[MomentulInceperiConvorbireRetea] cannot be null.")]
        public DateTime MomentulInceperiConvorbireRetea { get; set; }
        [Required(ErrorMessage = "[DurataConvorbireRetea] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[DurataConvorbireRetea] cannot be negative.")]
        public int DurataConvorbireRetea { get; set; }
        [Required(ErrorMessage = "[PretConvorbireRetea] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[PretConvorbireRetea] cannot be negative.")]
        public int PretConvorbireRetea { get; set; }

        //SMS
        [Required(ErrorMessage = "[SMSNationala] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[SMSNationala] cannot be negative.")]
        public int SMSNationala { get; set; }
        [Required(ErrorMessage = "[PretSMSNationala] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[PretSMSNationala] cannot be negative.")]
        public int PretSMSNationala { get; set; }
        [Required(ErrorMessage = "[SMSInternationala] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[SMSInternationala] cannot be negative.")]
        public int SMSInternationala { get; set; }
        [Required(ErrorMessage = "[PretSMSInternationala] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[PretSMSInternationala] cannot be negative.")]
        public int PretSMSInternationala { get; set; }
        [Required(ErrorMessage = "[SMSRetea] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[SMSRetea] cannot be negative.")]
        public int SMSRetea { get; set; }
        [Required(ErrorMessage = "[PretSMSRetea] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[PretSMSRetea] cannot be negative.")]
        public int PretSMSRetea { get; set; }

        //TRAFIC DE DATE IN MB
        [Required(ErrorMessage = "[TraficDeDateNationala] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[TraficDeDateNationala] cannot be negative.")]
        public int TraficDeDateNationala { get; set; }
        [Required(ErrorMessage = "[PretTraficDeDateNationala] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[PretTraficDeDateNationala] cannot be negative.")]
        public int PretTraficDeDateNationala { get; set; }
        [Required(ErrorMessage = "[TraficDeDateInternationala] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[TraficDeDateInternationala] cannot be negative.")]
        public int TraficDeDateInternationala { get; set; }
        [Required(ErrorMessage = "[PretTraficDeDateInternationala] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[PretTraficDeDateInternationala] cannot be negative.")]
        public int PretTraficDeDateInternationala { get; set; }
        [Required(ErrorMessage = "[TraficDeDateRetea] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[TraficDeDateRetea] cannot be negative.")]
        public int TraficDeDateRetea { get; set; }
        [Required(ErrorMessage = "[PretTraficDeDateRetea] cannot be null.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "[PretTraficDeDateRetea] cannot be negative.")]
        public int PretTraficDeDateRetea { get; set; }

        public Factura(string clientEmail, double tVA, int pretTotal, bool achitat, DateTime momentulInceperiConvorbireNationala, int durataConvorbireNationala, int pretConvorbireNationala, DateTime momentulInceperiConvorbireInternationala, int durataConvorbireInternationala, int pretConvorbireInternationala, DateTime momentulInceperiConvorbireRetea, int durataConvorbireRetea, int pretConvorbireRetea, int sMSNationala, int pretSMSNationala, int sMSInternationala, int pretSMSInternationala, int sMSRetea, int pretSMSRetea, int traficDeDateNationala, int pretTraficDeDateNationala, int traficDeDateInternationala, int pretTraficDeDateInternationala, int traficDeDateRetea, int pretTraficDeDateRetea)
        {
            ClientEmail = clientEmail;
            TVA = tVA;
            PretTotal = pretTotal;
            Achitat = achitat;
            MomentulInceperiConvorbireNationala = momentulInceperiConvorbireNationala;
            DurataConvorbireNationala = durataConvorbireNationala;
            PretConvorbireNationala = pretConvorbireNationala;
            MomentulInceperiConvorbireInternationala = momentulInceperiConvorbireInternationala;
            DurataConvorbireInternationala = durataConvorbireInternationala;
            PretConvorbireInternationala = pretConvorbireInternationala;
            MomentulInceperiConvorbireRetea = momentulInceperiConvorbireRetea;
            DurataConvorbireRetea = durataConvorbireRetea;
            PretConvorbireRetea = pretConvorbireRetea;
            SMSNationala = sMSNationala;
            PretSMSNationala = pretSMSNationala;
            SMSInternationala = sMSInternationala;
            PretSMSInternationala = pretSMSInternationala;
            SMSRetea = sMSRetea;
            PretSMSRetea = pretSMSRetea;
            TraficDeDateNationala = traficDeDateNationala;
            PretTraficDeDateNationala = pretTraficDeDateNationala;
            TraficDeDateInternationala = traficDeDateInternationala;
            PretTraficDeDateInternationala = pretTraficDeDateInternationala;
            TraficDeDateRetea = traficDeDateRetea;
            PretTraficDeDateRetea = pretTraficDeDateRetea;
        }
        public Factura()
        {
        }

      
    }
}
