using System;
using System.IO;
using System.Collections.Generic;

namespace RSSReader
{
	class SubscriptionList
	{
		static public string jsonpath = "";

		static public List<RssSubscription> currentList;

		public static void saveToJson()
		{
			SourceList data = new SourceList ();

			data.lst = currentList;

			string output = Newtonsoft.Json.JsonConvert.SerializeObject (data);
			File.WriteAllText (jsonpath, output);
		}

		public static void loadFromJson()
		{
			if (File.Exists (jsonpath)) {
				string input = File.ReadAllText (jsonpath);
				SourceList data = Newtonsoft.Json.JsonConvert.DeserializeObject<SourceList> (input);

				currentList = data.lst;
			} else {
				Console.WriteLine ("No previous list of subscriptions, creating a default.");

				currentList = new List<RssSubscription> ();

				// By default, sign people up to NPR and OSHA. Good mix of interesting content to suit any tastes.
				currentList.Add (new RssSubscription{ Url = "http://www.npr.org/rss/rss.php?id=1001", SrcTitle = "NPR News"});
				currentList.Add (new RssSubscription{ Url = "https://www.osha.gov/pls/oshaweb/newsRelease.xml", SrcTitle = "OSHA"});

				// We just made them a list, now save it.
				saveToJson ();
			}
		}
	}

	class SourceList
	{
		public List<RssSubscription> lst;
	}
}

