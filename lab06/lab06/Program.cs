﻿using System;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace lab06
{
    public class Program
    {
        public static async Task Main()
        {
            SortedSet<string> countries = new SortedSet<string>();
            List<Weather> weathers = new List<Weather>();
            /*for (int i = 0; i < 10; ++i)
            {
                weathers.Add(new Weather());
            }
            foreach (var weather in weathers)
            {
                await weather.genResponse();
                weather.printInfo();
            }*/
            for (int i = 0; i < 20; ++i)
            {
                weathers.Add(new Weather());
            }
            double average_temp = 0;
            foreach (var weather in weathers)
            {
                await weather.genResponse();
                average_temp += Convert.ToDouble(weather.Temp);
                countries.Add(weather.Country);
            }
            Console.WriteLine($"Average temperature: {average_temp}");
            
            var select_min_max = from weath in weathers
                                 orderby weath.Temp
                                 select weath;
            Console.WriteLine($"Max: {select_min_max.Last()}, Min: {select_min_max.First()}");

            Console.WriteLine($"Countries: {countries.Count}");

            var clear_sky_country = from weath in weathers
                                    where (weath.Description == "clear sky")
                                    select weath;
            Console.WriteLine($"First country with clear sky: {clear_sky_country.First().Country}");

            var rainy_country = from weath in weathers
                                where (weath.Description == "rain")
                                select weath;
            Console.WriteLine($"First country with rain: {rainy_country.First().Description}");

            var few_clouds_country = from weath in weathers
                                     where (weath.Description == "few clouds")
                                     select weath;
            Console.WriteLine($"First country with few clouds: {few_clouds_country.First().Description}");
        }
    } 
}

