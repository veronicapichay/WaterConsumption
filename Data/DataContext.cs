using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ConsumptionTracker.Models;



namespace ConsumptionTracker.Data
{
    public class DataContext : DbContext, IDataContext
    {
        
        //constructor
        public DataContext(DbContextOptions<DataContext> options) : base (options)
        {


        }
        
        public DbSet<WaterConsumption> Consumptions {get; set;}



    }
}