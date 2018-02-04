using LuasTimes.Dialogflow;
using LuasTimes.Responses;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace LuasTimes
{
	public static class DialogFlowLuasTimes
	{
		[FunctionName("DialogFlowLuasTimes")]
		public static async Task<HttpResponseMessage> Run(
			[HttpTrigger(AuthorizationLevel.Function, "post", Route = "dialogflow/")]HttpRequestMessage req,
			TraceWriter log)
		{
			log.Info("C# HTTP trigger function processed a request.");

			// Get request body
			DialogflowRequest dialogflowRequest = await req.Content.ReadAsAsync<DialogflowRequest>();
			Parameters parameters = dialogflowRequest.QueryResult.Parameters;

			Station station = Station.Stations.FirstOrDefault(st => st.Abbreviation == parameters.Station);
			Direction direction = parameters.Direction.ParseDirection();
			Station destinationStation = Station.Stations.FirstOrDefault(st => st.Abbreviation == parameters.DestinationStation);

			if (station != null)
			{
				IResponse responseBuilder = new LuasTimesResponse(station, direction, destinationStation);
				DialogFlowResponse response = new DialogFlowResponse(responseBuilder);

				return req.CreateResponse(HttpStatusCode.OK,
					JsonConvert.SerializeObject(response,
					Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
			}

			return req.CreateResponse(HttpStatusCode.BadRequest,
				JsonConvert.SerializeObject(new DialogFlowResponse(new SimpleResponse("Unknown Station"))));
		}
	}
}
