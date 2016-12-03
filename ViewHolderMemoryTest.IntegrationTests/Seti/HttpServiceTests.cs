using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ViewHolderMemoryTest.Core.Models;
using ViewHolderMemoryTest.Core.Services;
using Xunit;

namespace ViewHolderMemoryTest.IntegrationTests.Seti
{
    public class HttpServiceTests
    {
        [Theory]
        [InlineData("Mercury")]
        [InlineData("Venus")]
        [InlineData("Earth")]
        [InlineData("Mars")]
        [InlineData("Jupiter")]
        [InlineData("Saturn")]
        [InlineData("Uranus")]
        [InlineData("Neptune")]
        public async Task GetResults_NotNull(string planet)
        {
            IHttpService httpService = new HttpService();
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(15));
            Core.Models.Seti.Response results = await httpService.GetSetiResponse(planet, cancellationTokenSource.Token);
            Assert.NotNull(results);
        }

        [Theory]
        [InlineData("Mercury")]
        [InlineData("Venus")]
        [InlineData("Earth")]
        [InlineData("Mars")]
        [InlineData("Jupiter")]
        [InlineData("Saturn")]
        [InlineData("Uranus")]
        [InlineData("Neptune")]
        public async Task GetResults_Results_NotNull(string planet)
        {
            IHttpService httpService = new HttpService();
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(15));
            Core.Models.Seti.Response response = await httpService.GetSetiResponse(planet, cancellationTokenSource.Token);
            Assert.NotNull(response.data);
        }

        [Theory]
        [InlineData("Mercury")]
        [InlineData("Venus")]
        [InlineData("Earth")]
        [InlineData("Mars")]
        [InlineData("Jupiter")]
        [InlineData("Saturn")]
        [InlineData("Uranus")]
        [InlineData("Neptune")]
        public async Task GetResults_Results_PicturesNotEmpty(string planet)
        {
            IHttpService httpService = new HttpService();
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(15));
            Core.Models.Seti.Response response = await httpService.GetSetiResponse(planet, cancellationTokenSource.Token);
            List<string> urls = response.data.Select(x => x.full).ToList();
            Assert.NotNull(urls);
        }
    }
}
