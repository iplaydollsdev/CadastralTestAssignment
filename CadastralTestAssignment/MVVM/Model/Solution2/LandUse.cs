using System.Xml.Serialization;

public class LandUse
{
    [XmlElement("value")]
    public string? Value { get; set; }
}

