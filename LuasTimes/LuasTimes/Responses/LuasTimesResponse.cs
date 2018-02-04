using System.Collections.Generic;
using System.Linq;

namespace LuasTimes.Responses
{
	public class LuasTimesResponse : IResponse
	{
		Station Origin { get; set; }

		Forcast Forcast { get; set; }

		Direction Direction { get; set; }

		Station Destination { get; set; }

		List<ForcastDirection> Directions { get; set; }

		bool ToUsersDestination { get; set; }


		public LuasTimesResponse(Station origin, Direction direction = Direction.Undefined, Station destination = null)
		{
			Origin = origin;
			Forcast = origin.GetForcast();
			Direction = direction;
			Destination = destination;

			if (Destination != null)
			{
				Direction = origin.GetDirection(destination) != Direction.Undefined ? origin.GetDirection(destination) : Direction;
			}

			Directions = Forcast.Info.Directions;
		}


		public string Text
		{
			get
			{
				if (Direction != Direction.Undefined)
				{
					List<Tram> trams = Directions.ElementAt((int)Direction).Trams;

					if (trams.Count == 0)
						return string.Format("There are no Trams Forcast");

					return string.Format("The next tram from {0} {1}", Origin.Pronunciation, GetDirectionString(Direction));
				}
				else // Return both Directions
				{
					return string.Format("The next tram from {0}, {1} and {2}",
						Origin.Pronunciation,
						GetDirectionString(Direction.Inbound),
						GetDirectionString(Direction.Outbound));
				}
			}
		}

		public string Ssml
		{
			get
			{
				return "test";
			}
		}

		private string GetDirectionString(Direction direction)
		{
			List<Tram> trams = Directions.ElementAt((int)direction).Trams;
			if (trams.First().IsDue)
				return string.Format("{0} is Due. The following tram {0} is in {1} {2}.", direction, trams.ElementAt(1).Minutes, trams.First().Minutes > 1 ? "minutes" : "minute");

			return string.Format("{0} is in {1} {2}.", direction, trams.First().Minutes, trams.First().Minutes > 1 ? "minutes" : "minute");
		}
	}
}
