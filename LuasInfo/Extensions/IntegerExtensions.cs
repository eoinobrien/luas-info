namespace LuasTimes.Extensions
{
	public static class IntegerExtenstions
	{
		public static string GetMinutesString(this int minutes, bool shortString = false)
		{
			if (shortString)
			{
				return minutes == 1
					? string.Format(Properties.Resources.Time_Minutes_Single_Short, minutes)
					: string.Format(Properties.Resources.Time_Minutes_Plural_Short, minutes);
			}

			return minutes == 1
				? string.Format(Properties.Resources.Time_Minutes_Single, minutes)
				: string.Format(Properties.Resources.Time_Minutes_Plural, minutes);
		}
	}
}
