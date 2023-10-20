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
    public struct Ordinate
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class SpatialDataModel : BaseModel
    {
        public string? SkId { get; set; }
        public List<Ordinate> Ordinates { get; private set; } = new();
        public List<string> StringOrdinates { get; private set; } = new();

        public SpatialDataModel(XElement spatialData) 
        {
            Deserialize(spatialData);
        }

        public override XElement Serialize()
        {
            throw new NotImplementedException();
        }

        public override void SoloSerialize()
        {
            DateTime dateTime = DateTime.Now;

            XDocument xDoc = new XDocument();

            XElement spatialData = new XElement("SpatialData");
            XAttribute cadastralNumber = new XAttribute("CadastralNumber", CadastralNumber ?? "01:01:0000001:1");
            XAttribute dataCreated = new XAttribute("DateCreated", dateTime.ToString("yyyy'-'MM'-'dd"));



            XElement skId = new XElement("sk_id", SkId);
            XElement spatialsElements = new XElement("spatials_elements");



            XElement spatialElement = new XElement("spatial_element");
            XElement ordinates = new XElement("ordinates");

            foreach (var o in Ordinates)
            {
                var ordinate = new XElement("ordinate",
                                    new XElement("x", o.X),
                                    new XElement("y", o.Y));
                ordinates!.Add(ordinate);
            }

            spatialElement.Add(ordinates);
            spatialsElements.Add(spatialElement);

            spatialData.Add(skId);
            spatialData.Add(spatialsElements);

            spatialData.Add(cadastralNumber);
            spatialData.Add(dataCreated);

            xDoc.Add(spatialData);

            string savePath = Path.Combine("D:", $"SpatialData_{dateTime.ToString("yy'-'MM'-'dd'_'HH'-'mm")}.xml");
            xDoc.Save(savePath);
            MessageBox.Show($"File saved at: {savePath}");

        }

        protected override void Deserialize(XElement spatialEntity)
        {
            XElement? skId = spatialEntity.Element("sk_id");
            SkId = skId?.Value ?? string.Empty;

            XElement? orinates = spatialEntity?.Element("spatials_elements")?.Element("spatial_element")?.Element("ordinates");
            if (orinates is not null) 
            {
                foreach (var ordinate in orinates.Elements("ordinate"))
                {
                    Double.TryParse((string?)(ordinate?.Element("x")), CultureInfo.InvariantCulture, out double _x);
                    Double.TryParse((string?)(ordinate?.Element("y")), CultureInfo.InvariantCulture, out double _y);
                    var ordinateValue = new Ordinate
                    {
                        X = _x,
                        Y = _y
                    };
                    Ordinates.Add(ordinateValue);
                    StringOrdinates.Add(GetOrdinate(ordinateValue));
                }
            }

            if (string.IsNullOrWhiteSpace(CadastralNumber))
                SetRandomCadastralNumber();
        }

        public string GetOrdinate(Ordinate ordinate)
        {
            return $"X: {ordinate.X}, Y: {ordinate.Y}";
        }
    }
}
