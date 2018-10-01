using Alexa.NET.Response;
using Dialogflow.NET.Response;

namespace LuasTimes.Intents
{
	internal interface IIntent
	{
		SkillResponse GetAlexaSkillResponse();

		V2Response GetDialogFlowResponse();
	}
}
