﻿using LuasTimes.Dialogflow.Response;

namespace LuasTimes
{
	public class DialogFlowResponse
	{
		public static V2Response Tell(string text)
		{
			return Tell(text, $"<speak>{text}</speak>");
		}


		public static V2Response Tell(string text, string ssml)
		{
			V2Response res = new V2Response();

			res.FulfillmentText = text;

			res.FulfillmentMessages.Add(
				new Message(Platform.PLATFORM_UNSPECIFIED)
				{
					Text = new Text(text),
				});

			res.FulfillmentMessages.Add(
				new Message(Platform.ACTIONS_ON_GOOGLE)
				{
					SimpleResponses = new SimpleResponses(
						new SimpleResponse()
						{
							DisplayText = text,
							Ssml = ssml,
						}),
				});

			return res;
		}
	}
}
