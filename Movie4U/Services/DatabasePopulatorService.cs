using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Movie4U.EntitiesModels.Entities;
using Movie4U.EntitiesModels.Models;
using Movie4U.EntitiesModels.Models.uNoGS;
using Movie4U.Managers.IManagers;

namespace Movie4U.Services
{
    public class DatabasePopulatorService : IDatabasePopulatorService
    {

        public IConfiguration Configuration { get; }

        internal Movie4UContext db;

        private readonly IGenresManager genresManager;

        public DatabasePopulatorService(IConfiguration config, IGenresManager genresManager, Movie4UContext db)
        {
            Configuration = config;
            this.db = db;
            this.genresManager = genresManager;
        }

        public async Task CreateGenresAsync()
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
                var bodySerialized = await response.Content.ReadAsStringAsync();
                var genres = JsonSerializer.Deserialize<GenreResponseListModel>(bodySerialized);


                db.Database.OpenConnection();
                try
                {
                    // In order to be able to insert with a specified id
                    db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Genres ON;");
                    db.SaveChanges();

                    //await db.Genres.AddRangeAsync(genres.results.Select(r => new Genre
                    //{
                    //    genre_id = r.netflix_id,
                    //    genre = r.genre
                    //}));

                    await genresManager.CreateMultiple(genres.results);

                    //foreach (GenreResponseModel result in genres.results)
                    //{
                    //    await genresManager.Create(new GenreModel
                    //    {
                    //        genre_id = result.netflix_id,
                    //        genre = result.genre
                    //    });
                    //    break;
                    //}

                    db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT dbo.Genres OFF;");
                }
                finally
                {
                    db.Database.CloseConnection();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                Console.WriteLine("Error ({0})", e.Message);

            }

            // Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            client.Dispose();
        }
    }
}
