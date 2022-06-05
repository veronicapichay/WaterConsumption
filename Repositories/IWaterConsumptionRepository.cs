using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsumptionTracker.Models;

namespace ConsumptionTracker.Repositories
{
    public interface IWaterRepository
    {

        // returns all data as a list    
        Task<IEnumerable<WaterConsumption>> GetAll();

        //returns the top 10 highest consumers
        Task<IEnumerable<WaterConsumption>> GetTopConsumers();

    }
}