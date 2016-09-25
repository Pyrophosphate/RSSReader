
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Webkit;
using RSSReader;

namespace RSSReader
{
	[Activity(Label = "Web")]
	public class WebActivity : Activity, GestureDetector.IOnGestureListener
	{
		private GestureDetector _gestureDetector;
		private int differential = 7;
		private int velocityCAP = 300;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			_gestureDetector = new GestureDetector (this);

			SetContentView(Resource.Layout.WebActivity);

			WebView view = FindViewById<WebView>(Resource.Id.DetailView);

			view.LoadUrl(Intent.GetStringExtra("link"));
		}


		//GESTURE ACTIVITY
		public override bool OnTouchEvent(MotionEvent e)
		{
			_gestureDetector.OnTouchEvent(e);
			return false;
		}
		public bool OnDown(MotionEvent e)
		{
			return false;
		}
		public bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
		{
			if ((e1.GetX () + differential) < e2.GetX ()) {
				if (Math.Abs (velocityX) >= velocityCAP) {				} 	
			}
			if ((e1.GetX () + differential) > e2.GetX ()) {
				if (Math.Abs (velocityX) >= velocityCAP) {
					StartActivity (typeof(MainActivity));
				} 	
			}
			return true;

		}
		public void OnLongPress(MotionEvent e) {}
		public bool OnScroll(MotionEvent e1, MotionEvent e2, float distanceX, float distanceY)
		{
			return false;
		}
		public void OnShowPress(MotionEvent e) {}
		public bool OnSingleTapUp(MotionEvent e)
		{
			return false;
		}
	}
}

