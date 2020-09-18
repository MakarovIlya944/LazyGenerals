using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LazyGeneralsClient
{
    public class Broker
    {
        static HttpClient client = new HttpClient();

        static public void Init(string url)
        {
            client.BaseAddress = new Uri(url);
        }

        static public async Task<List<Game>> UpdateLobby()
        {
            HttpResponseMessage response = await client.GetAsync("api/game");
            if (response.IsSuccessStatusCode)
            {
                var a = response.Content.ReadAsStringAsync().Result;

                return new List<Game>();
            }
            return null;
        }
    }
}
