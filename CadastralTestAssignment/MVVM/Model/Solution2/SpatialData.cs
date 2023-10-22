using System.Xml.Serialization;

public class SpatialData : BaseRecordModel
{
    [XmlElement("entity_spatial")]
    public EntitySpatial? EntitySpatial { get; set; }
}

