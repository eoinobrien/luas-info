using Newtonsoft.Json;
using System.Collections.Generic;

namespace LuasTimes.Dialogflow.Response
{
	public class CarouselSelect
	{
		[JsonProperty(PropertyName = "items", NullValueHandling = NullValueHandling.Ignore)]
		public List<Item> Items { get; set; }
	}
}
