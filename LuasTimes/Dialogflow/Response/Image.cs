using Newtonsoft.Json;

namespace LuasTimes.Dialogflow.Response
{
	public class Image
	{
		[JsonProperty(PropertyName = "imageUri", NullValueHandling = NullValueHandling.Ignore)]
		public string ImageUri { get; set; }
	}
}
