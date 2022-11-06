using System;
using System.Dynamic;
using System.Net;
using Newtonsoft.Json;

namespace lab06
{
    public class Weather: IComparable<Weather>
    {
        private static string API_key = "b0196f08cb67b9600ecbb6f542b69c81";
        private string lat_;
        private string lon_;
        private IComparable<Weather> comparable;

        public string? Country;
        public string? Name;
        public string? Temp;
        public string? Description;

        public Weather()
        {
            lat_ = genLat();
            lon_ = genLon();
        }



        public string genLat()
        {
            string lat = "";
            Random random = new Random();
            int first = -90;
            int last = 90;
            lat = Convert.ToString(random.Next(first, last));
            return lat;
        }

        public string genLon()
        {
            string lon = "";
            Random random = new Random();
            int first = -180;
            int last = 180;
            lon = Convert.ToString(random.Next(first, last));
            return lon;
        }

        public async Task genResponse()
        {
            HttpClient httpClient = new HttpClient();
            string response_data = $"https://api.openweathermap.org/data/2.5/weather?lat={lat_}&lon={lon_}&appid={API_key}";
            using HttpResponseMessage response = await httpClient.GetAsync(response_data);
            string content = await response.Content.ReadAsStringAsync(); // получение ответа
            var deserializeObject = JsonConvert.DeserializeObject<dynamic>(content)!;
            Country = deserializeObject.sys.country;
            Name = deserializeObject.name;
            Temp = deserializeObject.main.temp;
            Description = deserializeObject.weather[0].description;
        }

        public void printInfo()
        {
            Console.WriteLine("Start printing a content: ");
            Console.WriteLine();
            Console.WriteLine($"Country: {Country}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Temp: {Temp}");
            Console.WriteLine($"Description: {Description}");
            Console.WriteLine();
            Console.WriteLine("End printing a content: ");
            Console.WriteLine("###############################################");
        }
        public int CompareTo(Weather other)
        {
            if (Temp.CompareTo(other.Temp) != 0)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
    }
}

