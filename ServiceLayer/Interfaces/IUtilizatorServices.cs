using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IUtilizatorServices
    {
        bool AddUtilizator(Utilizator utizator);

        List<Utilizator> GetAllUsers();

        Utilizator GetUtilizatorById(int id);


        public bool UserCloseAbonamentSooner(string userEmail, string abonament);

        bool DeleteUtilizator(Utilizator utilizator);

    }
}
