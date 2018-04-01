using Newtonsoft.Json;

namespace LuasTimes.Dialogflow.Request
{
	public class OriginalDetectIntentRequest
	{
		[JsonProperty(PropertyName = "payload")]
		public object Payload { get; set; }
	}
}
