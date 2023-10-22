using System.Xml.Serialization;

public class BObject
{
    [XmlElement("reg_numb_border")]
    public string? RegNumberBorder {  get; set; }

    [XmlElement("type_boundary")]
    public Subtype? TypeBoundary { get; set; }
}

