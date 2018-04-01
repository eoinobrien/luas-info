using Newtonsoft.Json;
using System.Collections.Generic;

namespace LuasTimes.Dialogflow.Response
{
	public class BasicCard
	{
		[JsonProperty(PropertyName = "title", NullValueHandling = NullValueHandling.Ignore)]
		public string Title { get; set; }

		[JsonProperty(PropertyName = "subtitle", NullValueHandling = NullValueHandling.Ignore)]
		public string SubTitle { get; set; }

		[JsonProperty(PropertyName = "formattedText", NullValueHandling = NullValueHandling.Ignore)]
		public string FormattedText { get; set; }

		[JsonProperty(PropertyName = "image", NullValueHandling = NullValueHandling.Ignore)]
		public List<Image> Image { get; set; }

		[JsonProperty(PropertyName = "buttons", NullValueHandling = NullValueHandling.Ignore)]
		public List<BasicCardButton> Buttons { get; set; }
	}


	public class BasicCardButton
	{
		[JsonProperty(PropertyName = "title", NullValueHandling = NullValueHandling.Ignore)]
		public string Title { get; set; }

		[JsonProperty(PropertyName = "openUriAction", NullValueHandling = NullValueHandling.Ignore)]
		public OpenUriAction OpenUriAction { get; set; }
	}


	public class OpenUriAction
	{
		[JsonProperty(PropertyName = "uri", NullValueHandling = NullValueHandling.Ignore)]
		public string Uri { get; set; }
	}
}
