using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Movie4U.Services
{
    public class DatabasePopulatorService : IDatabasePopulatorService
    {

        public DatabasePopulatorService(IConfiguration config)
        {
            Configuration = config;
        }

        public IConfiguration Configuration { get; }

        public async Task updateCountriesAsync()
        {
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://unogs-unogs-v1.p.rapidapi.com/static/genres"),
                Headers =
                    {
                        { "X-RapidAPI-Host", Configuration.GetSection("uNoGs").GetSection("X-RapidAPI-Host").Value},
                        { "X-RapidAPI-Key", Configuration.GetSection("uNoGs").GetSection("X-RapidAPI-Key").Value},
                    },
            };
            var response = await client.SendAsync(request);
            try
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsAsync<IEnumerable<>>);
                Console.WriteLine(body);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                Console.WriteLine("Error ({0})", e.Message);

            }

            //client.BaseAddress = new Uri(URL);

            //// Add an Accept header for JSON format.
            //client.DefaultRequestHeaders.Accept.Add(
            //new MediaTypeWithQualityHeaderValue("application/json"));

            //// List data response.
            //HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            //if (response.IsSuccessStatusCode)
            //{
            //    // Parse the response body.
            //    var dataObjects = response.Content.ReadAsAsync<IEnumerable<DataObject>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
            //    foreach (var d in dataObjects)
            //    {
            //        Console.WriteLine("{0}", d.Name);
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            //}

            // Make any other calls using HttpClient here.

            // Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            client.Dispose();
        }
    }
}
