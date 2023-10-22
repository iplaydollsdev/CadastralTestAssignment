using System;
using System.Xml.Serialization;

public class RecordInfo
{
    [XmlElement("registration_date")]
    public string? RegistrationDate { get; set; }
}

