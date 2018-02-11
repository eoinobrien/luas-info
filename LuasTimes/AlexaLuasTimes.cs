using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using LuasTimes.Dialogflow;
using LuasTimes.Responses;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace LuasTimes
{
    public static class AlexaLuasTimes
    {
        [FunctionName("AlexaLuasTimes")]
        public static async Task<HttpResponseMessage> Run(
			[HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "alexa/")]HttpRequestMessage req,
			TraceWriter log)
        {
			log.Info("C# HTTP trigger function processed a request.");

			// Get request body
			SkillRequest request = await req.Content.ReadAsAsync<SkillRequest>();

			var requestType = request.GetRequestType();

			if (requestType == typeof(IntentRequest))
			{
				IntentRequest intentRequest = request.Request as IntentRequest;

				// check the name to determine what you should do
				if (intentRequest.Intent.Name.Equals("LuasTimes"))
				{
					// get the slots
					var firstValue = intentRequest.Intent.Slots["FirstSlot"].Value;

					Station station = Station.GetFromAbbreviation(intentRequest.Intent.Slots["station"].Resolution.Authorities.First().Values.First().Value.Id);
					Direction direction = intentRequest.Intent.Slots["direction"].Resolution.Authorities.First().Values.First().Value.Id.ParseDirection();
					Station destinationStation = Station.GetFromAbbreviation(intentRequest.Intent.Slots["destinationStation"].Resolution.Authorities.First().Values.First().Value.Id);

					if (station != null)
					{
						IResponse responseBuilder = new LuasTimesResponse(station, direction, destinationStation);
						DialogFlowResponse response = new DialogFlowResponse(responseBuilder);

						return req.CreateResponse(HttpStatusCode.OK,
							JsonConvert.SerializeObject(response,
							Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
					}
				}
			}
			else if (requestType == typeof(LaunchRequest))
			{
				// default launch path executed
			}
			//Parameters parameters = dialogflowRequest.QueryResult.Parameters;

			//Station station = Station.Stations.FirstOrDefault(st => st.Abbreviation == parameters.Station);
			//Direction direction = parameters.Direction.ParseDirection();
			//Station destinationStation = Station.Stations.FirstOrDefault(st => st.Abbreviation == parameters.DestinationStation);

			//if (station != null)
			//{
			//	IResponse responseBuilder = new LuasTimesResponse(station, direction, destinationStation);
			//	DialogFlowResponse response = new DialogFlowResponse(responseBuilder);

			//	return req.CreateResponse(HttpStatusCode.OK,
			//		JsonConvert.SerializeObject(response,
			//		Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
			//}

			return req.CreateResponse(HttpStatusCode.BadRequest,
				JsonConvert.SerializeObject(new DialogFlowResponse(new SimpleResponse("Unknown Station"))));
		}
	}
}
