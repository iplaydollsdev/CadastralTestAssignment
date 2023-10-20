using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace CadastralTestAssignment.MVVM.Model
{
    public class ObjectRealtyConstructModel : BaseModel
    {
        public string? TypeCode { get; set; }
        public string? TypeValue { get; set; }
        public string? PurposeValue { get; set; }
        public string? Okato { get; set; }
        public string? Kladr { get; set; }
        public string? RegionCode { get; set; }
        public string? RegionValue { get; set; }
        public string? DistrictType { get; set; }
        public string? DistrictName { get; set; }
        public string? DetailedLevel { get; set; }
        public string? ReadableAddress { get; set; }

        public ObjectRealtyConstructModel(XElement objectRealtyConstrust)
        {
            Deserialize(objectRealtyConstrust);
        }

        public override XElement Serialize()
        {
            DateTime dateTime = DateTime.Now;

            var objectRealyConstruct = new XElement("ObjectRealtyConstruction",
                    new XAttribute("CadastralNumber", Indexer ?? string.Empty),
                    new XAttribute("State", TypeCode ?? string.Empty),
                    new XAttribute("DateCreated", dateTime.ToString("yyyy'-'MM'-'dd")),
                    new XElement("object",
                        new XElement("common_data",
                            new XElement("type",
                                new XElement("code", TypeCode),
                                new XElement("value", TypeValue)
                            ),
                            new XElement("cad_number", Indexer)
                    ),
                    new XElement("params",
                        new XElement("purpose", PurposeValue)
                    ),
                    new XElement("address_location",
                        new XElement("address",
                            new XElement("address_fias",
                                new XElement("level_settlement",
                                    new XElement("okato", Okato),
                                    new XElement("kladr", Kladr),
                                    new XElement("region",
                                        new XElement("code", RegionCode),
                                        new XElement("value", RegionValue)
                                    ),
                                    new XElement("district",
                                        new XElement("type_district", DistrictType),
                                        new XElement("name_district", DistrictName)
                            ),
                            new XElement("detailed_level",
                                new XElement("other", DetailedLevel),
                            new XElement("readable_address", ReadableAddress))))
                                    )
                                )
            ));
            return objectRealyConstruct;
        }

        public override void SoloSerialize()
        {
            DateTime dateTime = DateTime.Now;

            XDocument xDoc = new XDocument(
            new XElement("ObjectRealtyConstruction",
                    new XAttribute("CadastralNumber", Indexer ?? string.Empty),
                    new XAttribute("State", TypeCode ?? string.Empty),
                    new XAttribute("DateCreated", dateTime.ToString("yyyy'-'MM'-'dd")),
                    new XElement("object",
                        new XElement("common_data",
                            new XElement("type",
                                new XElement("code", TypeCode),
                                new XElement("value", TypeValue)
                            ),
                            new XElement("cad_number", Indexer)
                    ),
                    new XElement("params",
                        new XElement("purpose", PurposeValue)
                    ),
                    new XElement("address_location",
                        new XElement("address",
                            new XElement("address_fias",
                                new XElement("level_settlement",
                                    new XElement("okato", Okato),
                                    new XElement("kladr", Kladr),
                                    new XElement("region",
                                        new XElement("code", RegionCode),
                                        new XElement("value", RegionValue)
                                    ),
                                    new XElement("district",
                                        new XElement("type_district", DistrictType),
                                        new XElement("name_district", DistrictName)
                            ),
                            new XElement("detailed_level", 
                                new XElement("other", DetailedLevel),
                            new XElement("readable_address", ReadableAddress)))                                      )
                                    )
                                )
            )));

            string savePath = Path.Combine("D:", $"ObjectRealtyConstruction_{dateTime.ToString("yy'-'MM'-'dd'_'HH'-'mm")}.xml");
            xDoc.Save(savePath);
            MessageBox.Show($"File saved at: {savePath}");
        }

        protected override void Deserialize(XElement xElement)
        {
            XElement? objectElement = xElement.Element("object");
            XElement? commonData = objectElement?.Element("common_data");
            XElement? type = commonData?.Element("type");
            TypeCode = type?.Element("code")?.Value ?? string.Empty;
            TypeValue = type?.Element("value")?.Value ?? string.Empty;
            Indexer = commonData?.Element("cad_number")?.Value ?? string.Empty;

            XElement? paramsElement = xElement.Element("params");
            XElement? purpose = paramsElement?.Element("purpose");
            PurposeValue = purpose?.Element("value")?.Value ?? string.Empty;

            XElement? addressLocation = xElement.Element("address_location");
            XElement? address = addressLocation?.Element("address");
            XElement? addressFias = address?.Element("address_fias");
            XElement? levelSettlement = addressFias?.Element("level_settlement");

            Okato = levelSettlement?.Element("okato")?.Value ?? string.Empty;
            Kladr = levelSettlement?.Element("kladr")?.Value ?? string.Empty;

            XElement? region = levelSettlement?.Element("region");
            RegionCode = region?.Element("code")?.Value ?? string.Empty;
            RegionValue = region?.Element("value")?.Value ?? string.Empty;

            XElement? district = levelSettlement?.Element("district");
            DistrictType = district?.Element("type_district")?.Value ?? string.Empty;
            DistrictName = district?.Element("name_district")?.Value ?? string.Empty;


            XElement? detailedLevel = addressFias?.Element("detailed_level");

            DetailedLevel = detailedLevel?.Element("other")?.Value ?? string.Empty;

            ReadableAddress = address?.Element("readable_address")?.Value ?? string.Empty;

            if (string.IsNullOrWhiteSpace(Indexer))
                SetRandomIndexInsteadOfCadastralNumber();
        }
    }
}
