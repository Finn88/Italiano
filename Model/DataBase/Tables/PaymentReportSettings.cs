using System.Xml.Serialization;

namespace Model.Tables
{
    public class PaymentReportSettings
    {
        [XmlElement]
        public char Code { get; set; }

        [XmlElement]
        public string Name { get; set; }

        [XmlElement]
        public bool ShouldBeCount { get; set; }
    }
}