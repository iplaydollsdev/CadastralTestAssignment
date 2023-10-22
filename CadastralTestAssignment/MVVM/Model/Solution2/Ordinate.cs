using System.Text;
using System.Xml.Serialization;

public class Ordinate
{
    [XmlElement("x")]
    public double? X { get; set; }

    [XmlElement("y")]
    public double? Y { get; set; }

    [XmlElement("ord_nmb")]
    public int? OrdNumber { get; set; }

    [XmlElement("num_geopoint")]
    public int? GeopointNumber { get; set; }

    [XmlElement("delta_geopoint")]
    public double? DeltaGeopoint { get; set; }

    public string GetString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("X: ");
        sb.Append(X);
        sb.Append(" Y: ");
        sb.Append(Y);
        sb.Append(" OrdNum: ");
        sb.Append(OrdNumber);
        sb.Append(" GeoNum ");
        sb.Append(GeopointNumber);
        
        return sb.ToString();
    }
}