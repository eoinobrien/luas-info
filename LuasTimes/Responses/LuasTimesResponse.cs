using System;
using System.Collections.Generic;
using System.Linq;
using LuasTimes.Extensions;

using Response = LuasTimes.Rescources.Responses;

namespace LuasTimes.Responses
{
	public class LuasTimesResponse : IResponse
	{
		public Station Origin { get; private set; }


		public  Forcast Forcast { get; private set; }


		public Direction Direction { get; private set; }


		public Station Destination { get; private set; }


		public List<ForcastDirection> Directions { get; private set; }


		public bool ToUsersDestination { get; private set; }


		public string Text { get; private set; }


		public string Ssml { get; private set; }


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

			ConstuctText();
		}


		private void ConstuctText()
		{
			if (Direction != Direction.Undefined)
			{
				List<Tram> trams = Directions.ElementAt((int)Direction).Trams;

				if (trams.All(t => t.NoTramsForcast))
				{
					Text = string.Format(Response.Time_NoTramsForcast_Direction, Direction.ToString().ToLower(), Origin.Name);
					Ssml = string.Format(Response.Time_NoTramsForcast_Direction, Direction.ToString().ToLower(), Origin.Pronunciation);
					return;
				}

				if (Destination != null)
				{
					ToUsersDestination = Directions.ElementAt((int)Direction)
						.Trams
						.Any(t => t.TramGoesToDestination(Destination, Direction));

					if (!ToUsersDestination)
					{
						Text += string.Format(Response.Time_NoTramsToDestination, Destination.Name, Origin.Name, trams.First().Destination.Name);
						Ssml += string.Format(Response.Time_NoTramsToDestination, Destination.Pronunciation, Origin.Pronunciation, trams.First().Destination.Pronunciation);
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
						Text += string.Format(Response.Time_DueAndNext_1, Origin.Name, $"to {Destination.Name}", trams.ElementAt(1).Minutes.GetMinutesString());
						Ssml += string.Format(Response.Time_DueAndNext_1_SSML, Origin.Pronunciation, $"to {Destination.Pronunciation}", trams.ElementAt(1).Minutes.GetMinutesString());
					}
					else
					{
						Text += string.Format(Response.Time_ResultMinutes_1, Origin.Name, $"to {Destination.Name}", trams.First().Minutes.GetMinutesString());
						Ssml += string.Format(Response.Time_ResultMinutes_1_SSML, Origin.Pronunciation, $"to {Destination.Pronunciation}", trams.First().Minutes.GetMinutesString());
					}
				}
			}
			else
			{
				if (Directions.All(dir => dir.Trams.All(t => t.NoTramsForcast)))
				{
					Text = string.Format(Response.Time_NoTramsForcast, Origin.Name);
					Ssml = string.Format(Response.Time_NoTramsForcast, Origin.Pronunciation);
					return;
				}

				foreach (Direction dir in Enum.GetValues(typeof(Direction)).Cast<Direction>())
				{
					// Ignore undefined direction, and if the stop only has trams that go one direction, ignore the other direction
					if (dir == Direction.Undefined || Origin.OneWayDirection != dir)
						continue;

					List<Tram> trams = Directions.ElementAt((int)dir).Trams;

					if (trams.All(t => t.NoTramsForcast))
					{
						Text += string.Format(Response.Time_NoTramsForcast_Direction, dir.ToString().ToLower(), Origin.Name);
						Ssml += string.Format(Response.Time_NoTramsForcast_Direction, dir.ToString().ToLower(), Origin.Pronunciation);
					}
					else
					{
						if (trams.First().IsDue)
						{
							Text += string.Format(Response.Time_DueAndNext_1, Origin.Name, dir.ToString().ToLower(), trams.ElementAt(1).Minutes.GetMinutesString());
							Ssml += string.Format(Response.Time_DueAndNext_1_SSML, Origin.Pronunciation, dir.ToString().ToLower(), trams.ElementAt(1).Minutes.GetMinutesString());
						}
						else
						{
							Text += string.Format(Response.Time_ResultMinutes_1, Origin.Name, dir.ToString().ToLower(), trams.First().Minutes.GetMinutesString());
							Ssml += string.Format(Response.Time_ResultMinutes_1_SSML, Origin.Pronunciation, dir.ToString().ToLower(), trams.First().Minutes.GetMinutesString());
						}
					}
				}

				return;
			}
		}
	}
}
