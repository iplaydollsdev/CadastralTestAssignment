using System.Xml.Serialization;

public class GroupTopRequisites
{
    [XmlElement("organ_registr_rights")]
    public string? OrganRegistrRights { get; set; }

    [XmlElement("date_formation")]
    public string? DateFormation { get; set; }

    [XmlElement("registration_number")]
    public string? RegistrationNumber { get; set; }
}
