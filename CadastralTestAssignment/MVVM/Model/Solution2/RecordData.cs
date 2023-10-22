using System.Xml.Serialization;

public class RecordData
{
    [XmlElement("base_data")]
    public BaseData? BaseData { get; set; }
}
