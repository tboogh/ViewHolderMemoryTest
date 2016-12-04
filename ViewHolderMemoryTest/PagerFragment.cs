using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V13.App;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace ViewHolderMemoryTest
{
    public class PagerFragmentAdapter : FragmentStatePagerAdapter
    {
        private string[] _planets = { "Venus", "Earth", "Mars", "Jupiter", "Saturn", "Uranus", "Neptune" };

        public PagerFragmentAdapter(FragmentManager fm) : base(fm)
        {
        }

        public override int Count => _planets.Length;
        public override Fragment GetItem(int position)
        {
            return new PlanetFragment(_planets[position]);
        }
    }

    public class PagerFragment : Fragment
    {
        private PagerFragmentAdapter _adapter;
        private ViewPager _viewPager;

        public PagerFragment()
        {
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            _adapter = new PagerFragmentAdapter(ChildFragmentManager);
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.PagerFragment, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);
            _viewPager = view.FindViewById<ViewPager>(Resource.Id.viewPager);
            _viewPager.Adapter = _adapter;
        }

        public override void OnDestroyView()
        {
            base.OnDestroyView();
            _viewPager.Adapter = null;
            _viewPager = null;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            _adapter = null;
        }

        public override void OnResume()
        {
            base.OnResume();
        }
    }
}