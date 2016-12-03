using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ViewHolderMemoryTest.Core.Models;
using ViewHolderMemoryTest.Core.Services;
using Xunit;
using HttpService = ViewHolderMemoryTest.Core.Services.Stub.HttpService;

namespace ViewHolderMemoryTest.IntegrationTests
{
    public class HttpServiceTests
    {
        [Fact]
        public async Task GetResults_NotNull()
        {
            IHttpService httpService = new HttpService();
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(15));
            Response results = await httpService.GetResponse(100, cancellationTokenSource.Token);
            Assert.NotNull(results);
        }

        [Fact]
        public async Task GetResults_Results_NotNull()
        {
            IHttpService httpService = new HttpService();
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(15));
            Response response = await httpService.GetResponse(100, cancellationTokenSource.Token);
            Assert.NotNull(response.Results);
        }

        [Fact]
        public async Task GetResults_Results_PicturesNotEmpty()
        {
            IHttpService httpService = new HttpService();
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(15));
            Response response = await httpService.GetResponse(1, cancellationTokenSource.Token);
            List<Picture> pictures = response.Results.Select(x => x.Picture).ToList();
            Assert.NotNull(pictures);
        }

        [Fact]
        public async Task GetResults_Results_PicturesCorrectCount()
        {
            int pictureCount = 100;

            IHttpService httpService = new HttpService();
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(15));
            Response response = await httpService.GetResponse(pictureCount, cancellationTokenSource.Token);
            List<Picture> pictures = response.Results.Select(x => x.Picture).ToList();
            Assert.Equal(pictureCount, pictures.Count);
        }
    }
}