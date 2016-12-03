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

        public PictureRowAdapter(Context context)
        {
            _context = context;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
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
            View view = LayoutInflater.From(parent.Context)
                .Inflate(Resource.Layout.PictureView, parent, false);

            PictureViewHolder viewHolder = new PictureViewHolder(view);
            return viewHolder;
        }

        public override void OnViewRecycled(Object holder)
        {
            base.OnViewRecycled(holder);
            PictureViewHolder viewHolder = (PictureViewHolder) holder;
            Picasso.With(_context)
                .CancelRequest(viewHolder.ImageView);
            viewHolder.Cleanup();
        }

        public override int ItemCount => _urls?.Length ?? 0;

        public void Update(string[] urls)
        {
            _urls = urls;
            NotifyDataSetChanged();
        }
    }
}