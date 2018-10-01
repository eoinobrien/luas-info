using LuasAPI.NET;
using Xunit;

namespace LuasTimesTests
{
	public class DirectionExtensionsTests
	{
		[Fact]
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
				"outBOUND",
			};

			foreach (string dir in directions)
			{
				Assert.True((dir.ParseDirection() == Direction.Inbound) || (dir.ParseDirection() == Direction.Outbound));
			}
		}

		[Fact]
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
				Assert.True(dir.ParseDirection() == Direction.Undefined);
			}
		}
	}
}
