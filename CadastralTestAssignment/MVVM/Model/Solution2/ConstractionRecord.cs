using System.Xml.Serialization;

public class ConstructionRecord : BaseRecordModel
{
    [XmlElement("object")]
    public ConstructionObject? Object { get; set; }

    [XmlElement("params")]
    public ConstructionParams? Params { get; set; }

    [XmlElement("address_location")]
    public AddressLocation? AddressLocation { get; set; }
}