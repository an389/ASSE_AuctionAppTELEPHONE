using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.Interfaces
{
    public interface IBuisniessDataService
    {
        bool AddBuisniess(Buisniess buisniess);
        Buisniess GetBuisniess();
        Buisniess GetBuisniessById(int id);
    }
}
