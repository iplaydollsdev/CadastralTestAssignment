using System.Xml.Serialization;

public class AreaQuarter
{
    [XmlElement("area")]
    public string? Area { get; set; }

    [XmlElement("unit")]
    public string? Unit { get; set; }
}
