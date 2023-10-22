using System.Collections.Generic;
using System.Xml.Serialization;

public class SpatialElement
{
    [XmlArray("ordinates")]
    [XmlArrayItem("ordinate")]
    public List<Ordinate>? Ordinates { get; set; }
}
