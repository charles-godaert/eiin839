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

        private static string res;

        public async static Task Main()
        {
            try
            {
                res = getAllContractsAsync().Result;
                parseJson(res);     
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            //Console.WriteLine(res);
            
            Console.ReadKey();
        }

        public static async Task<string> getAllContractsAsync() {
            return await httpRequestAsync("https://api.jcdecaux.com/vls/v3/contracts?apiKey=2dae8548640ca7455930eb98ca0508a102ac8c1a");
        }

        public static async Task<string> httpRequestAsync(String request)
        {
            HttpResponseMessage response = await client.GetAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public static void parseJson(String json)
        {
            dynamic parsed = JsonConvert.DeserializeObject(res);
            foreach (var item in parsed)
            {
                Console.WriteLine($"- {item.name}");
            }
        }

    }
}
