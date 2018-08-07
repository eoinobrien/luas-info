using Alexa.NET.Response;
using LuasTimes.Dialogflow.Response;

namespace LuasTimes.Intents
{
	internal interface IIntent
	{
		SkillResponse GetAlexaSkillResponse();

		V2Response GetDialogFlowResponse();
	}
}
