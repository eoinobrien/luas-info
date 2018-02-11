using LuasTimes;
using NUnit.Framework;
using System;


namespace LuasTimesTests
{
	[TestFixture]
	public class DirectionExtensionsTests
	{
		[Test]
		public void ValidDirections_ReturnValidDirection()
		{
			string[] directions = new string[]
			{
				"inbound",
				"outbound",
				"INBOUND",
				"OUTBOUND",
				"Inbound",
				"Outbound",
				"InBOUnd",
				"outBOUND"
			};


			foreach(string dir in directions)
			{
				Assert.IsTrue((dir.ParseDirection() == Direction.Inbound) || (dir.ParseDirection() == Direction.Outbound));
			}
		}


		[Test]
		public void InvalidDirections_ReturnUndefinedDirection()
		{
			string[] directions = new string[]
			{
				"towards town",
				"invalid",
				"undefined",
				"outwards",
				"OUTWARDS"
			};


			foreach (string dir in directions)
			{
				Assert.IsTrue(dir.ParseDirection() == Direction.Undefined);
			}
		}
	}
}
