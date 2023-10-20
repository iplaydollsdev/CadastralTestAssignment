using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CadastralTestAssignment.MVVM.Model
{
    public abstract class BaseModel
    {
        public string? Indexer { get; set; }

        protected abstract void Deserialize(XElement xElement);

        public abstract void SoloSerialize();
        public abstract XElement Serialize();
        protected void SetRandomIndexInsteadOfCadastralNumber()
        {
            Random random = new Random();
            Indexer = $"Indexer: {random.Next(0, 1000)}";
        }
    }
}
