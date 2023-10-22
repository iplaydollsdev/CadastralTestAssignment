using System.Xml.Serialization;

public class ZonesAndTerritoriesRecord : BaseRecordModel
{
    [XmlElement("record_info")]
    public RecordInfo? RecordInfo { get; set; }

    [XmlElement("b_object_zones_and_territories")]
    public BObjectZonesAndTerritories? BObjectZonesAndTerritories { get; set; }

    [XmlElement("b_contours_location")]
    public ContoursLocation? ContoursLocation { get; set; }
}

