using System.Xml.Serialization;

public class DetailsStatement
{
    [XmlElement("group_top_requisites")]
    public GroupTopRequisites? GroupTopRequisites { get; set; }
}
