using System;
using System.Xml.Serialization;

namespace Model.Tables
{
    public class Budget
    {
        public Budget()
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
        [DataBase.CoulmnId("Сумма")]
        public Decimal Costs { get; set; }

        [XmlElement]
        public bool IsEarning { get; set; }

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