using Newtonsoft.Json;

namespace LuasTimes.Dialogflow.Request
{
	public class Intent
	{
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		[JsonProperty(PropertyName = "displayName")]
		public string DisplayName { get; set; }
	}
}
