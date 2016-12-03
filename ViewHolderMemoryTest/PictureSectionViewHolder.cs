using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using ViewHolderMemoryTest.Core.Models.Seti;
using ViewHolderMemoryTest.Core.Services;

namespace ViewHolderMemoryTest
{
    public class PictureSectionViewHolder : RecyclerView.ViewHolder
    {
        private readonly PictureRowAdapter _adapter;
        private readonly Context _context;
        public RecyclerView RecyclerView { get; set; }
        private CancellationTokenSource _cancellationTokenSource;

        public PictureSectionViewHolder(View itemView, Context context) : base(itemView)
        {
            _adapter = new PictureRowAdapter(context);
            _context = context;
            Init();
        }

        public void Init()
        {
            RecyclerView = ItemView.FindViewById<RecyclerView>(Resource.Id.PictureRowContainer);
            var layoutManager = new LinearLayoutManager(_context) { Orientation = LinearLayoutManager.Horizontal };
            RecyclerView.SetLayoutManager(layoutManager);
            RecyclerView.SetAdapter(_adapter);
        }

        public void Cleanup()
        {
            RecyclerView = null;
            _cancellationTokenSource.Cancel();
            GC.Collect();
        }

        public async void Update(string planet)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(15));

            IHttpService httpService = new HttpService();
            Response response = null;
            try
            {
                response = await httpService.GetSetiResponse(planet, _cancellationTokenSource.Token);
            }
            catch (TaskCanceledException)
            {
                
            }

            string[] urls = response?.data.Select(picture => picture.full.Contains("http") ? picture.full : $"http://pds-rings.seti.org/browse/{picture.full}")
                .ToArray();

            if (urls != null)
                _adapter.Update(urls);
        }
    }
}