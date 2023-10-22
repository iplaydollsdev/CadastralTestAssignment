using System.Xml.Serialization;

public class Area
{
    [XmlElement("value")]
    public string? Value { get; set; }

    [XmlElement("inaccuracy")]
    public double? Inaccuracy { get; set; }
}
