using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CadastralTestAssignment.MVVM.Model
{
    public class OrdinateModel : BaseModel
    {
        public double X { get; set; }
        public double Y { get; set; }
        public int? OrdNmb { get; set; }
        public int? NumGeopoint { get; set; }
        public string? GeopointCode { get; set; }
        public string? GeopointValue { get; set; }
        public string? GeopointDelta { get; set; }
        public string? StringView {  get; set; }
        public OrdinateModel(XElement xElement)
        {
            Deserialize(xElement);
        }

        protected override void Deserialize(XElement ordinate)
        {
            double.TryParse((ordinate?.Element("x")?.Value), CultureInfo.InvariantCulture, out double _x);
            double.TryParse((ordinate?.Element("y")?.Value), CultureInfo.InvariantCulture, out double _y);
            X = _x;
            Y = _y;

            int.TryParse((ordinate?.Element("ord_nmb")?.Value), CultureInfo.InvariantCulture, out int _ordNmb);
            int.TryParse((ordinate?.Element("num_geopoint")?.Value), CultureInfo.InvariantCulture, out int _numGeopoint);
            if (_ordNmb != 0) OrdNmb = _ordNmb;
            if (_numGeopoint != 0) NumGeopoint = _numGeopoint;

            GeopointCode = ordinate?.Element("geopoint_opred")?.Element("code")?.Value;
            GeopointValue = ordinate?.Element("geopoint_opred")?.Element("value")?.Value;
            GeopointDelta = ordinate?.Element("delta_geopoint")?.Value;

            StringBuilder sb = new StringBuilder();
            sb.Append(OrdNmb.ToString() ?? "");
            sb.Append(" X:");
            sb.Append(X);
            sb.Append(" Y:");
            sb.Append(Y);
            sb.Append(" ");
            sb.AppendLine(GeopointValue ?? "");
            StringView = sb.ToString();
        }

        public override void SoloSerialize(string path)
        {
            Console.WriteLine("Nothing to serialize solo");
        }

        public override XElement Serialize()
        {
            var ordinate = new XElement("ordinate",
                                new XElement("x", X),
                                new XElement("y", Y));
            if (OrdNmb != null && OrdNmb != 0)
            {
                var ordNmb = new XElement("ord_nmb", OrdNmb);
                ordinate.Add(ordNmb);
            }
            if (NumGeopoint != null && NumGeopoint != 0)
            {
                var numGeopoint = new XElement("num_geopoint", NumGeopoint);
                ordinate.Add(numGeopoint);
            }
            if (!string.IsNullOrWhiteSpace(GeopointValue))
            {
                var geopointOpder = new XElement("geopoint_opred",
                                    new XElement("code", GeopointCode),
                                    new XElement("value", GeopointValue));
                ordinate.Add(geopointOpder);
            }
            if (!string.IsNullOrWhiteSpace(GeopointDelta))
            {
                var geopointDelta = new XElement("delta_geopoint", GeopointDelta);
                ordinate.Add(geopointDelta);
            }

            return ordinate;
        }
    }
}
