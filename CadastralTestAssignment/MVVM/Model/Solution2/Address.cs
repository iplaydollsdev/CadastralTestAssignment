using System.Xml.Serialization;

public class Address
{
    [XmlElement("address_fias")]
    public AddressFias? AddressFias { get; set; }

    [XmlElement("readable_address")]
    public string? ReadableAddress { get; set; }
}
