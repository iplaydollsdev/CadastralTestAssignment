using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace CadastralTestAssignment.MVVM.Model
{
    public class ZoneModel : BaseModel
    {
        public string? RegistrationDate { get; private set; }
        public string? TypeBoundaryCode { get; private set; }
        public string? TypeBoundaryValue { get; private set; }
        public string? TypeZoneCode { get; private set; }
        public string? TypeZoneValue { get; private set; }
        public string? SkId { get; private set; }
        public List<SpatialElementModel> SpatialElements { get; private set; } = new();

        public ZoneModel(XElement boundary)
        {
            Name = "Zone";
            Deserialize(boundary);
        }
        public override XElement Serialize()
        {

            var boundaryData = new XElement("zones_and_territories_record");

            XElement recordInfo = new XElement("record_info",
                                      new XElement("registration_date", RegistrationDate));

            XElement bObjectMunicipal = new XElement("b_object_zones_and_territories",
                                            new XElement("b_object",
                                                new XElement("reg_numb_border", Indexer),
                                                new XElement("type_boundary",
                                                    new XElement("code", TypeBoundaryCode),
                                                    new XElement("value", TypeBoundaryValue))),
                                            new XElement("type_zone",
                                                new XElement("code", TypeZoneCode),
                                                new XElement("value", TypeZoneValue)));

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

        public override void SoloSerialize(string path)
        {
            DateTime dateTime = DateTime.Now;

            XDocument xDoc = new XDocument();

            XElement zoneData = Serialize();

            xDoc.Add(zoneData);

            string savePath = Path.Combine(path);
            xDoc.Save(savePath);
            MessageBox.Show($"File saved at: {savePath}");

        }

        protected override void Deserialize(XElement boundary)
        {
            XElement? recordInfo = boundary.Element("record_info");
            RegistrationDate = recordInfo?.Element("registration_date")?.Value ?? string.Empty;

            XElement? bObjectZones = boundary.Element("b_object_zones_and_territories");
            XElement? bObject = bObjectZones?.Element("b_object");
            Indexer = bObject?.Element("reg_numb_border")?.Value ?? string.Empty;

            XElement? typeBoundary = bObject?.Element("type_boundary");
            TypeBoundaryCode = typeBoundary?.Element("code")?.Value ?? string.Empty;
            TypeBoundaryValue = typeBoundary?.Element("value")?.Value ?? string.Empty;

            XElement? typeZone = bObjectZones?.Element("type_zone");
            TypeZoneCode = typeZone?.Element("code")?.Value ?? string.Empty;
            TypeZoneValue = typeZone?.Element("value")?.Value ?? string.Empty;

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
