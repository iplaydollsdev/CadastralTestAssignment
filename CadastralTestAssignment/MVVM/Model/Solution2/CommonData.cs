using System.Xml.Serialization;

public class CommonData
{
    [XmlElement("type")]
    public Subtype? Type { get; set; }

    [XmlElement("cad_number")]
    public string? CadNumber { get; set; }
}
