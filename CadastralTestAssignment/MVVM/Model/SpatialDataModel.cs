using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            throw new NotImplementedException();
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
