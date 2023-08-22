using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models
{
    public class MinutesSMSInternet
    {
        //CONVORBIRE
        public DateTime MomentulInceperiConvorbireNationala { get; set; }
        public int DurataConvorbireNationala { get; set; }
        public DateTime MomentulInceperiConvorbireInternationala { get; set; }
        public int DurataConvorbireInternationala { get; set; }
        public DateTime MomentulInceperiConvorbireRetea { get; set; }
        public int DurataConvorbireRetea { get; set; }

        //SMS
        public int SMSNationala { get; set; }
        public int SMSInternationala { get; set; }
        public int SMSRetea { get; set; }

        //TRAFIC DE DATE IN MB
        public int TraficDeDateNationala { get; set; }
        public int TraficDeDateInternationala { get; set; }
        public int TraficDeDateRetea { get; set; }

        public MinutesSMSInternet(DateTime momentulInceperiConvorbireNationala, int durataConvorbireNationala, DateTime momentulInceperiConvorbireInternationala, int durataConvorbireInternationala, DateTime momentulInceperiConvorbireRetea, int durataConvorbireRetea, int sMSNationala, int sMSInternationala, int sMSRetea, int traficDeDateNationala, int traficDeDateInternationala, int traficDeDateRetea)
        {
            MomentulInceperiConvorbireNationala = momentulInceperiConvorbireNationala;
            DurataConvorbireNationala = durataConvorbireNationala;
            MomentulInceperiConvorbireInternationala = momentulInceperiConvorbireInternationala;
            DurataConvorbireInternationala = durataConvorbireInternationala;
            MomentulInceperiConvorbireRetea = momentulInceperiConvorbireRetea;
            DurataConvorbireRetea = durataConvorbireRetea;
            SMSNationala = sMSNationala;
            SMSInternationala = sMSInternationala;
            SMSRetea = sMSRetea;
            TraficDeDateNationala = traficDeDateNationala;
            TraficDeDateInternationala = traficDeDateInternationala;
            TraficDeDateRetea = traficDeDateRetea;
        }
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
