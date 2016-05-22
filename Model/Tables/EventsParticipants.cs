using System;
using System.Xml.Serialization;

namespace Model.Tables
{
    public class EventsParticipants
    {
        public EventsParticipants()
        {
            Id = Guid.NewGuid().ToString(); 
        }

        [XmlElement]
        public string Id { get; set; }

        [XmlElement]
        public string EventId { get; set; }

        [XmlElement]
        public string ParticipantsId { get; set; }
    }
}