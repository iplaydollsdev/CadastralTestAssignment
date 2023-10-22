using System.Xml.Serialization;

public class RelPosition
{
    [XmlElement("in_boundaries_mark")]
    public string? InBoundariesMark { get; set; }
}
