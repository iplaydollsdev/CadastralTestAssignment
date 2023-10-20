using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using Windows.Perception.Spatial;

namespace CadastralTestAssignment.MVVM.Model
{
    public class SpatialDataModel : BaseModel
    {
        public string? SkId { get; set; }
        public List<SpatialElementModel> SpatialElements { get; private set; } = new();

        public SpatialDataModel(XElement spatialData) 
        {
            Deserialize(spatialData);
        }

        public override XElement Serialize()
        {
            DateTime dateTime = DateTime.Now;

            var spatialData = new XElement("SpatialData");
            XAttribute cadastralNumber = new XAttribute("CadastralNumber", Indexer ?? string.Empty);
            XAttribute dataCreated = new XAttribute("DateCreated", dateTime.ToString("yyyy'-'MM'-'dd"));



            XElement skId = new XElement("sk_id", SkId);
            XElement spatialsElements = new XElement("spatials_elements");

            foreach (var element in SpatialElements)
            {
                var statialElement = element.Serialize();
                spatialsElements.Add(statialElement);
            }

            spatialData.Add(skId);
            spatialData.Add(spatialsElements);

            spatialData.Add(cadastralNumber);
            spatialData.Add(dataCreated);

            return spatialData;
        }

        public override void SoloSerialize()
        {
            DateTime dateTime = DateTime.Now;

            XDocument xDoc = new XDocument();

            XElement spatialData = Serialize();

            xDoc.Add(spatialData);

            string savePath = Path.Combine("D:", $"SpatialData_{dateTime.ToString("yy'-'MM'-'dd'_'HH'-'mm")}.xml");
            xDoc.Save(savePath);
            MessageBox.Show($"File saved at: {savePath}");

        }

        protected override void Deserialize(XElement spatialEntity)
        {
            XElement? skId = spatialEntity.Element("sk_id");
            SkId = skId?.Value ?? string.Empty;

            XElement? spatialElements = spatialEntity?.Element("spatials_elements");
            if (spatialElements is not null)
            {
                foreach (var spatialElement in spatialElements.Elements("spatial_element"))
                {
                    var spatial = new SpatialElementModel(spatialElement);

                    SpatialElements.Add(spatial);
                }
            }

            if (string.IsNullOrWhiteSpace(Indexer))
                SetRandomIndexInsteadOfCadastralNumber();
        }
    }
}
