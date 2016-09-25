using System;
using Android.App;
using Android.Widget;
using System.Linq;
using Android.Views;
using System.Collections.Generic;
using RSSReader;

namespace RSSReader
{
	public class FeedAdapter : BaseAdapter<RssItem>
	{
		private List<RssItem> _items;
		private Activity _context;

		public FeedAdapter( Activity context, List<RssItem> items) : base()
		{
			_context = context;
			_items = items;
		}

		public override RssItem this[int position]
		{
			get { return _items[position]; }
		}

		public override int Count
		{
			get { return _items.Count(); }
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
				view = _context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);
			}

			view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = _items[position].SrcTitle + ": " + _items[position].Title;
			view.FindViewById<TextView>(Android.Resource.Id.Text2).Text = "On " + _items[position].PubDate;

			return view;
		}
	}
}

