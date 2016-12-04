using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Java.Lang;
using Square.Picasso;
using ViewHolderMemoryTest.Core.Services;

namespace ViewHolderMemoryTest
{
    public class PictureRowAdapter : RecyclerView.Adapter
    {
        private readonly Context _context;
        private string[] _urls;
        private string _planet;

        public PictureRowAdapter(Context context)
        {
            _context = context;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (position == 0)
            {
                TitleViewHolder titleViewHolder = (TitleViewHolder) holder;
                titleViewHolder.TextView.Text = _planet;
                return;
            }

            string url = _urls[position];
            PictureViewHolder pictureViewHolder = (PictureViewHolder) holder;
            pictureViewHolder.Init();
            Picasso.With(_context)
                .Load(url)
                .Fit()
                .CenterInside()
                .Into(pictureViewHolder.ImageView, () =>
                {
                    if (pictureViewHolder.ProgressBar != null)
                        pictureViewHolder.ProgressBar.Visibility = ViewStates.Gone;

                }, () =>
                {
                    if (pictureViewHolder.ProgressBar != null)
                        pictureViewHolder.ProgressBar.Visibility = ViewStates.Visible;
                });
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            if (viewType == 1)
            {
                View view = LayoutInflater.From(parent.Context)
                .Inflate(Resource.Layout.PictureView, parent, false);
                PictureViewHolder pictureViewHolder = new PictureViewHolder(view);
                return pictureViewHolder;
            }
            View titleView = LayoutInflater.From(parent.Context)
                .Inflate(Resource.Layout.RowLabel, parent, false);
            TitleViewHolder viewHolder = new TitleViewHolder(titleView);
            return viewHolder;
        }

        public override void OnViewRecycled(Object holder)
        {
            base.OnViewRecycled(holder);
            PictureViewHolder viewHolder = holder as PictureViewHolder;

            if (viewHolder == null)
                return;
            Picasso.With(_context)
                .CancelRequest(viewHolder.ImageView);
            viewHolder.Cleanup();
        }

        public override int GetItemViewType(int position)
        {
            if (position == 0)
            {
                return 0;
            }
            return 1;
        }

        public override int ItemCount => _urls?.Length + 1 ?? 1;

        public void Update(string[] urls, string planet)
        {
            _urls = urls;
            _planet = planet;
            NotifyDataSetChanged();
        }
    }
}