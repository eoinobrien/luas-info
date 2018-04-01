using Newtonsoft.Json;

namespace LuasTimes.Dialogflow.Request
{
	public class V2Request
	{
		[JsonProperty(PropertyName = "responseId")]
		public string ResponseId { get; set; }

		[JsonProperty(PropertyName = "queryResult")]
		public QueryResult QueryResult { get; set; }

		[JsonProperty(PropertyName = "originalDetectIntentRequest")]
		public OriginalDetectIntentRequest OriginalDetectIntentRequest { get; set; }

		[JsonProperty(PropertyName = "session")]
		public string Session { get; set; }
	}
}
