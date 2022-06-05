using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsumptionTracker.Models;

namespace ConsumptionTracker.Repositories
{
    public interface IWaterConsumptionRepository
    {
        // returns all data as a list    
        Task<IEnumerable<WaterUsage>> GetAll();

        //returns the top 10 highest consumers
        Task<IEnumerable<WaterUsage>> GetTopConsumers();

    }
}