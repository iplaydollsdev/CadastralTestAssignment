using System.Xml.Serialization;

public class Cost
{
    [XmlElement("value")]
    public double? Value { get; set; }
}
