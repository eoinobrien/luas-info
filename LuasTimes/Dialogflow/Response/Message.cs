using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace LuasTimes.Dialogflow.Response
{
	public class Message
	{
		public Message(Platform platform)
		{
			Platform = platform;
		}


		[JsonProperty(PropertyName = "platform", NullValueHandling = NullValueHandling.Ignore)]
		[JsonConverter(typeof(StringEnumConverter))]
		public Platform Platform { get; set; }

		[JsonProperty(PropertyName = "text", NullValueHandling = NullValueHandling.Ignore)]
		public Text Text { get; set; }

		[JsonProperty(PropertyName = "image", NullValueHandling = NullValueHandling.Ignore)]
		public Image Image { get; set; }

		[JsonProperty(PropertyName = "quickReplies", NullValueHandling = NullValueHandling.Ignore)]
		public QuickReplies QuickReplies { get; set; }

		[JsonProperty(PropertyName = "card", NullValueHandling = NullValueHandling.Ignore)]
		public Card Card { get; set; }

		[JsonProperty(PropertyName = "payload", NullValueHandling = NullValueHandling.Ignore)]
		public Dictionary<string, string> Payload { get; set; }

		[JsonProperty(PropertyName = "simpleResponses", NullValueHandling = NullValueHandling.Ignore)]
		public SimpleResponses SimpleResponses { get; set; }

		[JsonProperty(PropertyName = "basicCard", NullValueHandling = NullValueHandling.Ignore)]
		public BasicCard BasicCard { get; set; }

		[JsonProperty(PropertyName = "suggestions", NullValueHandling = NullValueHandling.Ignore)]
		public Suggestions Suggestions { get; set; }

		[JsonProperty(PropertyName = "linkOutSuggestion", NullValueHandling = NullValueHandling.Ignore)]
		public LinkOutSuggestion LinkOutSuggestion { get; set; }

		[JsonProperty(PropertyName = "listSelect", NullValueHandling = NullValueHandling.Ignore)]
		public ListSelect ListSelect { get; set; }

		[JsonProperty(PropertyName = "carouselSelect", NullValueHandling = NullValueHandling.Ignore)]
		public CarouselSelect CarouselSelect { get; set; }
	}
}
