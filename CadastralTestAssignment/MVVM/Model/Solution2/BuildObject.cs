using System.Xml.Serialization;

public class BuildObject
{
    [XmlElement("common_data")]
    public CommonData? CommonData { get; set; }
}