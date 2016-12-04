using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using ViewHolderMemoryTest.Core.Models;
using ViewHolderMemoryTest.Core.Models.Seti;
using ViewHolderMemoryTest.Core.Services;
using HttpService = ViewHolderMemoryTest.Core.Services.Stub.HttpService;
using Response = ViewHolderMemoryTest.Core.Models.Seti.Response;

namespace ViewHolderMemoryTest
{
    public class PictureFragment : Fragment
    {
        private PictureSectionAdapter _pictureSectionAdapter;
        private LinearLayoutManager _layoutManager;
        private RecyclerView _recyclerView;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _layoutManager = new LinearLayoutManager(Context, LinearLayoutManager.Vertical, false);
            _pictureSectionAdapter = new PictureSectionAdapter(Context);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.PictureListFragment, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _recyclerView = view.FindViewById<RecyclerView>(Resource.Id.PictureSectionContainer);
            _recyclerView.SetLayoutManager(_layoutManager);
            _recyclerView.SetAdapter(_pictureSectionAdapter);
        }

        public override void OnDestroyView()
        {
            base.OnDestroyView();
            _recyclerView = null;

            GC.Collect();
        }

        public override void OnResume()
        {
            base.OnResume();
            
        }
    }
}