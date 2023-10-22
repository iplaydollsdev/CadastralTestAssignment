using System.Collections.Generic;
using System.Xml.Serialization;

public class ContoursLocation
{
    [XmlArray("contours")]
    [XmlArrayItem("contour")]
    public List<Contour>? Contours { get; set; }
}
