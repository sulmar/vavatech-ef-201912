using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Vavatech.WebApi.Models;

namespace Vavatech.WebApi.ConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World");

            // GET api/customers
            string baseUri = "https://localhost:44333";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUri);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            // client.DefaultRequestHeaders.Add("Authorization", "Basic" )

            string user = "2125172157838";
            string password = "RkgHWSkKCA";

            string parameter = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"{user}:{password}"));

            var authorization = 
                new AuthenticationHeaderValue("Basic", parameter);

            client.DefaultRequestHeaders.Authorization = authorization;

            HttpResponseMessage response = await client.GetAsync("api/customers");

            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();

            var customers = JsonConvert.DeserializeObject<IEnumerable<Customer>>(json);

            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.FirstName} {customer.LastName}");
            }


            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

        }
    }


}
