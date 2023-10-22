using System.Xml.Serialization;

public class Subtype
{
    [XmlElement("code")]
    public string? Code { get; set; }

    [XmlElement("value")]
    public string? Value { get; set; }
}
