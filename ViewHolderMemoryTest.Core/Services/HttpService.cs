using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ViewHolderMemoryTest.Core.Models;

namespace ViewHolderMemoryTest.Core.Services
{
    public class HttpService : IHttpService
    {
        private HttpClient _httpClient;

        public HttpService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<Response> GetResponse(int count, CancellationToken cancellationToken)
        {
            string stringResult = await _httpClient.GetStringAsync($"http://randomuser.me/api/?results={count}&inc=picture");
            return JsonConvert.DeserializeObject<Response>(stringResult);
        }

        public async Task<Models.Seti.Response> GetSetiResponse(string searchName, CancellationToken cancellationToken)
        {
            HttpResponseMessage result = await _httpClient.GetAsync($"http://tools.pds-rings.seti.org/opus/api/images/full.json?planet={searchName}", cancellationToken);
            if (cancellationToken.IsCancellationRequested)
            {
                
            }

            string content = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Models.Seti.Response>(content);
        }
    }
}
