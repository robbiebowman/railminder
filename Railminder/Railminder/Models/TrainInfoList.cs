using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Railminder.Models
{
    [XmlRoot("ArrayOfObjStation")]
    public class TrainInfoList
    {
        [XmlElement("objStationData")]
        public List<TrainInfo> InfoList { get; set; }
    }
}
