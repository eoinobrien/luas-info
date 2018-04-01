using Newtonsoft.Json;
using System.Collections.Generic;

namespace LuasTimes.Dialogflow.Response
{
	public class Item
	{
		[JsonProperty(PropertyName = "info", NullValueHandling = NullValueHandling.Ignore)]
		public SelectItemInfo Info { get; set; }

		[JsonProperty(PropertyName = "title", NullValueHandling = NullValueHandling.Ignore)]
		public string Title { get; set; }

		[JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
		public string Description { get; set; }

		[JsonProperty(PropertyName = "image", NullValueHandling = NullValueHandling.Ignore)]
		public Image Image { get; set; }
	}


	public class SelectItemInfo
	{
		[JsonProperty(PropertyName = "key", NullValueHandling = NullValueHandling.Ignore)]
		public string Key { get; set; }

		[JsonProperty(PropertyName = "synonyms", NullValueHandling = NullValueHandling.Ignore)]
		public List<string> Synonyms { get; set; }
	}
}
