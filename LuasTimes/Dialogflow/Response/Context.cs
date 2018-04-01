using Newtonsoft.Json;
using System.Collections.Generic;

namespace LuasTimes.Dialogflow.Response
{
	public class Context
	{
		[JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
		public string Name { get; set; }

		[JsonProperty(PropertyName = "lifespanCount", NullValueHandling = NullValueHandling.Ignore)]
		public int LifespanCount { get; set; }

		[JsonProperty(PropertyName = "parameters", NullValueHandling = NullValueHandling.Ignore)]
		public Dictionary<string, string> Parameters { get; set; }
	}
}
