using System.Collections.Generic;
using System.Xml.Serialization;

public class EntitySpatial
{
    [XmlElement("sk_id")]
    public string? SkId { get; set; }

    [XmlArray("spatials_elements")]
    [XmlArrayItem("spatial_element")]
    public List<SpatialElement>? SpatialsElements { get; set; }
}
