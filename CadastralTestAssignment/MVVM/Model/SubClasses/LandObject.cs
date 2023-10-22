using System.Xml.Serialization;

public class LandObject
{
    [XmlElement("common_data")]
    public CommonData? CommonData { get; set; }

    [XmlElement("subtype")]
    public Subtype? Subtype { get; set; }

}
