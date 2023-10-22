using System.Xml.Serialization;

public class BuildParams
{
    [XmlElement("area")]
    public double? Area { get; set; }

    [XmlElement("purpose")]
    public Purpose? Purpose { get; set; }
}