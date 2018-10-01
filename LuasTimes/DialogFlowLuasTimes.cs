using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Dialogflow.NET;
using Dialogflow.NET.Request;
using Dialogflow.NET.Response;
using LuasTimes.Intents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

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
				WelcomeIntent welcomeIntent = new WelcomeIntent();
				return req.CreateResponse(HttpStatusCode.OK, welcomeIntent.GetDialogFlowResponse());
			}
			else if (intentName == "LuasTimes")
			{
				dynamic parameters = request.QueryResult.Parameters;
				LuasTimesIntent luasTimes = new LuasTimesIntent(log, parameters.station, parameters.direction, parameters.destinationStation);
				response = luasTimes.GetDialogFlowResponse();

				return req.CreateResponse(HttpStatusCode.OK, response);
			}

			ResponseBuilder builder = new ResponseBuilder();

			// Fallback
			response = builder.Reply("Unknown Request.");

			return req.CreateResponse(HttpStatusCode.BadRequest, response);
		}
	}
}
