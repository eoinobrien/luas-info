using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LuasTimes
{
	public class Forcast
	{
		public Station Station { get; private set; }

		public StopInfo Info { get; private set; }


		public Forcast(StopInfo stopInfo)
		{
			Station = Station.GetFromAbbreviation(stopInfo.StopAbv);

			Info = stopInfo;
		}
	}


	[XmlRoot(ElementName = "stopInfo")]
	public class StopInfo
	{
		[XmlElement(ElementName = "message")]
		public string Message { get; set; }

		public bool ServicesOperatingNormally => Message.ToLowerInvariant().Contains("line services operating normally");

		[XmlElement(ElementName = "direction")]
		public List<ForcastDirection> Directions { get; set; }

		[XmlAttribute(AttributeName = "created")]
		public string Created { get; set; }

		[XmlAttribute(AttributeName = "stop")]
		public string Stop { get; set; }

		[XmlAttribute(AttributeName = "stopAbv")]
		public string StopAbv { get; set; }
	}


	[XmlRoot(ElementName = "direction")]
	public class ForcastDirection
	{
		[XmlElement(ElementName = "tram")]
		public List<Tram> Trams { get; set; }

		[XmlAttribute(AttributeName = "name")]
		public string DirectionName { get; set; }

		public Direction Direction => DirectionName.ParseDirection();
	}


	[XmlRoot(ElementName = "tram")]
	public class Tram
	{
		[XmlAttribute(AttributeName = "dueMins")]
		public string DueMins { get; set; }

		[XmlAttribute(AttributeName = "destination")]
		public string DestinationName { get; set; }

		public Station Destination => Station.GetFromName(DestinationName.ToLowerInvariant());

		public bool IsDue => DueMins == "DUE";

		public bool NoTramsForcast => DestinationName == "No trams forecast";


		public int Minutes {
			get {
				if (IsDue)
					return 0;

				return int.TryParse(DueMins, out int mins) ? mins : -1;
			}
		}


		public bool TramGoesToDestination(Station UserDestination, Direction direction)
		{
			if (NoTramsForcast  || Destination.Line != UserDestination.Line)
			{
				return false;
			}

			if (direction == Direction.Inbound)
			{
				return Destination.StationOrder <= UserDestination.StationOrder;
			}
			else
			{
				return Destination.StationOrder >= UserDestination.StationOrder;
			}
		}
	}
}
