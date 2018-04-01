using Newtonsoft.Json;
using System.Collections.Generic;

namespace LuasTimes.Dialogflow.Response
{
	public class EventInput
	{
		[JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
		public string Name { get; set; }

		[JsonProperty(PropertyName = "languageCode", NullValueHandling = NullValueHandling.Ignore)]
		public string LanguageCode { get; set; }

		[JsonProperty(PropertyName = "parameters", NullValueHandling = NullValueHandling.Ignore)]
		public Dictionary<string, string> Parameters { get; set; }
	}
}
