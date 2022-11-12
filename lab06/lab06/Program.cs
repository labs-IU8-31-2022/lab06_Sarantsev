using System;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Data;
//using System.Globalization;

//CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");

namespace lab06
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            SortedSet<string> countries = new SortedSet<string>();
            List<Weather> weathers = new List<Weather>();
            for (; weathers.Count() < 10;)
            {
                weathers.Add(new Weather());
                Thread.Sleep(1000);
            }
            foreach (var weather in weathers)
            {
                await weather.genResponse();
                if (weather.Country is null || weather.Name is null)
                {
                    continue;
                }
                else
                {
                    weather.printInfo();
                }
            }
            foreach (var weather in weathers)
            {
                weather.printInfo();
            }

            double average_temp = 0;
            foreach (var weather in weathers)
            {
                await weather.genResponse();
                average_temp += Convert.ToDouble(weather.Temp);
                countries.Add(weather.Country);
            }
            Console.WriteLine($"Average temperature: {average_temp}");

            var maxTemp = weathers.MaxBy(i => i.Temp);
            Console.WriteLine($"Country with max temperature: {maxTemp.Country}  {maxTemp.Temp}°C");

            var minTemp = weathers.MinBy(i => i.Temp);
            Console.WriteLine($"Country with min temperature: {minTemp.Country}  {minTemp.Temp}°C");

            Console.WriteLine($"Countries: {countries.Count}");

            var clear_sky = from weath in weathers
                            where (weath.Description == "clear sky")
                            select weath;
            if (clear_sky.Count() == 0)
            {
                Console.WriteLine("!There are no objects with this description!");
            } else
            {
                Console.WriteLine($"First country with clear sky: {clear_sky.First().Country}");
            }

            var rainy = from weath in weathers
                        where (weath.Description == "rain")
                        select weath;
            if (rainy.Count() == 0)  
            {
                Console.WriteLine("!There are no objects with this description!");
            } else
            {
                Console.WriteLine($"First country with rain: {rainy.First().Description}");
            }

            var few_clouds = from weath in weathers
                             where (weath.Description == "few clouds")
                             select weath;
            if (few_clouds.Count() == 0)
            {
                Console.WriteLine("!There are no objects with this description!");
            } else
            {
                Console.WriteLine($"First country with few clouds: {few_clouds.First().Description}");
            }
        }
    }
}

