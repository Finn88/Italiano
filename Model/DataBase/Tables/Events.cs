using System;
using System.Xml.Serialization;

namespace Model.Tables
{
    public class Events
    {
        public Events()
        {
            Id = Guid.NewGuid().ToString();
        }

        [XmlElement]
        [DataBase.CoulmnId("Id")]
        public string Id { get; set; }

        [XmlElement]
        [DataBase.CoulmnId("Название")]
        public string EventName { get; set; }

        [XmlElement]
        public string Menu { get; set; }

        [XmlElement]
        public DateTime StartDate { get; set; }

        [XmlIgnore]
        [DataBase.CoulmnId("Дата проведения")]
        public string DateString
        {
            get
            {
                return StartDate.ToString("yyyy/MM/dd");
            }
        }

        [XmlIgnore]
        [DataBase.CoulmnId("Начало")]
        public string StartTimeString
        {
            get
            {
                return StartDate.ToString("HH:mm");
            }
        }

        [XmlElement]
        public DateTime EndDate { get; set; }

        [XmlIgnore]
        [DataBase.CoulmnId("Конец")]
        public string EndTimeString
        {
            get
            {
                return EndDate.ToString("HH:mm");
            }
        }

        [XmlElement]
        [DataBase.CoulmnId("Комментарий")]
        public string Comments { get; set; }

        [XmlElement]
        [DataBase.CoulmnId("Цена")]
        public Decimal Costs { get; set; }

        [XmlElement]
        [DataBase.CoulmnId("Ведущий")]
        public string MasterId { get; set; }

        [XmlElement]
        public DateTime CreateDate { get; set; }

        [XmlElement]
        public DateTime ModifiedDate { get; set; }

        [XmlElement]
        [DataBase.CoulmnId("Активный")]
        public bool IsActive { get; set; }

        [XmlElement]
        public char EventType { get; set; }

    }
}