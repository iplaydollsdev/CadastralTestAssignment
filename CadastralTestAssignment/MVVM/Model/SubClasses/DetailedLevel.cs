using System.Xml.Serialization;

public  class DetailedLevel
{
    [XmlElement("other")]
    public string? Other { get; set; }
}

