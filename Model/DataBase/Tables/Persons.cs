using System;
using System.Xml.Serialization;

namespace Model.Tables
{
    public class Persons
    {
        public Persons()
        {
            Id = Guid.NewGuid().ToString();
        }

        [XmlElement]
        [DataBase.CoulmnId("Id")]
        public string Id { get; set; }

        [XmlElement]
        [DataBase.CoulmnId("Имя")]
        public string FirstName { get; set; }

        [XmlElement]
        [DataBase.CoulmnId("Фамилия")]
        public string LastName { get; set; }

        [XmlElement]
        [DataBase.CoulmnId("Отчество")]
        public string MiddleName { get; set; }

        [XmlElement]
        public char? Sex { get; set; }

        [XmlIgnore]
        [DataBase.CoulmnId("Пол")]
        public string SexString
        {
            get
            {
                if (Sex != null)
                    return Sex == 'M' ? "Муж." : "Жен.";
                return "Муж.";
            }
        }

        [XmlElement]
        [DataBase.CoulmnId("Телефон")]
        public string Phone { get; set; }

        [XmlElement]
        [DataBase.CoulmnId("Email")]
        public string Email { get; set; }

        [XmlElement]
        public DateTime BirthDate { get; set; }

        [XmlIgnore]
        [DataBase.CoulmnId("Дата рождения")]
        public string BirthDateString
        {
            get { return BirthDate.ToString("yyyy/MM/dd"); }
        }

        [XmlElement]
        public DateTime SecondaryDate { get; set; }

        [XmlIgnore]
        public string SecondaryDateString
        {
            get { return SecondaryDate.ToString("yyyy/MM/dd"); }
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
        public string GroupId { get; set; }

        [XmlElement]
        public char PersonType { get; set; }

        [XmlIgnore]
        public string FullName
        {
            get { return LastName + " " + FirstName; }
        }
    }
}