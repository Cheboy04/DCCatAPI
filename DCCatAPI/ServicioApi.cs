using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCCatAPI.Models;
using System.Threading.Tasks;

namespace DCCatAPI
{
    public class ServicioApi
    {
        private readonly HttpClient _httpClient;

        public ServicioApi()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(AppSettings.CatApiBaseUrl)
            };
            _httpClient.DefaultRequestHeaders.Add("x-api-key", AppSettings.ApiKey);
        }

        public async Task<List<Cat>> GetBreedsAsync()
        {
            var response = await _httpClient.GetAsync("breeds");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Cat>>(json);
            }
            return new List<Cat>();
        }
    }
}
