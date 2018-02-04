namespace LuasTimes.Responses
{
	class SimpleResponse : IResponse
	{
		public string Text { get; private set; }

		public string Ssml { get; private set; }


		public SimpleResponse() { }

		public SimpleResponse(string text)
		{
			Text = text;
			Ssml = text;
		}
	}
}
