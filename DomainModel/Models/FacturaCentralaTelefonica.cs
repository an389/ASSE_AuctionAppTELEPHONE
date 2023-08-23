using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models
{
    public class FacturaCentralaTelefonica
    {
        public int IdFactura;
        public int IdCentralaTelefonica;

        public FacturaCentralaTelefonica(int idFactura, int idCentralaTelefonica)
        {
            IdFactura = idFactura;
            IdCentralaTelefonica = idCentralaTelefonica;
        }

        public FacturaCentralaTelefonica()
        {

        }
    }
}
