using System.Xml.Serialization;

public class ConstructionObject
{
    [XmlElement("common_data")]
    public CommonData? CommonData { get; set; }
}