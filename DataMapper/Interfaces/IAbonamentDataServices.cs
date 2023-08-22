using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.Interfaces
{
    public interface IAbonamentDataServices
    {
        bool AddAbonament(Abonament abonament);
        bool NameAlreadyExist(string name);
        Abonament GetAbonamentById(int id);
        bool FindAbonamentByName(string abonamentName);
        bool AddAbonamentForUtilizator(string userEmail, string abonamentName);
        List<AbonamentUser> GetAbonamentsByUserId(int id);
    }
}
