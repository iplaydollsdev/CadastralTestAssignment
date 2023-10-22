using System.Xml.Serialization;

public class ConstructionParams
{
    [XmlElement("purpose")]
    public string? Purpose { get; set; }
}