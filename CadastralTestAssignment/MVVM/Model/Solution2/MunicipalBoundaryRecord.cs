using System.Xml.Serialization;

public class MunicipalBoundaryRecord : BaseRecordModel
{
    [XmlElement("record_info")]
    public RecordInfo? RecordInfo { get; set; }

    [XmlElement("b_object_municipal_boundary")]
    public BObjectMunicipalBoundary? BObjectMunicipalBoundary { get; set; }

    [XmlElement("b_contours_location")]
    public ContoursLocation? BContoursLocation { get; set; }
}

