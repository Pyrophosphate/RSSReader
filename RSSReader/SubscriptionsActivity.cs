
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

namespace RSSReader
{
	[Activity (Label = "Subscriptions")]
	public class SubscriptionsActivity : ListActivity, GestureDetector.IOnGestureListener
	{
		private GestureDetector _gestureDetector;
		private int differential = 7;
		private int velocityCAP = 300;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			_gestureDetector = new GestureDetector(this);


			SetContentView (Resource.Layout.Subscriptions);

			Button NewBtn = FindViewById<Button> (Resource.Id.newBtn);

			NewBtn.Click += delegate {
				var second = new Intent(this, typeof(SubEditorActivity));
				second.PutExtra("position", SubscriptionList.currentList.Count());
				second.PutExtra ("isNew", true);
				StartActivity(second);
			};


			// Create a list of things.
			ListAdapter = new SubAdapter(this);
		}

		protected override void OnListItemClick(ListView l, View v, int position, long id)
		{
			base.OnListItemClick(l, v, position, id);

			var second = new Intent(this, typeof(SubEditorActivity));
			second.PutExtra("position", position);
			second.PutExtra ("isNew", false);
			StartActivity(second);
		}

		public override bool OnTouchEvent(MotionEvent e)
		{
			_gestureDetector.OnTouchEvent(e);
			return false;
		}

		protected override void OnResume ()
		{
			base.OnResume ();

			ListAdapter = new SubAdapter(this);
		}


		//GESTURE FUNCTIONALITY
		public bool OnDown(MotionEvent e)
		{
			return false;
		}
		public bool OnFling(MotionEvent e1, MotionEvent e2, float velocityX, float velocityY)
		{
			if ((e1.GetX () + differential) < e2.GetX ()) {
				if (Math.Abs (velocityX) >= velocityCAP) {
					StartActivity (typeof(MainActivity));
				} 	
			}
			if ((e1.GetX () + differential) > e2.GetX ()) {
				if (Math.Abs (velocityX) >= velocityCAP) {
					//StartActivity (typeof(...));
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

