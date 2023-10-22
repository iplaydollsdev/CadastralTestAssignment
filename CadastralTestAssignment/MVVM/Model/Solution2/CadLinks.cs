using System.Xml.Serialization;

public class CadLinks
{
    [XmlElement("common_land")]
    public CommonLand? CommonLand { get; set; }
}
