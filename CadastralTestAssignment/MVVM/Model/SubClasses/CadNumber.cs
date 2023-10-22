using System.Xml.Serialization;

public class CadNumber
{
    [XmlElement("cad_number")]
    public string? Value { get; set; }
}
