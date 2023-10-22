using System.Xml.Serialization;

public class Locality
{
    [XmlElement("type_locality")]
    public string? TypeLocality { get; set; }

    [XmlElement("name_locality")]
    public string? NameLocality { get; set; }
}

