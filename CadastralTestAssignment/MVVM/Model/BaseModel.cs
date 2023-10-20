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
        public string? CadastralNumber { get; set; }

        protected abstract void Deserialize(XElement xElement);

        public abstract void SoloSerialize();
        public abstract XElement Serialize();
        protected void SetRandomCadastralNumber()
        {
            Random random = new Random();
            CadastralNumber = $"{random.Next(0, 99)}:{random.Next(0, 999)}:{random.Next(0, 99999)}:{random.Next(0, 99)}";
        }
    }
}
