using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Perception.Spatial;

namespace CadastralTestAssignment.MVVM.Model
{
    public struct BoundaryOrdinate
    {
        public int OrdNum { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class BoundaryModel : BaseModel
    {
        public string? RegistrationDate { get; set; }
        public string? TypeBoundaryCode { get; set; }
        public string? TypeBoundaryValue { get; set; }
        public string? SkId { get; set; }
        public List<BoundaryOrdinate> Ordinates { get; set; } = new List<BoundaryOrdinate>(3010);
        public List<string> StringOrdinates { get; set; } = new List<string>(3010);

        public BoundaryModel(XElement boundary)
        {
            Deserialize(boundary);
        }
        public override XElement Serialize()
        {
            throw new NotImplementedException();
        }

        public override void SoloSerialize()
        {
            throw new NotImplementedException();
        }

        protected override void Deserialize(XElement boundary)
        {
            XElement? recordInfo = boundary.Element("record_info");
            RegistrationDate = recordInfo?.Element("registration_date")?.Value ?? string.Empty;

            XElement? bObjectMunicipalBoundary = boundary.Element("b_object_municipal_boundary");
            XElement? bObject = bObjectMunicipalBoundary?.Element("b_object");
            CadastralNumber = bObject?.Element("reg_numb_border")?.Value ?? string.Empty;

            XElement? typeBoundary = bObject?.Element("type_boundary");
            TypeBoundaryCode = typeBoundary?.Element("code")?.Value ?? string.Empty;
            TypeBoundaryValue = typeBoundary?.Element("value")?.Value ?? string.Empty;

            XElement? entitySpatial = boundary.Element("b_contours_location")?.Element("contours")?.Element("contour")?.Element("entity_spatial");
            SkId = entitySpatial?.Element("sk_id")?.Value ?? string.Empty;

            XElement? orinates = entitySpatial?.Element("spatials_elements")?.Element("spatial_element")?.Element("ordinates");
            if (orinates is not null)
            {
                foreach (var ordinate in orinates.Elements("ordinate"))
                {
                    Int32.TryParse(ordinate?.Element("ord_nmb")?.Value, out int i);
                    Double.TryParse((ordinate?.Element("x")?.Value), CultureInfo.InvariantCulture, out double _x);
                    Double.TryParse((ordinate?.Element("y")?.Value), CultureInfo.InvariantCulture, out double _y);
                    var ordinateValue = new BoundaryOrdinate
                    {
                        X = _x,
                        Y = _y,
                        OrdNum = i
                    };
                    Ordinates.Add(ordinateValue);
                    StringOrdinates.Add(GetOrdinate(ordinateValue));
                }
            }

            if (string.IsNullOrWhiteSpace(CadastralNumber))
                SetRandomCadastralNumber();
        }

        public string GetOrdinate(BoundaryOrdinate ordinate)
        {
            return $"{ordinate.OrdNum}. X: {ordinate.X}, Y: {ordinate.Y}";
        }

    }
}
