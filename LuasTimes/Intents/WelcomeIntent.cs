using System;
using Alexa.NET.Response;
using LuasTimes.Dialogflow.Response;

namespace LuasTimes.Intents
{
	public class WelcomeIntent : IIntent
	{
		public WelcomeIntent()
		{
		}

		public SkillResponse GetAlexaSkillResponse()
		{
			throw new NotImplementedException();
		}

		public V2Response GetDialogFlowResponse()
		{
			return DialogFlowResponse.Tell(string.Format(Properties.Resources.Welcome, Properties.Resources.AppName));
		}
	}
}
