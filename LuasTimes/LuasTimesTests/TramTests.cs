using LuasTimes;
using NUnit.Framework;
using System;
using System.Linq;

namespace LuasTimesTests
{
	[TestFixture]

	public class TramTests
	{
		[Test]
		public void TramForcast_ParseDirectionCorrectly()
		{
			Tuple<string, Station>[] testCases = new Tuple<string, Station>[]
			{
				new Tuple<string, Station>("bride's glen", Station.GetFromName("Bride's Glen")),
				new Tuple<string, Station>("sandyford", Station.GetFromName("Sandyford")),
				new Tuple<string, Station>("the point", Station.GetFromName("The Point")),
				new Tuple<string, Station>("parnell", Station.GetFromName("Parnell")),
			};


			foreach (Tuple<string, Station> testCase in testCases)
			{
				Tram tram = new Tram() { DestinationName = testCase.Item1 };
				Assert.AreEqual(tram.Destination.Name, testCase.Item2.Name, $"Tram Forcast destination was {tram.Destination.Name} when input was {tram.DestinationName}.");
			}
		}


		[Test]
		public void TramForcast_ParseDirectionToDefault()
		{
			string[] testCases = new string[]
			{
				 "test",
				 "brides glen",
			};


			foreach (string testCase in testCases)
			{
				Tram tram = new Tram() { DestinationName = testCase };
				Assert.IsNull(tram.Destination, $"Tram Forcast destination provided was {tram.DestinationName} output {tram.Destination} should have been null.");
			}
		}


		[Test]
		public void TramForcast_TramGoesToDestination()
		{
			Tuple<Tram, Station, Direction>[] testCases = new Tuple<Tram, Station, Direction>[]
			{
				new Tuple<Tram, Station, Direction>( new Tram() { DestinationName = "Bride's Glen" }, Station.GetFromAbbreviation("CPK"), Direction.Outbound),
				new Tuple<Tram, Station, Direction>( new Tram() { DestinationName = "Parnell" }, Station.GetFromAbbreviation("STS"), Direction.Inbound),
				new Tuple<Tram, Station, Direction>( new Tram() { DestinationName = "Heuston" }, Station.GetFromAbbreviation("MUS"), Direction.Outbound),
			};


			foreach (Tuple<Tram, Station, Direction> testCase in testCases)
			{
				Assert.IsTrue(testCase.Item1.TramGoesToDestination(testCase.Item2, testCase.Item3));
			}
		}


		[Test]
		public void TramForcast_TramDoesntGoToDestination()
		{
			Tuple<Tram, Station, Direction>[] testCases = new Tuple<Tram, Station, Direction>[]
			{
				new Tuple<Tram, Station, Direction>( new Tram() { DestinationName = "Sandyford" }, Station.GetFromAbbreviation("CPK"), Direction.Outbound),
				new Tuple<Tram, Station, Direction>( new Tram() { DestinationName = "Parnell" }, Station.GetFromAbbreviation("ABB"), Direction.Inbound),
				new Tuple<Tram, Station, Direction>( new Tram() { DestinationName = "Parnell" }, Station.GetFromAbbreviation("MUS"), Direction.Inbound),
				new Tuple<Tram, Station, Direction>( new Tram() { DestinationName = "Heuston" }, Station.GetFromAbbreviation("TAL"), Direction.Outbound),
				new Tuple<Tram, Station, Direction>( new Tram() { DestinationName = "Heuston" }, Station.GetFromAbbreviation("MUS"), Direction.Inbound),
			};


			foreach (Tuple<Tram, Station, Direction> testCase in testCases)
			{
				Assert.IsFalse(testCase.Item1.TramGoesToDestination(testCase.Item2, testCase.Item3));
			}
		}

	}
}
