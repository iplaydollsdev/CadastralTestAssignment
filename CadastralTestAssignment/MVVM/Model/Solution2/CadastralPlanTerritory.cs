using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;

[XmlRoot("extract_cadastral_plan_territory")]
public class CadastralPlanTerritory
{
    [XmlElement("details_statement")]
    public DetailsStatement? DetailsStatement { get; set; }

    [XmlElement("details_request")]
    public DetailsRequest? DetailsRequest { get; set; }

    [XmlArray("cadastral_blocks")]
    [XmlArrayItem("cadastral_block")]
    public List<CadastralBlock>? CadastralBlocks { get; set; }
}
