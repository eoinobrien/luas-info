namespace LuasTimes.Responses
{
	class SimpleResponse : IResponse
	{
		public string Text { get; private set; }

		public string Ssml { get; private set; }


		public SimpleResponse() { }

		public SimpleResponse(string text, string ssml = null)
		{
			Text = text;
			if (string.IsNullOrWhiteSpace(ssml))
			{
				Ssml = text;
			}
			else
			{
				Ssml = ssml;
			}
		}
	}
}
