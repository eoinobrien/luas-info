using System;
using System.Collections.Generic;
using System.Linq;
using LuasAPI.NET;
using LuasAPI.NET.Forecast;
using LuasAPI.NET.Stations;
using LuasTimes.Extensions;

namespace LuasTimes.Responses
{
	public class LuasTimesResponse : IResponse
	{
		public LuasTimesResponse(Station origin, Direction direction = Direction.Undefined, Station destination = null)
		{
			Origin = origin;
			RealTimeInfo = origin.GetRealTimeInfo();
			Direction = direction;
			Destination = destination;

			if (Destination != null)
			{
				Direction = origin.GetDirection(destination) != Direction.Undefined ? origin.GetDirection(destination) : Direction;
			}

			Directions = RealTimeInfo.Directions;

			Ssml = "<speak>";
			ConstuctText();
			Ssml += "</speak>";
		}


		public Station Origin { get; private set; }


		public IRealTimeInfo RealTimeInfo { get; private set; }


		public Direction Direction { get; private set; }


		public Station Destination { get; private set; }


		public List<ForecastDirection> Directions { get; private set; }


		public bool ToUsersDestination { get; private set; }


		public string Text { get; private set; }


		public string Ssml { get; private set; }


		private void ConstuctText()
		{
			if (Direction != Direction.Undefined)
			{
				List<Tram> trams = Directions.ElementAt((int)Direction).Trams;

				if (trams.All(t => t.NoTramsForcast))
				{

					Text += string.Format(Properties.Resources.Time_NoTramsForcast_Direction, Direction.ToString().ToLower(), Origin.Name);
					Ssml += string.Format(Properties.Resources.Time_NoTramsForcast_Direction, Direction.ToString().ToLower(), Origin.Pronunciation);
					return;
				}

				if (Destination != null)
				{
					ToUsersDestination = Directions.ElementAt((int)Direction)
						.Trams
						.Any(t => t.TramGoesToDestination(Destination, Direction));

					if (!ToUsersDestination)
					{
						Text += string.Format(Properties.Resources.Time_NoTramsToDestination, Destination.Name, Origin.Name, trams.First().DestinationStation.Name);
						Ssml += string.Format(Properties.Resources.Time_NoTramsToDestination, Destination.Pronunciation, Origin.Pronunciation, trams.First().DestinationStation.Pronunciation);
					}
					else
					{
						trams = Directions.ElementAt((int)Direction)
							.Trams
							.Where(t => t.TramGoesToDestination(Destination, Direction))
							.ToList();
					}

					if (trams.First().IsDue)
					{
						Text += string.Format(Properties.Resources.Time_DueAndNext_1, Origin.Name, $"to {Destination.Name}", trams.ElementAt(1).Minutes.GetMinutesString());
						Ssml += string.Format(Properties.Resources.Time_DueAndNext_1_SSML, Origin.Pronunciation, $"to {Destination.Pronunciation}", trams.ElementAt(1).Minutes.GetMinutesString());
					}
					else
					{
						Text += string.Format(Properties.Resources.Time_ResultMinutes_1, Origin.Name, $"to {Destination.Name}", trams.First().Minutes.GetMinutesString());
						Ssml += string.Format(Properties.Resources.Time_ResultMinutes_1_SSML, Origin.Pronunciation, $"to {Destination.Pronunciation}", trams.First().Minutes.GetMinutesString());
					}
				}
			}
			else
			{
				if (Directions.All(dir => dir.Trams.All(t => t.NoTramsForcast)))
				{
					Text += string.Format(Properties.Resources.Time_NoTramsForcast, Origin.Name);
					Ssml += string.Format(Properties.Resources.Time_NoTramsForcast, Origin.Pronunciation);

					return;
				}

				foreach (Direction dir in Enum.GetValues(typeof(Direction)).Cast<Direction>())
				{
					// Ignore undefined direction, and if the stop only has trams that go one direction, ignore the other direction
					if (dir == Direction.Undefined || Origin.GetDirectionStations(dir).Count() == 0)
					{
						continue;
					}

					List<Tram> trams = Directions.ElementAt((int)dir).Trams;

					if (trams.All(t => t.NoTramsForcast))
					{
						Text += string.Format(Properties.Resources.Time_NoTramsForcast_Direction, dir.ToString().ToLower(), Origin.Name);
						Ssml += string.Format(Properties.Resources.Time_NoTramsForcast_Direction, dir.ToString().ToLower(), Origin.Pronunciation);
					}
					else
					{
						if (trams.First().IsDue)
						{
							Text += string.Format(Properties.Resources.Time_DueAndNext_1, Origin.Name, dir.ToString().ToLower(), trams.ElementAt(1).Minutes.GetMinutesString());
							Ssml += string.Format(Properties.Resources.Time_DueAndNext_1_SSML, Origin.Pronunciation, dir.ToString().ToLower(), trams.ElementAt(1).Minutes.GetMinutesString());
						}
						else
						{
							Text += string.Format(Properties.Resources.Time_ResultMinutes_1, Origin.Name, dir.ToString().ToLower(), trams.First().Minutes.GetMinutesString());
							Ssml += string.Format(Properties.Resources.Time_ResultMinutes_1_SSML, Origin.Pronunciation, dir.ToString().ToLower(), trams.First().Minutes.GetMinutesString());
						}
					}
				}

				return;
			}
		}
	}
}
