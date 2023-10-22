using System.Xml.Serialization;

public class PermittedUse
{
    [XmlElement("permitted_use_established")]
    public PermittedUseEstablished? PermittedUseEstablished { get; set; }
}

