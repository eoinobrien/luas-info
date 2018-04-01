using Newtonsoft.Json;
using System.Collections.Generic;

namespace LuasTimes.Dialogflow.Response
{
	public class SimpleResponses
	{
		public SimpleResponses(SimpleResponse simpleResponse)
		{
			SimpleResponsesList = new List<SimpleResponse>();
			SimpleResponsesList.Add(simpleResponse);
		}

		[JsonProperty(PropertyName = "simpleResponses", NullValueHandling = NullValueHandling.Ignore)]
		public List<SimpleResponse> SimpleResponsesList { get; set; }
	}


	public class SimpleResponse
	{
		[JsonProperty(PropertyName = "textToSpeech", NullValueHandling = NullValueHandling.Ignore)]
		public string TextToSpeech { get; set; }

		[JsonProperty(PropertyName = "ssml", NullValueHandling = NullValueHandling.Ignore)]
		public string Ssml { get; set; }

		[JsonProperty(PropertyName = "displayText", NullValueHandling = NullValueHandling.Ignore)]
		public string DisplayText { get; set; }
	}
}
