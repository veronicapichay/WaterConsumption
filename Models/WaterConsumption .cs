using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumptionTracker.Models
{
    public class WaterConsumption
    {       

    public int id { get; set; }

    public string city { get; set; }

    public string county { get; set; }

    public int averageMonthlyGal { get; set; }

    public string coordinates { get; set; }

    }
}