using System.Xml.Serialization;

public class CommonLand
{
    [XmlElement("common_land_cad_number")]
    public CadNumber? CadNumber { get; set; }
}
