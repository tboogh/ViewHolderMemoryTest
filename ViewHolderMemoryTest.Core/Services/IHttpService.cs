using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ViewHolderMemoryTest.Core.Models;

namespace ViewHolderMemoryTest.Core.Services
{
    public interface IHttpService
    {
        Task<Response> GetResponse(int count, CancellationToken cancellationToken);
        Task<Models.Seti.Response> GetSetiResponse(string searchName, CancellationToken cancellationToken);
    }
}