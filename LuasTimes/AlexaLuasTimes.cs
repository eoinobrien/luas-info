using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using LuasAPI.NET;
using LuasAPI.NET.Stations;
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
			[HttpTrigger(AuthorizationLevel.Function, "post", Route = "alexa/")] HttpRequestMessage req,
			TraceWriter log)
		{
			log.Info("Alexa trigger function processed a request.");

			// Get request body
			SkillRequest request = await req.Content.ReadAsAsync<SkillRequest>();

			var requestType = request.GetRequestType();

			if (requestType == typeof(IntentRequest))
			{
				IntentRequest intentRequest = request.Request as IntentRequest;

				// check the name to determine what you should do
				if (intentRequest.Intent.Name.Equals("LuasTimes"))
				{
					string stationResolution = intentRequest.Intent.Slots["station"].Resolution != null && intentRequest.Intent.Slots["station"].Resolution.Authorities.First().Status.Code == ResolutionStatusCode.SuccessfulMatch ?
						intentRequest.Intent.Slots["station"].Resolution.Authorities.First().Values.First().Value.Id :
						intentRequest.Intent.Slots["station"].Value;
					Station station = Station.GetFromNameOrAbbreviation(stationResolution);


					string directionResolution = intentRequest.Intent.Slots["direction"].Resolution != null && intentRequest.Intent.Slots["direction"].Resolution.Authorities.First().Status.Code == ResolutionStatusCode.SuccessfulMatch ?
						intentRequest.Intent.Slots["direction"].Resolution.Authorities.First().Values.First().Value.Id :
						intentRequest.Intent.Slots["direction"].Value;
					Direction direction = directionResolution.ParseDirection();

					string destinationStationResolution = intentRequest.Intent.Slots["destinationStation"].Resolution != null && intentRequest.Intent.Slots["destinationStation"].Resolution.Authorities.First().Status.Code == ResolutionStatusCode.SuccessfulMatch ?
						intentRequest.Intent.Slots["destinationStation"].Resolution.Authorities.First().Values.First().Value.Id :
						intentRequest.Intent.Slots["destinationStation"].Value;
					Station destinationStation = Station.GetFromNameOrAbbreviation(destinationStationResolution);

					log.Info(string.Format("Parameters: Station: '{0}', Direction: '{1}', Destination: '{2}'.", station == null ? "null" : station.Name, direction, destinationStation == null ? "null" : destinationStation.Name));

					if (station != null)
					{
						IResponse responseBuilder = new LuasTimesResponse(station, direction, destinationStation);

						var speech = new Alexa.NET.Response.PlainTextOutputSpeech
						{
							Text = responseBuilder.Text
						};

						Alexa.NET.Response.SkillResponse response = ResponseBuilder.Tell(speech);

						return req.CreateResponse(HttpStatusCode.OK, response);
					}
				}
			}
			else if (requestType == typeof(LaunchRequest))
			{
				// default launch path executed
			}

			Alexa.NET.Response.SkillResponse defaultResponse = ResponseBuilder.Tell(new Alexa.NET.Response.PlainTextOutputSpeech { Text = "Unknown Station" });

			return req.CreateResponse(HttpStatusCode.BadRequest, JsonConvert.SerializeObject(defaultResponse));
		}
	}
}
