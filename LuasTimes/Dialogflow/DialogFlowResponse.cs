using LuasTimes.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace LuasTimes.Dialogflow
{
	public class DialogFlowResponse
	{
		[JsonProperty(PropertyName = "fulfillmentText", NullValueHandling = NullValueHandling.Ignore)]
		public string FulfillmentText { get; private set; }

		[JsonProperty(PropertyName = "fulfillmentMessages", NullValueHandling = NullValueHandling.Ignore)]
		public List<Message> FulfillmentMessages { get; private set; }

		[JsonProperty(PropertyName = "source", NullValueHandling = NullValueHandling.Ignore)]
		public string Source { get; private set; }

		[JsonProperty(PropertyName = "payload", NullValueHandling = NullValueHandling.Ignore)]
		public Dictionary<string, string> Payload { get; private set; }

		[JsonProperty(PropertyName = "outputContexts", NullValueHandling = NullValueHandling.Ignore)]
		public List<Context> OutputContexts { get; private set; }

		[JsonProperty(PropertyName = "followupEventInput", NullValueHandling = NullValueHandling.Ignore)]
		public EventInput FollowupEventInput { get; private set; }

		public DialogFlowResponse(IResponse response)
		{
			CraftResponse(response);
		}


		private void CraftResponse(IResponse response)
		{
			FulfillmentText = response.Text;
			FulfillmentMessages = new List<Message>()
			{
				new Message(Platform.PLATFORM_UNSPECIFIED)
				{
					Text = new TextObj(response.Text)
				},
				new Message(Platform.ACTIONS_ON_GOOGLE)
				{
					SimpleResponses = new SimpleResponsesObj(
						new SimpleResponseObj()
						{
							DisplayText = response.Text,
							Ssml = response.Ssml,
						})
				}
			};
		}
	}


	public class Message
	{
		[JsonProperty(PropertyName = "platform", NullValueHandling = NullValueHandling.Ignore), JsonConverter(typeof(StringEnumConverter))]
		public Platform Platform { get; set; }

		[JsonProperty(PropertyName = "text", NullValueHandling = NullValueHandling.Ignore)]
		public TextObj Text { get; set; }

		[JsonProperty(PropertyName = "image", NullValueHandling = NullValueHandling.Ignore)]
		public Image Image { get; set; }

		[JsonProperty(PropertyName = "quickReplies", NullValueHandling = NullValueHandling.Ignore)]
		public QuickRepliesObj QuickReplies { get; set; }

		[JsonProperty(PropertyName = "card", NullValueHandling = NullValueHandling.Ignore)]
		public Card Card { get; set; }

		[JsonProperty(PropertyName = "payload", NullValueHandling = NullValueHandling.Ignore)]
		public Dictionary<string, string> Payload { get; set; }

		[JsonProperty(PropertyName = "simpleResponses", NullValueHandling = NullValueHandling.Ignore)]
		public SimpleResponsesObj SimpleResponses { get; set; }

		[JsonProperty(PropertyName = "basicCard", NullValueHandling = NullValueHandling.Ignore)]
		public BasicCard BasicCard { get; set; }

		[JsonProperty(PropertyName = "suggestions", NullValueHandling = NullValueHandling.Ignore)]
		public SuggestionsObj Suggestions { get; set; }

		[JsonProperty(PropertyName = "linkOutSuggestion", NullValueHandling = NullValueHandling.Ignore)]
		public LinkOutSuggestion LinkOutSuggestion { get; set; }

		[JsonProperty(PropertyName = "listSelect", NullValueHandling = NullValueHandling.Ignore)]
		public ListSelect ListSelect { get; set; }

		[JsonProperty(PropertyName = "carouselSelect", NullValueHandling = NullValueHandling.Ignore)]
		public CarouselSelect CarouselSelect { get; set; }

		public Message(Platform platform)
		{
			Platform = platform;
		}
	}


	public class Context
	{
		[JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
		public string Name { get; set; }

		[JsonProperty(PropertyName = "lifespanCount", NullValueHandling = NullValueHandling.Ignore)]
		public int LifespanCount { get; set; }

		[JsonProperty(PropertyName = "parameters", NullValueHandling = NullValueHandling.Ignore)]
		public Dictionary<string, string> Parameters { get; set; }
	}


	public class EventInput
	{
		[JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
		public string Name { get; set; }

		[JsonProperty(PropertyName = "languageCode", NullValueHandling = NullValueHandling.Ignore)]
		public string LanguageCode { get; set; }

		[JsonProperty(PropertyName = "parameters", NullValueHandling = NullValueHandling.Ignore)]
		public Dictionary<string, string> Parameters { get; set; }
	}


	public enum Platform
	{
		PLATFORM_UNSPECIFIED,
		FACEBOOK,
		SLACK,
		TELEGRAM,
		KIK,
		SKYPE,
		LINE,
		VIBER,
		ACTIONS_ON_GOOGLE
	}


	public class TextObj
	{
		[JsonProperty(PropertyName = "text", NullValueHandling = NullValueHandling.Ignore)]
		public List<string> Text { get; set; }


		public TextObj()
		{
			Text = new List<string>();
		}


		public TextObj(string text)
		{
			Text = new List<string> { text };
		}


		public TextObj(List<string> textList)
		{
			Text = textList;
		}
	}


	public class Image
	{
		[JsonProperty(PropertyName = "imageUri", NullValueHandling = NullValueHandling.Ignore)]
		public string ImageUri { get; set; }
	}


	public class QuickRepliesObj
	{
		[JsonProperty(PropertyName = "title", NullValueHandling = NullValueHandling.Ignore)]
		public string Title { get; set; }

		[JsonProperty(PropertyName = "quickReplies", NullValueHandling = NullValueHandling.Ignore)]
		public List<string> QuickReplies { get; set; }
	}


	public class Card
	{
		[JsonProperty(PropertyName = "title", NullValueHandling = NullValueHandling.Ignore)]
		public string Title { get; set; }

		[JsonProperty(PropertyName = "subtitle", NullValueHandling = NullValueHandling.Ignore)]
		public string SubTitle { get; set; }

		[JsonProperty(PropertyName = "imageUri", NullValueHandling = NullValueHandling.Ignore)]
		public string ImageUri { get; set; }

		[JsonProperty(PropertyName = "buttons", NullValueHandling = NullValueHandling.Ignore)]
		public List<CardButton> Button { get; set; }
	}


	public class CardButton
	{
		[JsonProperty(PropertyName = "text", NullValueHandling = NullValueHandling.Ignore)]
		public string Text { get; set; }

		[JsonProperty(PropertyName = "postback", NullValueHandling = NullValueHandling.Ignore)]
		public string Postback { get; set; }
	}


	public class SimpleResponsesObj
	{
		[JsonProperty(PropertyName = "simpleResponses", NullValueHandling = NullValueHandling.Ignore)]
		public List<SimpleResponseObj> SimpleResponses { get; set; }


		public SimpleResponsesObj() { }


		public SimpleResponsesObj(SimpleResponseObj simpleResponse)
		{
			SimpleResponses = new List<SimpleResponseObj> { simpleResponse };
		}


		public SimpleResponsesObj(List<SimpleResponseObj> simpleResponses)
		{
			SimpleResponses = simpleResponses;
		}
	}


	public class SimpleResponseObj
	{
		[JsonProperty(PropertyName = "textToSpeech", NullValueHandling = NullValueHandling.Ignore)]
		public string TextToSpeech { get; set; }

		[JsonProperty(PropertyName = "ssml", NullValueHandling = NullValueHandling.Ignore)]
		public string Ssml { get; set; }

		[JsonProperty(PropertyName = "displayText", NullValueHandling = NullValueHandling.Ignore)]
		public string DisplayText { get; set; }
	}


	public class BasicCard
	{
		[JsonProperty(PropertyName = "title", NullValueHandling = NullValueHandling.Ignore)]
		public string Title { get; set; }

		[JsonProperty(PropertyName = "subtitle", NullValueHandling = NullValueHandling.Ignore)]
		public string SubTitle { get; set; }

		[JsonProperty(PropertyName = "formattedText", NullValueHandling = NullValueHandling.Ignore)]
		public string FormattedText { get; set; }

		[JsonProperty(PropertyName = "image", NullValueHandling = NullValueHandling.Ignore)]
		public List<Image> Image { get; set; }

		[JsonProperty(PropertyName = "buttons", NullValueHandling = NullValueHandling.Ignore)]
		public List<BasicCardButton> Buttons { get; set; }
	}


	public class BasicCardButton
	{
		[JsonProperty(PropertyName = "title", NullValueHandling = NullValueHandling.Ignore)]
		public string Title { get; set; }

		[JsonProperty(PropertyName = "openUriAction", NullValueHandling = NullValueHandling.Ignore)]
		public OpenUriAction OpenUriAction { get; set; }
	}


	public class OpenUriAction
	{
		[JsonProperty(PropertyName = "uri", NullValueHandling = NullValueHandling.Ignore)]
		public string Uri { get; set; }
	}


	public class SuggestionsObj
	{
		[JsonProperty(PropertyName = "suggestions", NullValueHandling = NullValueHandling.Ignore)]
		public List<Suggestion> Suggestions { get; set; }

	}


	public class Suggestion
	{
		[JsonProperty(PropertyName = "title", NullValueHandling = NullValueHandling.Ignore)]
		public string Title { get; set; }
	}


	public class LinkOutSuggestion
	{
		[JsonProperty(PropertyName = "destinationName", NullValueHandling = NullValueHandling.Ignore)]
		public string DestinationName { get; set; }

		[JsonProperty(PropertyName = "uri", NullValueHandling = NullValueHandling.Ignore)]
		public string Uri { get; set; }
	}


	public class ListSelect
	{
		[JsonProperty(PropertyName = "title", NullValueHandling = NullValueHandling.Ignore)]
		public string Title { get; set; }

		[JsonProperty(PropertyName = "items", NullValueHandling = NullValueHandling.Ignore)]
		public List<ItemObj> Items { get; set; }
	}


	public class ItemObj
	{
		[JsonProperty(PropertyName = "info", NullValueHandling = NullValueHandling.Ignore)]
		public SelectItemInfo Info { get; set; }

		[JsonProperty(PropertyName = "title", NullValueHandling = NullValueHandling.Ignore)]
		public string Title { get; set; }

		[JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
		public string Description { get; set; }

		[JsonProperty(PropertyName = "image", NullValueHandling = NullValueHandling.Ignore)]
		public Image Image { get; set; }
	}


	public class SelectItemInfo
	{
		[JsonProperty(PropertyName = "key", NullValueHandling = NullValueHandling.Ignore)]
		public string Key { get; set; }

		[JsonProperty(PropertyName = "synonyms", NullValueHandling = NullValueHandling.Ignore)]
		public List<string> Synonyms { get; set; }
	}


	public class CarouselSelect
	{
		[JsonProperty(PropertyName = "items", NullValueHandling = NullValueHandling.Ignore)]
		public List<ItemObj> Items { get; set; }
	}
}
