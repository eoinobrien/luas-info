using System;
using System.Linq;
using LuasTimes;
using NUnit.Framework;

namespace LuasTimesTests
{
	[TestFixture]
	public class StationTests
	{
		[Test]
		public void TwoStations_CorrectDirection()
		{
			Tuple<Station, Station, Direction>[] combos = new Tuple<Station, Station, Direction>[]
			{
				new Tuple<Station, Station, Direction>(Station.Stations.First(s => s.Abbreviation == "TPT"), Station.Stations.First(s => s.Abbreviation == "ABB"), Direction.Outbound),
				new Tuple<Station, Station, Direction>(Station.Stations.First(s => s.Abbreviation == "ABB"), Station.Stations.First(s => s.Abbreviation == "TPT"), Direction.Inbound),
				new Tuple<Station, Station, Direction>(Station.Stations.First(s => s.Abbreviation == "ABB"), Station.Stations.First(s => s.Abbreviation == "JER"), Direction.Outbound),
				new Tuple<Station, Station, Direction>(Station.Stations.First(s => s.Abbreviation == "ABB"), Station.Stations.First(s => s.Abbreviation == "SAG"), Direction.Outbound),
				new Tuple<Station, Station, Direction>(Station.Stations.First(s => s.Abbreviation == "CPK"), Station.Stations.First(s => s.Abbreviation == "SAN"), Direction.Inbound),
				new Tuple<Station, Station, Direction>(Station.Stations.First(s => s.Abbreviation == "CPK"), Station.Stations.First(s => s.Abbreviation == "BRO"), Direction.Inbound),
				new Tuple<Station, Station, Direction>(Station.Stations.First(s => s.Abbreviation == "BRO"), Station.Stations.First(s => s.Abbreviation == "CPK"), Direction.Outbound),
				new Tuple<Station, Station, Direction>(Station.Stations.First(s => s.Abbreviation == "CPK"), Station.Stations.First(s => s.Abbreviation == "ABB"), Direction.Undefined),
				new Tuple<Station, Station, Direction>(Station.Stations.First(s => s.Abbreviation == "ABB"), Station.Stations.First(s => s.Abbreviation == "CPK"), Direction.Undefined),
			};


			foreach(Tuple<Station, Station, Direction> testCombo in combos)
			{
				Assert.AreEqual(testCombo.Item1.GetDirection(testCombo.Item2), testCombo.Item3);
			}
		}
	}
}
