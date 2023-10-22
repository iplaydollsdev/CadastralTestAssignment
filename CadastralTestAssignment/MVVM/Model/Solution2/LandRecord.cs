using System.Xml.Serialization;

public class LandRecord : BaseRecordModel
{
    [XmlElement("object")]
    public LandObject? Object { get; set; }

    [XmlElement("params")]
    public Params? Params { get; set; }

    [XmlElement("address_location")]
    public AddressLocation? AddressLoc { get; set; }

    [XmlElement("cost")]
    public Cost? Cost { get; set; }
}
