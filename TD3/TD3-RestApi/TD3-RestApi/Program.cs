using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace TD3_RestApi
{
    class Program
    {
        static readonly HttpClient client = new HttpClient();

        private static string apiKey = "2dae8548640ca7455930eb98ca0508a102ac8c1a";

        public async static Task Main()
        {
            try
            {
                String contracts  = getAllContractsAsync().Result;
                parseArrayJson(contracts);

                String stations = getStationsFromContract("marseille").Result;
                parseArrayJson(stations);

                String station = getStationNews("9087", "marseille").Result;
                parseJson(station);

                // JCDecaux API bug
                //String bikes = getBikesParksFromContract("marseille").Result;
                //parseArrayJson(bikes);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            
            Console.ReadKey();
        }

        public static async Task<string> getAllContractsAsync() {
            return await httpRequestAsync("https://api.jcdecaux.com/vls/v3/contracts?apiKey=" + apiKey);
        }

        public static async Task<string> getStationsFromContract(String contract)
        {
            return await httpRequestAsync("https://api.jcdecaux.com/vls/v1/stations?contract=" + contract + "&apiKey=" + apiKey);
        }

        public static async Task<string> getStationNews(String stationNumber, String contract)
        {
            return await httpRequestAsync("https://api.jcdecaux.com/vls/v3/stations/" + "9087" + "?contract=" + contract + "&apiKey=" + apiKey);
        }

        public static async Task<string> getBikesParksFromContract(String contract)
        {
            return await httpRequestAsync("https://api.jcdecaux.com/parking/v1/contracts/" + contract + "/parks?apiKey=" + apiKey);
        }

        public static async Task<string> httpRequestAsync(String request)
        {
            HttpResponseMessage response = await client.GetAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public static void parseArrayJson(String json)
        {
            dynamic parsed = JsonConvert.DeserializeObject(json);
            foreach (var item in parsed)
            {
                Console.WriteLine($"- {item.name}");
            }
            Console.WriteLine($"-------------------------------");
        }

        public static void parseJson(String json)
        {
            dynamic parsed = JsonConvert.DeserializeObject(json);
            Console.WriteLine($"- {parsed.name}");
            Console.WriteLine($"-------------------------------");
        }

    }
}
