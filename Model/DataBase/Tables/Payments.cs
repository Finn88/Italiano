using System;
using System.Xml.Serialization;

namespace Model.Tables
{
    public class Payments
    {
        public Payments()
        {
            Id = Guid.NewGuid().ToString();
        }

        [XmlElement]
        [DataBase.CoulmnId("Id")]
        public string Id { get; set; }

        [XmlElement]
        [DataBase.CoulmnId("Id плательщика")]
        public string PersonId { get; set; }

        [XmlElement]
        [DataBase.CoulmnId("Оплата за")]
        public char PaymentType { get; set; }// PaymentsTypes class values

        [XmlElement]
        [DataBase.CoulmnId("Сумма")]
        public Decimal Costs { get; set; }

        [XmlElement]
        public DateTime DateFrom { get; set; }

        [XmlIgnore]
        [DataBase.CoulmnId("Период от")]
        public string DateFromString {
            get
            {
                return DateFrom.ToString("yyyy/MM/dd");
            }
        }

        [XmlElement]
        public DateTime DateTo { get; set; }
        
        [XmlIgnore]
        [DataBase.CoulmnId("Период до")]
        public string DateToString
        {
            get
            {
                return DateTo.ToString("yyyy/MM/dd");
            }
        }

        [XmlElement]
        public DateTime PaymentDate { get; set; }

        [XmlIgnore]
        [DataBase.CoulmnId("Дата оплаты")]
        public string PaymentDateString
        {
            get
            {
                return PaymentDate.ToString("yyyy/MM/dd");
            }
        }

        [XmlElement]
        [DataBase.CoulmnId("Комментарий")]
        public string Comments { get; set; }

        [XmlElement]
        public DateTime CreateDate { get; set; }

        [XmlElement]
        public DateTime ModifiedDate { get; set; }
    }
}