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
    //implements the interface fx
    public class WaterConsumptionRepository : IWaterConsumptionRepository
    {
        //instance of context 
        private readonly IDataContext _context;


        public WaterConsumptionRepository(IDataContext context)
        {

            _context = context;

        }

        //body of the fx
        public async Task<IEnumerable<WaterUsage>> GetAll()
        {

            SaveData();

            //queries the data and returns as a list 
            return await _context.Consumptions.ToListAsync();

        }

        public async Task<IEnumerable<WaterUsage>> GetTopConsumers()
        {
            //queries the data and returns as a list 
            var qty = _context.Consumptions
                .OrderByDescending(avgGal => avgGal.averageMonthlyGal) //sorted 
                .Take(10)
                .ToListAsync();

            return await qty;
        }
        //ETL operations
        public void SaveData()
        {
            //read data from table 
            var result_dataset = _context.Consumptions.ToList();

            //checks if table is empty before loading the data 
            //extract from file
            if (result_dataset.Count() == 0)
            {
                Console.WriteLine("No Data");

                //reading json data
                var geoJSON = File.ReadAllText("C:\\Users\\veron\\OneDrive\\Desktop\\bayarea.geojson");

                //converts to a json file 
                dynamic jsonObj = JsonConvert.DeserializeObject(geoJSON);

                //grabs values in each feature from geojson and stores in a string var
                foreach (var feature in jsonObj["features"])
                {
                    //Extraction
                    string str_city = feature["properties"]["city"];
                    string str_county = feature["properties"]["county"];
                    string str_avgMonthlyGal = feature["properties"]["averageMonthlyGal"];
                    string str_geometry = feature["geometry"]["coordinates"].ToString(Newtonsoft.Json.Formatting.None);  //strores a single string value

                    //Transformation
                    string conv_avgMnthlyGal = str_avgMonthlyGal.Replace(".0", "");

                    //keeping it as int
                    //convert string to int
                    int avgMnthlyGal = Convert.ToInt32(conv_avgMnthlyGal);

                    //Load data into the table
                    WaterUsage wu = new()
                    {
                        city = str_city,
                        county = str_county,
                        averageMonthlyGal = avgMnthlyGal,
                        coordinates = str_geometry

                    };

                    _context.Consumptions.Add(wu);
                    _context.SaveChanges();

                }
            }

            else
            {
                //displays when table is already populated 
                Console.WriteLine("Data Loaded");

            }

        }

    }
}