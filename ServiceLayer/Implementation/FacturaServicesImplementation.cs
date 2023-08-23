using DataMapper.Interfaces;
using DomainModel.Models;
using log4net;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Implementation
{
    public class FacturaServicesImplementation : IFacturaService
    {
        private ILog logger;
        private IFacturaDataService facturaDataService;

        public FacturaServicesImplementation(IFacturaDataService facturaDataService, ILog logger)
        {
            this.logger = logger;
            this.facturaDataService = facturaDataService;
        }
        public bool AddFactura(Factura factura)
        {
            if (factura != null)
            {
                var context = new ValidationContext(factura, serviceProvider: null, items: null);
                var results = new List<ValidationResult>();

                if (Validator.TryValidateObject(factura, context, results, true))
                {
                    return this.facturaDataService.AddFactura(factura);

                }
                else
                {
                    this.logger.Warn("Attempted to add an invalid factura.");
                    return false;
                }
            }
            else
            {
                this.logger.Warn("Attempted to add a null factura.");
                return false;
            }
        }


        public List<Factura> CalculareFacturiInFunctieDeLuna(int lastMonth, UtilizatorServicesImplementation utilizatorServicesImplementation, AbonamentServicesImplementation abonamentServiceImplementation, CentralaTelefonicaServicesImplementation centralaTelefonicaServicesImplementation, BuisniessServiceImplementation buisniessServiceImplementation, BonusuriServiceImplementation bonusuriServiceImplementation)
        {
            List<Factura> facturi = new List<Factura>();

            foreach (Utilizator utilizator in utilizatorServicesImplementation.GetAllUsers())
            {
                Console.WriteLine(utilizator.Emali);
                List<AbonamentUser> abonaments = new List<AbonamentUser>();

                //calculam bonusurile pentru utilizator
                List<Bonusuri> bonusuri = new List<Bonusuri>();
                string[] idsBonusuri = utilizator.BonusuriId.Split(',');
                foreach (string stringId in idsBonusuri)
                {
                    Bonusuri bonus = bonusuriServiceImplementation.GetBonusById(int.Parse(stringId));
                    if (bonus != null)
                    {
                        Console.WriteLine(" " + bonus.Name);
                        bonusuri.Add(bonus);
                    }
                }

                int pretAbonamente = 0;
                double totalTVA = 0;
                double procentDepasireValoriAbonamentTotal = 0;
                Console.WriteLine(utilizator.Id);

                foreach (AbonamentUser abonamentUser in abonamentServiceImplementation.GetAbonamentsByUserId(utilizator.Id))
                {
                    if (abonamentUser != null)
                    {
                        Console.WriteLine(abonamentUser.Name);
                        abonaments.Add(abonamentUser);
                        pretAbonamente += abonamentUser.Pret;
                        totalTVA += buisniessServiceImplementation.GetBuisniessById(abonamentUser.BuissniesID).TVA;
                        procentDepasireValoriAbonamentTotal += buisniessServiceImplementation.GetBuisniessById(abonamentUser.BuissniesID).ProcentDepasireValoriAbonament;
                    }

                }

                MinutesSMSInternet minutesSMSInternetTotalePrimiteAbonament = abonamentServiceImplementation.CalculateAllNumberOfMinutesSMSInternet(abonaments, bonusuri);

                //cat a vorbit in functie de luna, inf de la centrala telefonica
                List<CentralaTelefonica> centralaTelefonicaList = centralaTelefonicaServicesImplementation.GetUtilizatoriCentralaTelefonicaByEmail(lastMonth, utilizator.Emali);


                MinutesSMSInternet minutesSMSInternetConsumatePrinCentralaTelefonica = new MinutesSMSInternet(0, 0, 0, 0, 0, 0, 0, 0, 0); ;

                foreach (CentralaTelefonica centralaTelefonica in centralaTelefonicaList)
                {
                    minutesSMSInternetConsumatePrinCentralaTelefonica.DurataConvorbireRetea += centralaTelefonica.DurataConvorbireRetea;
                    minutesSMSInternetConsumatePrinCentralaTelefonica.DurataConvorbireNationala += centralaTelefonica.DurataConvorbireNationala;
                    minutesSMSInternetConsumatePrinCentralaTelefonica.DurataConvorbireInternationala += centralaTelefonica.DurataConvorbireInternationala;
                    minutesSMSInternetConsumatePrinCentralaTelefonica.SMSRetea += centralaTelefonica.SMSRetea;
                    minutesSMSInternetConsumatePrinCentralaTelefonica.SMSNationala += centralaTelefonica.SMSNationala;
                    minutesSMSInternetConsumatePrinCentralaTelefonica.SMSInternationala += centralaTelefonica.SMSInternationala;
                    minutesSMSInternetConsumatePrinCentralaTelefonica.TraficDeDateRetea += centralaTelefonica.TraficDeDateRetea;
                    minutesSMSInternetConsumatePrinCentralaTelefonica.TraficDeDateNationala += centralaTelefonica.TraficDeDateNationala;
                    minutesSMSInternetConsumatePrinCentralaTelefonica.TraficDeDateInternationala += centralaTelefonica.TraficDeDateInternationala;
                }
                
                double tVA = totalTVA / abonaments.Count;
                double procentDepasireValoriAbonament = procentDepasireValoriAbonamentTotal / abonaments.Count;
                if (abonaments.Count == 0)
                {
                    tVA = totalTVA;
                    procentDepasireValoriAbonament = procentDepasireValoriAbonamentTotal;
                }

                facturi.Add(new Factura
                {

                    ClientEmail = utilizator.Emali,
                    TVA = tVA,
                    PretTotal = this.PretTotal(tVA, pretAbonamente, procentDepasireValoriAbonament, minutesSMSInternetTotalePrimiteAbonament, minutesSMSInternetConsumatePrinCentralaTelefonica),
                    Achitat = false,
                });
               
            }

         /*   foreach(Factura factura in facturi)
            {
                Console.WriteLine(factura.PretTotal);
            }*/

            return facturi;
        }

        public double PretTotal(double tva, int pretAbonamente, double procentDepasireValoriAbonament, MinutesSMSInternet minutesSMSInternetTotalePrimiteAbonament, MinutesSMSInternet minutesSMSInternetConsumatePrinCentralaTelefonica)
        {
        double pretTotalDepasireAbonament = 0;
        //calculam cate minute ii mai raman dupa ce se scad cele de la cdentrala din abonament
        MinutesSMSInternet minutesSMSInternetCalcTot = new MinutesSMSInternet(0, 0, 0, 0, 0, 0, 0, 0, 0)
        {
            DurataConvorbireRetea = minutesSMSInternetTotalePrimiteAbonament.DurataConvorbireRetea - minutesSMSInternetConsumatePrinCentralaTelefonica.DurataConvorbireRetea,
            DurataConvorbireNationala = minutesSMSInternetTotalePrimiteAbonament.DurataConvorbireNationala - minutesSMSInternetConsumatePrinCentralaTelefonica.DurataConvorbireNationala,
            DurataConvorbireInternationala = minutesSMSInternetTotalePrimiteAbonament.DurataConvorbireInternationala - minutesSMSInternetConsumatePrinCentralaTelefonica.DurataConvorbireInternationala,
            SMSRetea = minutesSMSInternetTotalePrimiteAbonament.SMSRetea - minutesSMSInternetConsumatePrinCentralaTelefonica.SMSRetea,
            SMSNationala = minutesSMSInternetTotalePrimiteAbonament.SMSNationala - minutesSMSInternetTotalePrimiteAbonament.SMSNationala,
            SMSInternationala = minutesSMSInternetTotalePrimiteAbonament.SMSInternationala - minutesSMSInternetConsumatePrinCentralaTelefonica.SMSInternationala,
            TraficDeDateRetea = minutesSMSInternetTotalePrimiteAbonament.TraficDeDateRetea - minutesSMSInternetConsumatePrinCentralaTelefonica.TraficDeDateRetea,
            TraficDeDateNationala = minutesSMSInternetTotalePrimiteAbonament.TraficDeDateNationala - minutesSMSInternetConsumatePrinCentralaTelefonica.TraficDeDateNationala,
            TraficDeDateInternationala = minutesSMSInternetTotalePrimiteAbonament.TraficDeDateInternationala - minutesSMSInternetConsumatePrinCentralaTelefonica.TraficDeDateInternationala,
        };

        pretTotalDepasireAbonament =
            ((minutesSMSInternetCalcTot.DurataConvorbireRetea > 0) ? 0 : (minutesSMSInternetCalcTot.DurataConvorbireRetea * (-1) * (procentDepasireValoriAbonament * pretAbonamente) / 100)) +
            ((minutesSMSInternetCalcTot.DurataConvorbireNationala > 0) ? 0 : (minutesSMSInternetCalcTot.DurataConvorbireNationala * (-1) * (procentDepasireValoriAbonament * pretAbonamente) / 100)) +
            ((minutesSMSInternetCalcTot.DurataConvorbireInternationala > 0) ? 0 : (minutesSMSInternetCalcTot.DurataConvorbireInternationala * (-1) * (procentDepasireValoriAbonament * pretAbonamente) / 100)) +
            ((minutesSMSInternetCalcTot.SMSRetea > 0) ? 0 : (minutesSMSInternetCalcTot.SMSRetea * (-1) * (procentDepasireValoriAbonament * pretAbonamente) / 100)) +
            ((minutesSMSInternetCalcTot.SMSNationala > 0) ? 0 : (minutesSMSInternetCalcTot.SMSNationala * (-1) * (procentDepasireValoriAbonament * pretAbonamente) / 100)) +
            ((minutesSMSInternetCalcTot.SMSInternationala > 0) ? 0 : (minutesSMSInternetCalcTot.SMSInternationala * (-1) * (procentDepasireValoriAbonament * pretAbonamente) / 100)) +
            ((minutesSMSInternetCalcTot.TraficDeDateRetea > 0) ? 0 : (minutesSMSInternetCalcTot.TraficDeDateRetea * (-1) * (procentDepasireValoriAbonament * pretAbonamente) / 100)) +
            ((minutesSMSInternetCalcTot.TraficDeDateNationala > 0) ? 0 : (minutesSMSInternetCalcTot.TraficDeDateNationala * (-1) * (procentDepasireValoriAbonament * pretAbonamente) / 100)) +
            ((minutesSMSInternetCalcTot.TraficDeDateInternationala > 0) ? 0 : (minutesSMSInternetCalcTot.TraficDeDateInternationala * (-1) * (procentDepasireValoriAbonament * pretAbonamente) / 100));

        double pretTotalDePlata = pretAbonamente + pretTotalDepasireAbonament;
        return pretTotalDePlata;
        }
    }

}
