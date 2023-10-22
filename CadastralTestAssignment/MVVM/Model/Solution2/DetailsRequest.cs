using System.Xml.Serialization;

public class DetailsRequest
{
    [XmlElement("date_received_request")]
    public string? DateReceivedRequest { get; set; }

    [XmlElement("date_receipt_request_reg_authority_rights")]
    public string? DateReceiptRequestRegAuthorityRights { get; set; }
}
