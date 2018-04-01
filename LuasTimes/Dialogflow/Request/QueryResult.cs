using Newtonsoft.Json;
using System.Dynamic;

namespace LuasTimes.Dialogflow.Request
{
	public class QueryResult
	{
		[JsonProperty(PropertyName = "queryText")]
		public string QueryText { get; set; }

		[JsonProperty(PropertyName = "parameters")]
		public ExpandoObject Parameters { get; set; }

		[JsonProperty(PropertyName = "allRequiredParamsPresent")]
		public bool AllRequiredParamsPresent { get; set; }

		[JsonProperty(PropertyName = "intent")]
		public Intent Intent { get; set; }

		[JsonProperty(PropertyName = "intentDetectionConfidence")]
		public double IntentDetectionConfidence { get; set; }

		[JsonProperty(PropertyName = "diagnosticInfo")]
		public object DiagnosticInfo { get; set; }

		[JsonProperty(PropertyName = "languageCode")]
		public string LanguageCode { get; set; }
	}
}
