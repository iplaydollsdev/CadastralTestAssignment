using System.Collections.Generic;
using System.Xml.Serialization;

public class CadastralBlock
{
    [XmlElement("cadastral_number")]
    public string? CadastralNumber { get; set; }

    [XmlElement("area_quarter")]
    public AreaQuarter? AreaQuarter { get; set; }

    [XmlElement("record_data")]
    public RecordData? RecordData { get; set; }

    [XmlElement("spatial_data")]
    public SpatialData? SpatialData { get; set; }

    [XmlArray("municipal_boundaries")]
    [XmlArrayItem("municipal_boundary_record")]
    public List<MunicipalBoundaryRecord>? MunicipalBoundaryRecords { get; set; }

    [XmlArray("zones_and_territories_boundaries")]
    [XmlArrayItem("zones_and_territories_record")]
    public List<ZonesAndTerritoriesRecord>? ZonesAndTerritoriesRecords { get; set; }
}
