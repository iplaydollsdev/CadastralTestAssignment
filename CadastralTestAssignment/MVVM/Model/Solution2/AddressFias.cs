using System.Xml.Serialization;

public class AddressFias
{
    [XmlElement("level_settlement")]
    public LevelSettlement? LevelSettlement { get; set; }

    [XmlElement("detailed_level")]
    public DetailedLevel? DetailedLevel { get; set; }
}
