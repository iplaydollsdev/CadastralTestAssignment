using System.Xml.Serialization;

public class Params
{

    [XmlElement("category")]
    public Category? Category { get; set; }

    [XmlElement("permitted_use")]
    public PermittedUse? PermittedUse { get; set; }

    [XmlElement("area")]
    public Area? Area { get; set; }
}
