using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ConsumptionTracker.Models;

namespace ConsumptionTracker.Data
{
    public interface IDataContext
    {
        //creates db table directly 
        DbSet<WaterUsage> Consumptions {get; set;}

        //updates the table after every query
        int SaveChanges();






    }
}