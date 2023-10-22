using System.Collections.Generic;
using System.Xml.Serialization;

public class CadastralBlocks
{
    [XmlArray("cadastral_blocks")]
    [XmlArrayItem("cadastral_block")]
    public List<CadastralBlock>? CadastralBlock { get; set; }
}
