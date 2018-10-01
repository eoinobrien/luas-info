namespace LuasTimes.Responses
{
	public interface IResponse
	{
		string Text { get; }

		string Ssml { get; }
	}
}
