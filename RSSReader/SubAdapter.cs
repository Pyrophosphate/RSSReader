using System;
using Android.Widget;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using System.Linq;

namespace RSSReader
{
	public class SubAdapter : BaseAdapter<RssSubscription>
	{
		private Activity _context;

		public SubAdapter( Activity context) : base()
		{
			_context = context;
		}

		public override RssSubscription this[int position]
		{
			get { return SubscriptionList.currentList [position]; }
		}

		public override int Count
		{
			get { return SubscriptionList.currentList.Count(); }
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var view = convertView;
			if (view == null)
			{
				view = _context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
			}

			view.FindViewById<TextView> (Android.Resource.Id.Text1).Text = SubscriptionList.currentList[position].SrcTitle + ": " + SubscriptionList.currentList[position].Url;

			return view;
		}
	}
}

