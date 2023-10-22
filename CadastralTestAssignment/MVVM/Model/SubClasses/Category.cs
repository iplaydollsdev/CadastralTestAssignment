using System.Xml.Serialization;

public class Category
{
    [XmlElement("type")]
    public Subtype? Type { get; set; }
}

