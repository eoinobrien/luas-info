using Alexa.NET.Response;

namespace LuasTimes.Intents
{
	internal interface IIntent
	{
		SkillResponse GetAlexaSkillResponse();

		// DialogFlowResponse GetDialogFlowResponse();
	}
}
