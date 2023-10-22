using System.Xml.Serialization;

public class PermittedUseEstablished
{
    [XmlElement("by_document")]
    public string? ByDocument { get; set; }

    [XmlElement("land_use")]
    public LandUse? landUse { get; set; }
}
