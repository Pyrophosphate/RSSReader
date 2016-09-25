using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
//using Xamarin.Media;
using System.Linq;
using System.Xml.Linq;
using System.Net.Http;
using System.IO;

namespace RSSReader
{
	[Activity (Label = "RSS", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : ListActivity, GestureDetector.IOnGestureListener
	{
		private GestureDetector _gestureDetector;
		private int differential = 7;
		private int velocityCAP = 300;
		private List<RssItem> items;


		protected override void OnCreate (Bundle bundle)
		{
			_gestureDetector = new GestureDetector(this);
			//SETUP
			base.OnCreate (bundle);

			// Set up JSON for the subscription list.
			SubscriptionList.jsonpath = Path.Combine(System.Environment.GetFolderPath (System.Environment.SpecialFolder.MyDocuments), "rssfeeds.json");
			SubscriptionList.loadFromJson ();

			items = new List<RssItem> ();

			SetContentView (Resource.Layout.Main);

			Button SubscriptionBtn = FindViewById<Button> (Resource.Id.subBtn);

			SubscriptionBtn.Click += delegate {
				StartActivity(typeof(SubscriptionsActivity));
			};

			Button RefreshBtn = FindViewById<Button> (Resource.Id.refBtn);

			RefreshBtn.Click += delegate {
				items = new List<RssItem>();
				PopulateList();
			};

			PopulateList ();
		}


		private async void PopulateList()
		{
			// Load up some RSS feeds.
			foreach(RssSubscription src in SubscriptionList.currentList)
			{
				try{
					using (var client = new HttpClient())
					{
						var xmlFeed = await client.GetStringAsync (src.Url);
						var doc = XDocument.Parse(xmlFeed);
						XNamespace dc = "http://purl.org/dc/elements/1.1/";

						items.AddRange( (from item in doc.Descendants("item")
							select new RssItem
							{
								SrcTitle = src.SrcTitle,// item.Parent.Element("title").Value,
								Title = item.Element("title").Value,
								PubDate = item.Element("pubDate").Value,
								Link = item.Element("link").Value
							}));
					}
				}
				catch (Exception e)
				{
					continue;
				}
			}

			ListAdapter = new FeedAdapter(this, items);
		}


		protected override void OnListItemClick(ListView l, View v, int position, long id)
		{
			base.OnListItemClick(l, v, position, id);

			var second = new Intent(this, typeof(WebActivity));
			second.PutExtra("link", items[position].Link);
			StartActivity(second);
		}

		public override bool OnTouchEvent(MotionEvent e)
		{
			_gestureDetector.OnTouchEvent(e);
			return false;
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
					//StartActivity (typeof(...));
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
