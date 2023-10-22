using System.Xml.Serialization;

public class AddressLocation
{
    [XmlElement("address")]
    public Address? Address { get; set; }

    [XmlElement("rel_position")]
    public RelPosition? RelPosition { get; set; }
}
