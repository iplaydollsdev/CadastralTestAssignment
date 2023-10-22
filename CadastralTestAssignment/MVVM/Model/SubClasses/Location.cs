using System.Xml.Serialization;

public class Location
{
    [XmlElement("okato")]
    public string? Okato { get; set; }

    [XmlElement("region")]
    public Region? Region { get; set; }
}