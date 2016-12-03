using System;
using System.Reactive.Linq;
using System.Reflection;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;

namespace ViewHolderMemoryTest
{
    [Activity(Label = "ViewHolderMemoryTest", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        private IDisposable _buttonOneDisposable;
        private IDisposable _buttonTwoDisposable;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // tests can be inside the main assembly

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Button buttonOne = FindViewById<Button>(Resource.Id.button1);
            Button buttonTwo = FindViewById<Button>(Resource.Id.button2);
            
            _buttonOneDisposable = Observable.FromEventPattern<EventHandler, EventArgs>(eh => buttonOne.Click += eh, eh => buttonOne.Click -= eh)
                .Subscribe(x => SelectFragment(0));
            _buttonTwoDisposable = Observable.FromEventPattern<EventHandler, EventArgs>(eh => buttonTwo.Click += eh, eh => buttonTwo.Click -= eh)
                .Subscribe(x => SelectFragment(1));

            SelectFragment(0);
        }

        void SelectFragment(int index)
        {
            FragmentTransaction ft = FragmentManager.BeginTransaction();
            Fragment fragment = null;
            fragment = new PictureFragment();

            ft.Replace(Resource.Id.fragment_container, fragment);

            ft.Commit();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _buttonOneDisposable?.Dispose();
            _buttonTwoDisposable?.Dispose();
        }
    }
}