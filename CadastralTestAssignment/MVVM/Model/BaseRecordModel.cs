using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

[Serializable]
public abstract class BaseRecordModel
{
    [XmlIgnore]
    public string Index { get; set; } = string.Empty;
    [XmlIgnore]
    public string ModelName { get; set; } = string.Empty;
    [XmlIgnore]
    public bool IsSelected { get; set; } = false;
}

