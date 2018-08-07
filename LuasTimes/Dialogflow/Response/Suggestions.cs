using Newtonsoft.Json;
using System.Collections.Generic;

namespace LuasTimes.Dialogflow.Response
{
	public class Suggestions
	{
		[JsonProperty(PropertyName = "suggestions", NullValueHandling = NullValueHandling.Ignore)]
		public List<Suggestion> SuggestionsList { get; set; }



		[JsonProperty(PropertyName = "title", NullValueHandling = NullValueHandling.Ignore)]
		public string Title { get; set; }
	}
}
