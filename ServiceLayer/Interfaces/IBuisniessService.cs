using DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IBuisniessService
    {
        bool AddBuisniess(Buisniess buisniess);
        public Buisniess GetBuisniess();
        public Buisniess GetBuisniessById(int id);  
    }
}
