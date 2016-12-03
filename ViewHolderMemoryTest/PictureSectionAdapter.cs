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
using Android.Views;
using Android.Widget;
using ViewHolderMemoryTest.Core.Models.Seti;
using ViewHolderMemoryTest.Core.Services;
using Object = Java.Lang.Object;

namespace ViewHolderMemoryTest
{
    public class PictureSectionAdapter : RecyclerView.Adapter
    {
        private readonly Context _context;

        private string[] _planets = { "Venus", "Earth", "Mars", "Jupiter", "Saturn", "Uranus", "Neptune"};
        public PictureSectionAdapter(Context context)
        {
            _context = context;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            PictureSectionViewHolder viewHolder = (PictureSectionViewHolder)holder;
            if (viewHolder.RecyclerView == null)
            {
                viewHolder.Init();
            }
            viewHolder.Update(_planets[position]);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context)
               .Inflate(Resource.Layout.PictureSectionLayout, parent, false);

            PictureSectionViewHolder viewHolder = new PictureSectionViewHolder(view, _context);
            return viewHolder;
        }

        public override int ItemCount => _planets.Length;
        public override void OnViewRecycled(Object holder)
        {
            base.OnViewRecycled(holder);
            PictureSectionViewHolder viewHolder = (PictureSectionViewHolder) holder;
            viewHolder.Cleanup();
        }
    }
}