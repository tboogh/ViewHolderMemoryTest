using System;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using FFImageLoading.Views;


namespace ViewHolderMemoryTest
{
    public class PictureViewHolder : RecyclerView.ViewHolder
    {
        public ImageView ImageView { get; }
        public ProgressBar ProgressBar { get; }
        public PictureViewHolder(View itemView) : base(itemView)
        {
            ImageView = itemView.FindViewById<ImageView>(Resource.Id.imageView);
            ProgressBar = itemView.FindViewById<ProgressBar>(Resource.Id.progressBar);
        }

        private void ImageViewOnClick(object sender, EventArgs eventArgs)
        {
            System.Diagnostics.Debug.WriteLine("Clicked!");
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