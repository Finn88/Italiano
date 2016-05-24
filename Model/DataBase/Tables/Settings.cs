using System;
using System.Xml.Serialization;

namespace Model.Tables
{
    public class Settings
    {
        [XmlElement]
        public string Code { get; set; }

        [XmlElement]
        public string Value { get; set; }

        [XmlElement]
        public string Description { get; set; }

        [XmlElement]
        public DateTime CreateDate { get; set; }

        [XmlElement]
        public DateTime ModifiedDate { get; set; }

    }
}