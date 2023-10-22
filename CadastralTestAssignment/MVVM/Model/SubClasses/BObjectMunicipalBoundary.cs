using System.Xml.Serialization;

public class BObjectMunicipalBoundary
{
    [XmlElement("b_object")]
    public BObject? BObject { get; set; }
}

