
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
	[Activity (Label = "Edit Subscription")]			
	public class SubEditorActivity : Activity
	{
		private int position;
		private bool isNew;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.SubEditor);

			position = Intent.GetIntExtra ("position", -1);
			isNew = Intent.GetBooleanExtra ("isNew", true);


			EditText UrlText = FindViewById<EditText> (Resource.Id.urlTxt);
			EditText NameText = FindViewById<EditText> (Resource.Id.nameTxt);

			if (position != -1 && !isNew)
			{
				UrlText.Text = SubscriptionList.currentList [position].Url;
				NameText.Text = SubscriptionList.currentList [position].SrcTitle;
			}


			Button SaveBtn = FindViewById<Button> (Resource.Id.saveBtn);
			SaveBtn.Click += delegate
			{
				if(isNew)
				{
					SubscriptionList.currentList.Add(new RssSubscription
						{
							SrcTitle = NameText.Text,
							Url = UrlText.Text
						});
				}
				else
				{
					SubscriptionList.currentList[position] = new RssSubscription
						{
							SrcTitle = NameText.Text,
							Url = UrlText.Text
						};
				}

				SubscriptionList.saveToJson();
				this.Finish();
			};

			Button DeleteBtn = FindViewById<Button> (Resource.Id.delBtn);
			DeleteBtn.Click += delegate
			{
				if(!isNew)
					SubscriptionList.currentList.RemoveAt(position);


				SubscriptionList.saveToJson();
				this.Finish();
			};

			Button CancelBtn = FindViewById<Button> (Resource.Id.cancelBtn);
			CancelBtn.Click += delegate
			{
				this.Finish();
			};
		}
	}
}

