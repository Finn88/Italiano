using System.Xml.Serialization;

namespace Model.Tables
{
    public class GridsSettings
    {
        [XmlElement]
        public int GridId { get; set; }

        [XmlElement]
        public string ColumnName { get; set; }

        [XmlElement]
        public string ColumnHeader { get; set; }

        [XmlElement]
        public int Width { get; set; }

        [XmlElement]
        public int OrderNr { get; set; }

        [XmlElement]
        public bool IsVisible { get; set; }
    }
}