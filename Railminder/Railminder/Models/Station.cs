using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Railminder.Models
{
    [XmlRoot("objStation")]
    public class Station
    {
        [XmlElement("StationDesc")]
        public string Description { get; set; }

        [XmlElement("StationAlias")]
        public string Alias { get; set; }

        [XmlElement("StationLatitude")]
        public double Latitude { get; set; }

        [XmlElement("StationLongitude")]
        public double Longitude { get; set; }

        [XmlElement("StationCode")]
        public string Code { get; set; }

        [XmlElement("StationId")]
        public string Id { get; set; }
    }
}
