using System;
using Alexa.NET.Response;
using Dialogflow.NET;
using Dialogflow.NET.Response;

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
			ResponseBuilder builder = new ResponseBuilder();
			return builder.Reply(string.Format(Properties.Resources.Welcome, Properties.Resources.AppName));
		}
	}
}
