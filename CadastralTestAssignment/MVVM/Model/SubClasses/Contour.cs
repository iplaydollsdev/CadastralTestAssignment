using System.Xml.Serialization;

public class Contour
{
    [XmlElement("entity_spatial")]
    public EntitySpatial? EntitySpatial { get; set; }
}
