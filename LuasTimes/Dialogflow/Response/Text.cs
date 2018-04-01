using Newtonsoft.Json;
using System.Collections.Generic;

namespace LuasTimes.Dialogflow.Response
{
	public class Text
	{
		public Text()
		{
			TextList = new List<string>();
		}


		public Text(string text)
		{
			TextList = new List<string> { text };
		}


		public Text(List<string> textList)
		{
			TextList = textList;
		}


		[JsonProperty(PropertyName = "text", NullValueHandling = NullValueHandling.Ignore)]
		public List<string> TextList { get; set; }
	}
}
