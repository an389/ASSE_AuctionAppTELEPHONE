using DataMapper;
using DataMapper.Interfaces;
using DomainModel.Models;
using log4net;
using ServiceLayer.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AuctionConsoleApp
{
    public class ReteaMobilaManager
    {
        private readonly IDAOFactory currentDAOFactory;
        private readonly ILog logger;

        private UtilizatorServicesImplementation utilizatorServicesImplementation;
        private AbonamentServicesImplementation abonamentServiceImplementation;
        private BuisniessServiceImplementation buisniessServiceImplementation;
        private BonusuriServiceImplementation bonusuriServiceImplementation;
        private CentralaTelefonicaServicesImplementation centralaTelefonicaServicesImplementation;
        private FacturaServicesImplementation facturaServicesImplementation;



        public ReteaMobilaManager(IDAOFactory currentDAOFactory, ILog logger)
        {
            this.currentDAOFactory = currentDAOFactory;
            this.logger = logger;

            this.utilizatorServicesImplementation = new UtilizatorServicesImplementation(currentDAOFactory.UtilizatorDataServices, this.logger);
            this.abonamentServiceImplementation = new AbonamentServicesImplementation(currentDAOFactory.AbonamentDataServices, this.logger, currentDAOFactory.UtilizatorDataServices);
            this.buisniessServiceImplementation = new BuisniessServiceImplementation(currentDAOFactory.BuisniessDataService, this.logger);
            this.bonusuriServiceImplementation = new BonusuriServiceImplementation(currentDAOFactory.BonusDataService, this.logger);
            this.centralaTelefonicaServicesImplementation = new CentralaTelefonicaServicesImplementation(currentDAOFactory.CentralaTelefonicaDataService, this.logger);
            this.facturaServicesImplementation = new FacturaServicesImplementation(currentDAOFactory.FacturaDataService, this.logger);
        }

        public void UtilizatorManager()
        {
            foreach (Utilizator utilizator in this.GetUtilizatori())
            {

                this.utilizatorServicesImplementation.AddUtilizator(utilizator);
            }

            //utilizatorul vrea sa inchida un contract mai devreme

            this.utilizatorServicesImplementation.UserCloseAbonamentSooner("andrei@fakemail.com", "1");

            //utilizatorul vrea sa faca reportare de minute/ sms in alt tip de minute/sms (nationale -> internationale)


        }

        public List<Utilizator> GetUtilizatori()
        {


            return new List<Utilizator>
            {
                new Utilizator("Andrei", "Mihai", "andrei@fakemail.com", "P@ssword123", 5000430385597, "0"),
                new Utilizator("Andrei", "Iulian", "awwndrei@fakemail.com", "P@ssword123", 5000430385597, "0"),
                new Utilizator("Andrei", "Andrei", "ndrei@fakemail.com", "P@ssword123", 5000430385597, "0"),
                new Utilizator("Andrei", "Andreiut", "aaaaaaandrei@fakemail.com","P@ssword123", 5000430385597, "1"),
                new Utilizator("Andrei", "Mihaita", "aaaazaaaaasd@fakemail.com","P@ssword123", 5000430385597, "1"),
            };
        }

        public void AbonamentManager()
        {
            foreach(Abonament abonament in this.GetAbonamente())
            {
                this.abonamentServiceImplementation.AddAbonament(abonament);
            }
        }

        private List<Abonament> GetAbonamente()
        {
            return new List<Abonament>
            {
                new Abonament("Abonament0", 100, DateTime.Today, DateTime.Today.AddDays(360), 100,100,100,100,100,100,100,100,100,this.buisniessServiceImplementation.GetBuisniess().Id),
                new Abonament("Abonament1", 110, DateTime.Today, DateTime.Today.AddDays(360), 200,200,200,200,200,200,200,200,200,this.buisniessServiceImplementation.GetBuisniess().Id),
                new Abonament("Abonament2", 120, DateTime.Today, DateTime.Today.AddDays(360), 300,300,300,300,300,300,300,300,300,this.buisniessServiceImplementation.GetBuisniess().Id),
                new Abonament("Abonament3", 130, DateTime.Today, DateTime.Today.AddDays(360), 400,400,400,400,400,400,400,400,400,this.buisniessServiceImplementation.GetBuisniess().Id),
            };
        }

        public void BuisniessManager()
        {
            foreach (Buisniess buisniess in this.GetBuisniess())
            {

                this.buisniessServiceImplementation.AddBuisniess(buisniess);
            }
            
        }
        private List<Buisniess> GetBuisniess()
        {
            return new List<Buisniess>
            {
                new Buisniess(1.2,1.2,1.2,5.0,5.0,1.5,20),
            };
        }

        public void BonusuriManager()
        {
            foreach (Bonusuri bonusuri in this.GetBonusuri())
            {
                this.bonusuriServiceImplementation.AddBonus(bonusuri);
            }

        }

        private List<Bonusuri> GetBonusuri()
        {
            return new List<Bonusuri>
            {
                new Bonusuri("Bonus1", DateTime.Today, DateTime.Today.AddDays(100), true, 100, 100, 100, 100, 100, 100, 100, 100, 100),
                new Bonusuri("Bonus2", DateTime.Today, DateTime.Today.AddDays(100), true, 200, 200, 200, 200, 200, 200, 200, 200, 200),
                new Bonusuri("Bonus3", DateTime.Today, DateTime.Today.AddDays(100), true, 300, 300, 300, 300, 300, 300, 300, 300, 300),
                new Bonusuri("Bonus4", DateTime.Today, DateTime.Today.AddDays(100), true, 400, 400, 400, 400, 400, 400, 400, 400, 400),
                new Bonusuri("Bonus5", DateTime.Today, DateTime.Today.AddDays(100), true, 500, 500, 500, 500, 500, 500, 500, 500, 500),
            };
        }

        public void CentralaTelefonicaManager()
        {
            foreach (CentralaTelefonica centralaTelefonica in this.GetCentralaTelefonica())
            {
                this.centralaTelefonicaServicesImplementation.AddCentralaTelefonica(centralaTelefonica);
            }

        }

        private List<CentralaTelefonica> GetCentralaTelefonica()
        {
            return new List<CentralaTelefonica>
            {
                 new CentralaTelefonica("andrei@fakemail.com", DateTime.Today, 12, DateTime.Today, 10, DateTime.Today, 9, DateTime.Today, 2, DateTime.Today, 2,DateTime.Today, 3,DateTime.Today, 44,DateTime.Today ,12,DateTime.Today, 0),
                 new CentralaTelefonica("awwndrei@fakemail.com", DateTime.Today, 12, DateTime.Today, 10, DateTime.Today, 9, DateTime.Today, 2, DateTime.Today, 2,DateTime.Today, 3,DateTime.Today, 44,DateTime.Today ,12,DateTime.Today, 0),
                 new CentralaTelefonica("ndrei@fakemail.com", DateTime.Today, 12, DateTime.Today, 10, DateTime.Today, 9, DateTime.Today, 2, DateTime.Today, 2,DateTime.Today, 3,DateTime.Today, 44,DateTime.Today ,12,DateTime.Today, 0),
            };
        }

        public void FacturaManager()
        {
            int month = DateTime.Now.Month;
            foreach (Factura factura in this.GetFacturaLunaTrecuta(month))
            {
                this.facturaServicesImplementation.AddFactura(factura);
            }

        }

        private List<Factura> GetFacturaLunaTrecuta(int lastMonth)
        {
            return this.facturaServicesImplementation.CalculareFacturiInFunctieDeLuna(lastMonth, this.utilizatorServicesImplementation, this.abonamentServiceImplementation, this.centralaTelefonicaServicesImplementation, this.buisniessServiceImplementation, this.bonusuriServiceImplementation);
        }

        public void AbonamentAndUserManager()
        {
            this.abonamentServiceImplementation.AddAbonamentForUtilizator("andrei@fakemail.com", "Abonament0");
            this.abonamentServiceImplementation.AddAbonamentForUtilizator("andrei@fakemail.com", "Abonament1");
            this.abonamentServiceImplementation.AddAbonamentForUtilizator("andrei@fakemail.com", "Abonament2");
            this.abonamentServiceImplementation.AddAbonamentForUtilizator("awwndrei@fakemail.com", "Abonament1");
            this.abonamentServiceImplementation.AddAbonamentForUtilizator("ndrei@fakemail.com", "Abonament3");
        }
    }
}
