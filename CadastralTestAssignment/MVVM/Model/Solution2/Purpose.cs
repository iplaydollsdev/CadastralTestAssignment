using System.Xml.Serialization;

public class Purpose
{
    [XmlElement("code")]
    public string? Code { get; set; }

    [XmlElement("value")]
    public string? Value { get; set; }
}