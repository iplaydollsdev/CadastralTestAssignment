using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[Serializable]
public abstract class BaseRecordModel
{
    public string Index { get; set; } = string.Empty;
    public string ModelName { get; set; } = string.Empty;
    public bool IsSelected { get; set; } = false;
}

