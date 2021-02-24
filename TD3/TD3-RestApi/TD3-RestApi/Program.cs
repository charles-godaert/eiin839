using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TD3_RestApi
{
    class Program
    {
        // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
        static readonly HttpClient client = new HttpClient();

        private static Task<string> res;

        public async static Task Main()
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                res = getAllContractsAsync();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            Console.WriteLine(res.Result.ToString());
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

    }
}
