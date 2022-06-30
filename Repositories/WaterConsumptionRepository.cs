using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsumptionTracker.Models;
using ConsumptionTracker.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ConsumptionTracker.Repositories
{
    public class WaterConsumptionRepository : IWaterConsumptionRepository
    {
        private readonly IDataContext _context;

        public WaterConsumptionRepository(IDataContext context)
        {
            _context = context;
        }

        //functions 
        public async Task<IEnumerable<WaterConsumption>> GetAll()
        {
            SaveData();
            return await _context.Consumptions.ToListAsync();    //runs the query and returns as a list
        }

        public async Task<IEnumerable<WaterConsumption>> GetTopConsumers()
        {
            var qty = _context.Consumptions
                .OrderByDescending(avgGal => avgGal.averageMonthlyGal)
                .Take(10)
                .ToListAsync();

            return await qty;
        }

        public void SaveData()
        {
            //Extract
            // Checks if the table is empty before extraction
            var res_dataset = _context.Consumptions.ToList();

            if (res_dataset.Count() == 0)
            {
                Console.WriteLine("No Data");

                var geoJSON = File.ReadAllText("C:\\DataGeo\\bayareacountiestwo.geojson");

                //converting to json object with the use of newton lib
                dynamic jsonObj = JsonConvert.DeserializeObject(geoJSON);

                foreach (var feature in jsonObj["features"]) //collection from geojson file
                {
                    // Extract values from the file object using the fields and store in a string variable
                    string str_city = feature["properties"]["city"];
                    string str_county = feature["properties"]["county"];
                    string str_avgMonthlyGal = feature["properties"]["averageMonthlyGal"];
                    string str_geometry = feature["geometry"]["coordinates"].ToString(Newtonsoft.Json.Formatting.None);

                    // Apply Transformations; Remove .0's from values
                    string conv_avgMthlyGal = str_avgMonthlyGal.Replace(".0", "");

                    // Convert string to integer
                    int avgMthlyGal = Convert.ToInt32(conv_avgMthlyGal);

                    // Load the data into our table
                    WaterConsumption wc = new()
                    {
                        city = str_city,
                        county = str_county,
                        averageMonthlyGal = avgMthlyGal,
                        coordinates = str_geometry
                    };

                    //writing data to table
                    _context.Consumptions.Add(wc);
                    _context.SaveChanges();
                }
            }
            else
            {
                Console.WriteLine("Data Loaded"); //displays message if table is not empty 
            }
        }
    }
}