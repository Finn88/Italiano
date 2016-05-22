using System;
using System.Xml.Serialization;

namespace Model.Tables
{
    public class Groups
    {
        public Groups()
        {
            Id = Guid.NewGuid().ToString();
        }

        [XmlElement]
        [DataBase.CoulmnId("Id")]
        public string Id { get; set; }

        [XmlElement]
        [DataBase.CoulmnId("Название группы")]
        public string Name { get; set; }

        [XmlElement]
        [DataBase.CoulmnId("Уровень")]
        public string Level { get; set; }

        [XmlElement]
        [DataBase.CoulmnId("Преподаватель")]
        public string TeacherId { get; set; }

        [XmlElement]
        public DateTime StartEducation { get; set; }

        [XmlIgnore]
        [DataBase.CoulmnId("Начало обучения")]
        public string StartEducationString
        {
            get { return StartEducation.ToString("yyyy/MM/dd"); }
        }

        [XmlElement]
        [DataBase.CoulmnId("Комментарий")]
        public string Comments { get; set; }

        [XmlElement]
        public DateTime CreateDate { get; set; }

        [XmlElement]
        public DateTime ModifiedDate { get; set; }

        [XmlElement]
        [DataBase.CoulmnId("Активный")]
        public bool IsActive { get; set; }

        [XmlElement]
        public char GroupType { get; set; }
    }
}