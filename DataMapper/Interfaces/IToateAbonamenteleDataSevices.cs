using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.Interfaces
{
    public interface IToateAbonamenteleDataSevices
    {
        bool AddAbonament(Abonament abonament);
        Abonament GetAbonament(Abonament abonament);
    }
}
