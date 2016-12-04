using System;
using System.Diagnostics;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace ViewHolderMemoryTest
{
    public class TitleViewHolder : RecyclerView.ViewHolder
    {
        public TitleViewHolder(View itemView) : base(itemView)
        {
            TextView = itemView.FindViewById<TextView>(Resource.Id.textView1);
        }

        public TextView TextView { get; set; }
    }

    public class PictureViewHolder : RecyclerView.ViewHolder
    {
        public PictureViewHolder(View itemView) : base(itemView)
        {
            ImageView = itemView.FindViewById<ImageView>(Resource.Id.imageView);
            ProgressBar = itemView.FindViewById<ProgressBar>(Resource.Id.progressBar);
        }

        public ImageView ImageView { get; }
        public ProgressBar ProgressBar { get; }

        private void ImageViewOnClick(object sender, EventArgs eventArgs)
        {
            Debug.WriteLine("Clicked!");
        }

        public void Init()
        {
            ImageView.Click += ImageViewOnClick;
        }

        public void Cleanup()
        {
            ImageView.Click -= ImageViewOnClick;
            ImageView.SetImageResource(0);
            ProgressBar.Visibility = ViewStates.Visible;
        }
    }
}