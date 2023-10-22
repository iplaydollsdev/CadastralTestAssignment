using System.Xml.Serialization;

public class BObjectZonesAndTerritories
{
    [XmlElement("b_object")]
    public BObject? BObject { get; set; }

    [XmlElement("type_zone")]
    public Subtype? Subtype { get; set; }
}

