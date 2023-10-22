using System.Xml.Serialization;

public class LevelSettlement
{
    [XmlElement("okato")]
    public string? Okato { get; set; }

    [XmlElement("kladr")]
    public string? Kladr { get; set; }

    [XmlElement("region")]
    public Region? Region { get; set; }

    [XmlElement("district")]
    public District? District { get; set; }

    [XmlElement("locality")]
    public Locality? Locality { get; set; }
}
