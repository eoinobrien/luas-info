using LuasTimes.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace LuasTimes.Dialogflow
{
	public class DialogFlowResponse
	{
		[JsonProperty(PropertyName = "fulfillmentText")]
		public string FulfillmentText { get; private set; }

		[JsonProperty(PropertyName = "fulfillmentMessages")]
		public List<Message> FulfillmentMessages { get; private set; }

		[JsonProperty(PropertyName = "source")]
		public string Source { get; private set; }

		[JsonProperty(PropertyName = "payload")]
		public Dictionary<string, string> Payload { get; private set; }

		[JsonProperty(PropertyName = "outputContexts")]
		public List<Context> OutputContexts { get; private set; }

		[JsonProperty(PropertyName = "followupEventInput")]
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
		[JsonProperty(PropertyName = "platform"), JsonConverter(typeof(StringEnumConverter))]
		public Platform Platform { get; set; }

		[JsonProperty(PropertyName = "text")]
		public TextObj Text { get; set; }

		[JsonProperty(PropertyName = "image")]
		public Image Image { get; set; }

		[JsonProperty(PropertyName = "quickReplies")]
		public QuickRepliesObj QuickReplies { get; set; }

		[JsonProperty(PropertyName = "card")]
		public Card Card { get; set; }

		[JsonProperty(PropertyName = "payload")]
		public Dictionary<string, string> Payload { get; set; }

		[JsonProperty(PropertyName = "simpleResponses")]
		public SimpleResponsesObj SimpleResponses { get; set; }

		[JsonProperty(PropertyName = "basicCard")]
		public BasicCard BasicCard { get; set; }

		[JsonProperty(PropertyName = "suggestions")]
		public SuggestionsObj Suggestions { get; set; }

		[JsonProperty(PropertyName = "linkOutSuggestion")]
		public LinkOutSuggestion LinkOutSuggestion { get; set; }

		[JsonProperty(PropertyName = "listSelect")]
		public ListSelect ListSelect { get; set; }

		[JsonProperty(PropertyName = "carouselSelect")]
		public CarouselSelect CarouselSelect { get; set; }

		public Message(Platform platform)
		{
			Platform = platform;
		}
	}


	public class Context
	{
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		[JsonProperty(PropertyName = "lifespanCount")]
		public int LifespanCount { get; set; }

		[JsonProperty(PropertyName = "parameters")]
		public Dictionary<string, string> Parameters { get; set; }
	}


	public class EventInput
	{
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		[JsonProperty(PropertyName = "languageCode")]
		public string LanguageCode { get; set; }

		[JsonProperty(PropertyName = "parameters")]
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
		[JsonProperty(PropertyName = "text")]
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
		[JsonProperty(PropertyName = "imageUri")]
		public string ImageUri { get; set; }
	}


	public class QuickRepliesObj
	{
		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }

		[JsonProperty(PropertyName = "quickReplies")]
		public List<string> QuickReplies { get; set; }
	}


	public class Card
	{
		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }

		[JsonProperty(PropertyName = "subtitle")]
		public string SubTitle { get; set; }

		[JsonProperty(PropertyName = "imageUri")]
		public string ImageUri { get; set; }

		[JsonProperty(PropertyName = "buttons")]
		public List<CardButton> Button { get; set; }
	}


	public class CardButton
	{
		[JsonProperty(PropertyName = "text")]
		public string Text { get; set; }

		[JsonProperty(PropertyName = "postback")]
		public string Postback { get; set; }
	}


	public class SimpleResponsesObj
	{
		[JsonProperty(PropertyName = "simpleResponses")]
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
		[JsonProperty(PropertyName = "textToSpeech")]
		public string TextToSpeech { get; set; }

		[JsonProperty(PropertyName = "ssml")]
		public string Ssml { get; set; }

		[JsonProperty(PropertyName = "displayText")]
		public string DisplayText { get; set; }
	}


	public class BasicCard
	{
		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }

		[JsonProperty(PropertyName = "subtitle")]
		public string SubTitle { get; set; }

		[JsonProperty(PropertyName = "formattedText")]
		public string FormattedText { get; set; }

		[JsonProperty(PropertyName = "image")]
		public List<Image> Image { get; set; }

		[JsonProperty(PropertyName = "buttons")]
		public List<BasicCardButton> Buttons { get; set; }
	}


	public class BasicCardButton
	{
		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }

		[JsonProperty(PropertyName = "openUriAction")]
		public OpenUriAction OpenUriAction { get; set; }
	}


	public class OpenUriAction
	{
		[JsonProperty(PropertyName = "uri")]
		public string Uri { get; set; }
	}


	public class SuggestionsObj
	{
		[JsonProperty(PropertyName = "suggestions")]
		public List<Suggestion> Suggestions { get; set; }

	}


	public class Suggestion
	{
		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }
	}


	public class LinkOutSuggestion
	{
		[JsonProperty(PropertyName = "destinationName")]
		public string DestinationName { get; set; }

		[JsonProperty(PropertyName = "uri")]
		public string Uri { get; set; }
	}


	public class ListSelect
	{
		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }

		[JsonProperty(PropertyName = "items")]
		public List<ItemObj> Items { get; set; }
	}


	public class ItemObj
	{
		[JsonProperty(PropertyName = "info")]
		public SelectItemInfo Info { get; set; }

		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }

		[JsonProperty(PropertyName = "description")]
		public string Description { get; set; }

		[JsonProperty(PropertyName = "image")]
		public Image Image { get; set; }
	}


	public class SelectItemInfo
	{
		[JsonProperty(PropertyName = "key")]
		public string Key { get; set; }

		[JsonProperty(PropertyName = "synonyms")]
		public List<string> Synonyms { get; set; }
	}


	public class CarouselSelect
	{
		[JsonProperty(PropertyName = "items")]
		public List<ItemObj> Items { get; set; }
	}
}
