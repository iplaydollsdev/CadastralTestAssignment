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
    public class BoundaryModel : BaseModel
    {
        public string? RegistrationDate { get; set; }
        public string? TypeBoundaryCode { get; set; }
        public string? TypeBoundaryValue { get; set; }
        public string? SkId { get; set; }
        public List<SpatialElementModel> SpatialElements { get; set; } = new();

        public BoundaryModel(XElement boundary)
        {
            Deserialize(boundary);
        }
        public override XElement Serialize()
        {
            DateTime dateTime = DateTime.Now;

            var boundaryData = new XElement("BoundaryData",
                                        new XAttribute("CadastralNumber", Indexer ?? string.Empty),
                                        new XAttribute("DateCreated", dateTime.ToString("yyyy'-'MM'-'dd")));

            XElement recordInfo = new XElement("record_info",
                                      new XElement("registration_date", RegistrationDate));

            XElement bObjectMunicipal = new XElement("b_object_municipal_boundary",
                                            new XElement("b_object",
                                                new XElement("reg_numb_border", Indexer),
                                                new XElement("type_boundary",
                                                    new XElement("code", TypeBoundaryCode),
                                                    new XElement("value", TypeBoundaryValue))));

            XElement bContoursLocation = new XElement("b_contours_location");
            XElement contours = new XElement("contours");
            XElement contour = new XElement("contour");
            XElement entitySpatial = new XElement("entity_spatial");

            XElement skId = new XElement("sk_id", SkId);
            XElement spatialsElements = new XElement("spatials_elements");

            foreach (var spatial in SpatialElements)
            {
                var spatialElement = spatial.Serialize();
                spatialsElements!.Add(spatialElement);
            }

            entitySpatial.Add(skId);
            entitySpatial.Add(spatialsElements);
            contour.Add(entitySpatial);
            contours.Add(contour);
            bContoursLocation.Add(contours);

            boundaryData.Add(recordInfo);
            boundaryData.Add(bObjectMunicipal);
            boundaryData.Add(bContoursLocation);

            return boundaryData;
        }

        public override void SoloSerialize()
        {
            DateTime dateTime = DateTime.Now;

            XDocument xDoc = new XDocument();

            XElement boundaryData = Serialize();

            xDoc.Add(boundaryData);

            string savePath = Path.Combine("D:", $"BoundaryData_{dateTime.ToString("yy'-'MM'-'dd'_'HH'-'mm")}.xml");
            xDoc.Save(savePath);
            MessageBox.Show($"File saved at: {savePath}");

        }

        protected override void Deserialize(XElement boundary)
        {
            XElement? recordInfo = boundary.Element("record_info");
            RegistrationDate = recordInfo?.Element("registration_date")?.Value ?? string.Empty;

            XElement? bObjectMunicipalBoundary = boundary.Element("b_object_municipal_boundary");
            XElement? bObject = bObjectMunicipalBoundary?.Element("b_object");
            Indexer = bObject?.Element("reg_numb_border")?.Value ?? string.Empty;

            XElement? typeBoundary = bObject?.Element("type_boundary");
            TypeBoundaryCode = typeBoundary?.Element("code")?.Value ?? string.Empty;
            TypeBoundaryValue = typeBoundary?.Element("value")?.Value ?? string.Empty;

            XElement? entitySpatial = boundary.Element("b_contours_location")?.Element("contours")?.Element("contour")?.Element("entity_spatial");
            SkId = entitySpatial?.Element("sk_id")?.Value ?? string.Empty;

            XElement? spatialElements = entitySpatial?.Element("spatials_elements");
            if (spatialElements is not null)
            {
                foreach (var spatial in spatialElements.Elements("spatial_element"))
                {
                    var spatialInstance = new SpatialElementModel(spatial);
                    SpatialElements.Add(spatialInstance);
                }
            }

            if (string.IsNullOrWhiteSpace(Indexer))
                SetRandomIndexInsteadOfCadastralNumber();
        }
    }
}
