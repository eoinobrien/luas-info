using Newtonsoft.Json;

namespace LuasTimes.Dialogflow.Response
{
	public class LinkOutSuggestion
	{
		[JsonProperty(PropertyName = "destinationName", NullValueHandling = NullValueHandling.Ignore)]
		public string DestinationName { get; set; }


		[JsonProperty(PropertyName = "uri", NullValueHandling = NullValueHandling.Ignore)]
		public string Uri { get; set; }
	}
}
