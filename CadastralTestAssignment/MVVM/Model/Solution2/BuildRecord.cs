using System.Xml.Serialization;

public class BuildRecord : BaseRecordModel
{
    [XmlElement("object")]
    public BuildObject? Object { get; set; }

    [XmlElement("params")]
    public BuildParams? Params { get; set; }

    [XmlElement("address_location")]
    public AddressLocation? AddressLocation { get; set; }

    [XmlElement("cost")]
    public Cost? Cost { get; set; }
}