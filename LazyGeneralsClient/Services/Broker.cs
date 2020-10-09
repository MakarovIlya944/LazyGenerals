using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using LazyGenerals.Client.Models;
using Microsoft.Extensions.Logging;

namespace LazyGenerals.Client.Services
{
    public class Broker
    {
        private readonly HttpClient _client;

        private readonly ILogger<Broker> _logger;

        public Broker(ILogger<Broker> logger, HttpClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<List<Game>> UpdateLobby()
        {
            HttpResponseMessage response = await _client.GetAsync("api/game");

            if (response.IsSuccessStatusCode)
            {
                string a = response.Content.ReadAsStringAsync().Result;

                try
                {
                    var b = JsonSerializer.Deserialize<List<Game>>(a);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Can't deserialize: " + ex.Message);

                    throw;
                }

                return new List<Game>();
            }

            return null;
        }
    }
}