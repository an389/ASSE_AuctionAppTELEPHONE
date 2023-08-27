using DomainModel.Models;
using ServiceLayer.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IFacturaService
    {
        bool AddFactura(Factura factura);
        public List<Factura> CalculareFacturiInFunctieDeLuna(int lastMonth, UtilizatorServicesImplementation utilizatorServicesImplementation, AbonamentServicesImplementation abonamentServiceImplementation, CentralaTelefonicaServicesImplementation centralaTelefonicaServicesImplementation, BuisniessServiceImplementation buisniessServiceImplementation, BonusuriServiceImplementation bonusuriServiceImplementation);

    }
}
