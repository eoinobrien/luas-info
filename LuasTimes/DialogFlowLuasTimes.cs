using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using LuasTimes.Dialogflow.Request;
using LuasTimes.Dialogflow.Response;
using LuasTimes.Intents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace LuasTimes
{
	public static class DialogFlowLuasTimes
	{
		/// <summary>
		/// Entry point from Azure Function.
		/// </summary>
		/// <param name="req">Http Request</param>
		/// <param name="log">Log that is later outputted to Azure</param>
		/// <returns>Http Response</returns>
		[FunctionName("DialogFlowLuasTimes")]
		public static async Task<HttpResponseMessage> Run(
			[HttpTrigger(AuthorizationLevel.Function, "post", Route = "dialogflow/")] HttpRequestMessage req,
			TraceWriter log)
		{
			log.Info("DialogFlow trigger function processed a request.");

			// Get request body
			V2Request request = await req.Content.ReadAsAsync<V2Request>();
			V2Response response;


			string intentName = request.QueryResult.Intent.DisplayName;
			log.Info($"Intent: '{request.QueryResult.Intent.DisplayName}'");

			if (intentName == "Welcome")
			{
				response = DialogFlowResponse.Tell("Hi. Try asking when is the next Luas from St. Stephen's Green.");

				return req.CreateResponse(HttpStatusCode.OK, response);
			}
			else if (intentName == "LuasTimes")
			{
				dynamic parameters = request.QueryResult.Parameters;
				LuasTimesIntent luasTimes = new LuasTimesIntent(log, parameters.station, parameters.direction, parameters.destinationStation);
				response = luasTimes.GetDialogFlowResponse();

				return req.CreateResponse(HttpStatusCode.OK, response);
			}

			// Fallback
			response = DialogFlowResponse.Tell("Unknown Request.");

			return req.CreateResponse(HttpStatusCode.BadRequest, response);
		}
	}
}
