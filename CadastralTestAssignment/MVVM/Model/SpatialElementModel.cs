using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Perception.Spatial;

namespace CadastralTestAssignment.MVVM.Model
{

    public class SpatialElementModel : BaseModel
    {
        public List<OrdinateModel> Ordinates { get; set; } = new();

        public SpatialElementModel(XElement xElement)
        {
            Deserialize(xElement);
        }

        protected override void Deserialize(XElement spatialElement)
        {
            XElement? oritanes = spatialElement?.Element("ordinates");
            if (oritanes is not null)
            {
                foreach (var ordinate in oritanes.Elements("ordinate"))
                {
                    var ordinateInstance = new OrdinateModel(ordinate);

                    Ordinates.Add(ordinateInstance);
                }
            }

            if (string.IsNullOrWhiteSpace(Indexer))
                SetRandomIndexInsteadOfCadastralNumber();
        }

        public override void SoloSerialize(string path)
        {
            Console.WriteLine("Nothing to serialize solo");
        }

        public override XElement Serialize()
        {
            var spatialElement = new XElement("spatial_element");
            var ordinates = new XElement("ordinates");

            foreach (var ordinate in Ordinates)
            {
                var ordinateInstance = ordinate.Serialize();
                ordinates.Add(ordinateInstance);
            }
            spatialElement.Add(ordinates);

            return spatialElement;
        }
    }
}
