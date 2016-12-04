using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Microsoft.Practices.Unity;
using ViewHolderMemoryTest.Core.Models.Seti;
using ViewHolderMemoryTest.Core.Services;

namespace ViewHolderMemoryTest
{
    public class PlanetFragment : Fragment
    {
        private readonly string _planet;
        private CancellationTokenSource _cancellationTokenSource;
        private RecyclerView _recyclerView;
        private TextView _textView;
        private PictureRowAdapter _adapter;

        public PlanetFragment(string planet)
        {
            _planet = planet;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            _adapter = new PictureRowAdapter(Context);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.PlanetFragment, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            _recyclerView = view.FindViewById<RecyclerView>(Resource.Id.PlanetImageRecyclerView);
            _textView = view.FindViewById<TextView>(Resource.Id.textView);

            _textView.Text = _planet;
            _recyclerView.SetLayoutManager(new LinearLayoutManager(Context, LinearLayoutManager.Horizontal, false));
            _recyclerView.SetAdapter(_adapter);
        }

        public override void OnDestroyView()
        {
            base.OnDestroyView();
            _recyclerView = null;
            _textView = null;
            GC.Collect();
        }

        public override void OnResume()
        {
            base.OnResume();
            Update(_planet);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            _adapter = null;
            _cancellationTokenSource?.Cancel();
        }

        private async void Update(string planet)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(15));

            IHttpService httpService = MainActivity.Container.Resolve<IHttpService>();
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
                _adapter.Update(urls, planet);
        }
    }
}