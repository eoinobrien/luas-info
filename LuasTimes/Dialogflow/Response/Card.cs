using Newtonsoft.Json;
using System.Collections.Generic;

namespace LuasTimes.Dialogflow.Response
{
	public class Card
	{
		[JsonProperty(PropertyName = "title", NullValueHandling = NullValueHandling.Ignore)]
		public string Title { get; set; }

		[JsonProperty(PropertyName = "subtitle", NullValueHandling = NullValueHandling.Ignore)]
		public string SubTitle { get; set; }

		[JsonProperty(PropertyName = "imageUri", NullValueHandling = NullValueHandling.Ignore)]
		public string ImageUri { get; set; }

		[JsonProperty(PropertyName = "buttons", NullValueHandling = NullValueHandling.Ignore)]
		public List<CardButton> Button { get; set; }
	}

	public class CardButton
	{
		[JsonProperty(PropertyName = "text", NullValueHandling = NullValueHandling.Ignore)]
		public string Text { get; set; }

		[JsonProperty(PropertyName = "postback", NullValueHandling = NullValueHandling.Ignore)]
		public string Postback { get; set; }
	}
}
