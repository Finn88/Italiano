using System;
using System.Xml.Serialization;

namespace Model.Tables
{
    public class GroupsDays
    {
        public GroupsDays()
        {
            Id = Guid.NewGuid().ToString();
        }

        [XmlElement]
        public string Id { get; set; }

        [XmlElement]
        public string GroupId { get; set; }

        [XmlElement]
        public int Day { get; set; }

        [XmlElement]
        public DateTime StartTime { get; set; }

        [XmlElement]
        public DateTime EndTime { get; set; }

        [XmlIgnore]
        public string StartTimeString
        {
            get
            {
                return StartTime.ToString("HH:mm");
            }
        }

        [XmlIgnore]
        public string EndTimeString
        {
            get
            {
                return EndTime.ToString("HH:mm");
            }
        }
    }
}