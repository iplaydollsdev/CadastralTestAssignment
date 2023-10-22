using System.Xml.Serialization;

public class District
{
    [XmlElement("type_district")]
    public string? TypeDistrict { get; set; }

    [XmlElement("name_district")]
    public string? NameDistrict { get; set; }
}

