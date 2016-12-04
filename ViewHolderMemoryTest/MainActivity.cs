using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reflection;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Microsoft.Practices.Unity;
using ViewHolderMemoryTest.Core.Services;
using Fragment = Android.App.Fragment;
using FragmentManager = Android.App.FragmentManager;
using FragmentTransaction = Android.App.FragmentTransaction;

namespace ViewHolderMemoryTest
{
    [Activity(Label = "ViewHolderMemoryTest", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        public static  UnityContainer Container;

        private List<IDisposable> _disposables = new List<IDisposable>();
        private FragmentAdapter _fragmentAdapter;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // tests can be inside the main assembly
            Container = new UnityContainer();
            Container.RegisterType<IHttpService, HttpService>();

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            
            Button buttonOne = FindViewById<Button>(Resource.Id.button1);
            Button buttonTwo = FindViewById<Button>(Resource.Id.button2);
            Button buttonThree = FindViewById<Button>(Resource.Id.button3);
            Button buttonFour = FindViewById<Button>(Resource.Id.button4);
            Button buttonFive = FindViewById<Button>(Resource.Id.button5);

            Button[] buttons = {buttonOne, buttonTwo, buttonThree, buttonFour, buttonFive};

            for (int index = 0; index < buttons.Length; index++){
                var button = buttons[index];
                var buttonIndex = index;
                IDisposable disposable = Observable.FromEventPattern<EventHandler, EventArgs>(eh => button.Click += eh, eh => button.Click -= eh)
                    .Subscribe(x => SelectFragment(buttonIndex));
                _disposables.Add(disposable);
            }

            _fragmentAdapter = new FragmentAdapter(FragmentManager, buttons.Length);

            SelectFragment(0);
        }

        void SelectFragment(int index)
        {
            FragmentTransaction ft = FragmentManager.BeginTransaction();
            Fragment fragment = null;
            fragment = _fragmentAdapter.GetItem(index);

            ft.Replace(Resource.Id.fragment_container, fragment);

            ft.Commit();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
            _disposables.Clear();
        }
    }

    class FragmentAdapter : Android.Support.V13.App.FragmentStatePagerAdapter
    {
        private readonly int _fragmentCount;

        public FragmentAdapter(FragmentManager fm, int fragmentCount) : base(fm)
        {
            _fragmentCount = fragmentCount;
        }

        public override int Count => _fragmentCount;
        public override Fragment GetItem(int position)
        {
            return new PictureFragment();
        }
    }
}