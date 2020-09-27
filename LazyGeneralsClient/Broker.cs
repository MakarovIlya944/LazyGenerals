using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using Serilog;

namespace LazyGeneralsClient
{
    public class Broker
    {
        static HttpClient client = new HttpClient();
        private static ILogger _logger;

        static public void Init(string url)
        {
            client.BaseAddress = new Uri(url);
            try
            {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("logs\\myapp.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
            _logger = Log.Logger;
        }

        static public async Task<List<Game>> UpdateLobby()
        {

            HttpResponseMessage response = await client.GetAsync("api/game");
            if (response.IsSuccessStatusCode)
            {
                string a = response.Content.ReadAsStringAsync().Result;
                try
                {
                var b = JsonSerializer.Deserialize<List<Game>>(a);
                }
                catch (Exception ex)
                {
                    _logger.Error("Can't deserialize: "  + ex.Message);
                    throw;
                }
                int c = 3 + 4;
                return new List<Game>();
            }
            return null;
        }
    }
}
