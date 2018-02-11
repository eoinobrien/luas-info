using Newtonsoft.Json;

namespace LuasTimes.Dialogflow
{
	public class DialogflowRequest
	{
		[JsonProperty(PropertyName = "responseId")]
		public string ResponseId { get; set; }

		[JsonProperty(PropertyName = "queryResult")]
		public QueryResult QueryResult { get; set; }

		[JsonProperty(PropertyName = "originalDetectIntentRequest")]
		public OriginalDetectIntentRequest OriginalDetectIntentRequest { get; set; }

		[JsonProperty(PropertyName = "session")]
		public string Session { get; set; }

	}

	public class Parameters
	{
		[JsonProperty(PropertyName = "station")]
		public string Station { get; set; }

		[JsonProperty(PropertyName = "direction")]
		public string Direction { get; set; }

		[JsonProperty(PropertyName = "destinationStation")]
		public string DestinationStation { get; set; }
	}

	public class Intent
	{
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		[JsonProperty(PropertyName = "displayName")]
		public string DisplayName { get; set; }
	}


	public class QueryResult
	{
		[JsonProperty(PropertyName = "queryText")]
		public string QueryText { get; set; }

		[JsonProperty(PropertyName = "parameters")]
		public Parameters Parameters { get; set; }

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

	public class OriginalDetectIntentRequest
	{

		[JsonProperty(PropertyName = "payload")]
		public object Payload { get; set; }
	}
}
