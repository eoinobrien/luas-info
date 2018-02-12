using LuasTimes.Dialogflow;
using LuasTimes.Responses;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace LuasTimes
{
	public static class DialogFlowLuasTimes
	{
		[FunctionName("DialogFlowLuasTimes")]
		public static async Task<HttpResponseMessage> Run(
			[HttpTrigger(AuthorizationLevel.Function, "post", Route = "dialogflow/")] HttpRequestMessage req,
			TraceWriter log)
		{
			log.Info("DialogFlow trigger function processed a request.");

			// Get request body
			DialogflowRequest request = await req.Content.ReadAsAsync<DialogflowRequest>();
			Parameters parameters = request.QueryResult.Parameters;

			Station station = Station.GetFromAbbreviation(parameters.Station);
			Direction direction = parameters.Direction.ParseDirection();
			Station destinationStation = Station.GetFromAbbreviation(parameters.DestinationStation);

			log.Info(string.Format("Parameters: Station: '{0}', Direction: '{1}', Destination: '{2}'.", station == null ? "null" : station.Name, direction, destinationStation == null ? "null" : destinationStation.Name));

			if (station != null)
			{
				IResponse responseBuilder = new LuasTimesResponse(station, direction, destinationStation);
				DialogFlowResponse response = new DialogFlowResponse(responseBuilder);

				return req.CreateResponse(HttpStatusCode.OK, response);

				//return req.CreateResponse(HttpStatusCode.OK,
				//	JsonConvert.SerializeObject(response,
				//	Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
			}

			return req.CreateResponse(HttpStatusCode.OK, new DialogFlowResponse(new SimpleResponse("Unknown Station")));

			//return req.CreateResponse(HttpStatusCode.BadRequest,
				//JsonConvert.SerializeObject());
		}
	}
}
