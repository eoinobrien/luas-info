using Alexa.NET;
using Alexa.NET.Response;
using Dialogflow.NET.Response;
using LuasAPI.NET;
using LuasAPI.NET.Forecast;
using LuasAPI.NET.Stations;
using LuasTimes.Responses;
using Microsoft.Azure.WebJobs.Host;

namespace LuasTimes.Intents
{
	public class LuasTimesIntent : IIntent
	{
		private IRealTimeInfo forcast;

		public LuasTimesIntent(TraceWriter log, string origin = null, string direction = null, string destination = null)
		{
			OriginStation = Station.GetFromNameOrAbbreviation(origin);
			Direction = direction.ParseDirection();
			DestinationStation = Station.GetFromNameOrAbbreviation(destination);

			log.Info(string.Format("Parameters: Station: '{0}', Direction: '{1}', Destination: '{2}'.", OriginStation == null ? "null" : OriginStation.Name, Direction, DestinationStation == null ? "null" : DestinationStation.Name));

			if (OriginStation != null)
			{
				forcast = OriginStation.GetRealTimeInfo();

				// If there is a Destination Staion, use the Direction between the origin and station, instead of the provided station.
				if (DestinationStation != null)
				{
					Direction = OriginStation.GetDirection(DestinationStation) != Direction.Undefined ? OriginStation.GetDirection(DestinationStation) : Direction;
				}
			}
		}


		public Station OriginStation { get; set; }


		public Direction Direction { get; set; }


		public Station DestinationStation { get; set; }


		public SkillResponse GetAlexaSkillResponse()
		{
			LuasTimesResponse responseBuilder = new LuasTimesResponse(OriginStation, Direction, DestinationStation);

			var speech = new Alexa.NET.Response.PlainTextOutputSpeech
			{
				Text = responseBuilder.Text
			};

			return ResponseBuilder.Tell(speech);
		}


		public V2Response GetDialogFlowResponse()
		{
			LuasTimesResponse luasTimesResponse = new LuasTimesResponse(OriginStation, Direction, DestinationStation);
			Dialogflow.NET.ResponseBuilder builder = new Dialogflow.NET.ResponseBuilder();

			return builder.Reply(luasTimesResponse.Text, luasTimesResponse.Ssml);
		}


		private string GetDirectedResponse()
		{
			return string.Empty;
		}
	}
}
