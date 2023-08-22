using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface ICentralaTelefonicaService
    {
        bool AddCentralaTelefonica(CentralaTelefonica centralaTelefonica);
        List<CentralaTelefonica> GetUtilizatoriCentralaTelefonicaByEmail(int month, string email);
       
    }
}
