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
    public class ObjectRealtyModel : BaseModel
    {
        public string? TypeCode { get; set; }
        public string? TypeValue { get; set; }
        public string? Area { get; set; }
        public string? PurposeCode { get; set; }
        public string? PurposeValue { get; set; }
        public string? Okato { get; set; }
        public string? Kladr { get; set; }
        public string? RegionCode { get; set; }
        public string? RegionValue { get; set; }
        public string? DistrictType { get; set; }
        public string? DistrictName { get; set; }
        public string? LocalityType { get; set; }
        public string? LocalityName { get; set; }
        public string? StreetType { get; set; }
        public string? StreetName { get; set; }
        public string? Level1Type { get; set; }
        public string? Level1Name { get; set; }
        public string? ReadableAddress { get; set; }
        public string? LocationOkato { get; set; }
        public string? LocationRegionCode { get; set; }
        public string? LocationRegionValue { get; set; }
        public string? LocationDescription { get; set; }
        public string? Cost { get; set; }

        public ObjectRealtyModel(XElement objectRealty)
        {
            Deserialize(objectRealty);
        }

        public override XElement Serialize()
        {
            DateTime dateTime = DateTime.Now;

            var objectRealy = new XElement("ObjectRealty",
                    new XAttribute("CadastralNumber", Indexer ?? "01:01:0000001:1"),
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
                        new XElement("area", Area),
                        new XElement("purpose",
                            new XElement("code", PurposeCode),
                            new XElement("value", PurposeValue)
                            )
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
                                    new XElement("locality",
                                        new XElement("type_locality", LocalityType),
                                        new XElement("name_locality", LocalityName)
                                    )
                                ),
                                new XElement("detailed_level",
                                    new XElement("street",
                                        new XElement("type_street", StreetType),
                                        new XElement("name_street", StreetName)
                                    ),
                                    new XElement("level1",
                                        new XElement("type_level1", Level1Type),
                                        new XElement("name_level1", Level1Name)
                                    )
                                )
                            ),
                            new XElement("readable_address", ReadableAddress)
                        ),
                        new XElement("location",
                            new XElement("okato", Okato),
                            new XElement("region",
                                new XElement("code", RegionCode),
                                new XElement("value", RegionValue) 
                            ),
                            new XElement("position_descrition", LocationDescription)
                        )
                    ),
                    new XElement("cost",
                        new XElement("value", Cost)
                    )
            ));
            return objectRealy;
        }

        public override void SoloSerialize()
        {
            DateTime dateTime = DateTime.Now;

            XDocument xDoc = new XDocument(
            new XElement("ObjectRealty",
                    new XAttribute("CadastralNumber", Indexer ?? "01:01:0000001:1"),
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
                        new XElement("area", Area),
                        new XElement("purpose",
                            new XElement("code", PurposeCode),
                            new XElement("value", PurposeValue)
                            )
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
                                    new XElement("locality",
                                        new XElement("type_locality", LocalityType),
                                        new XElement("name_locality", LocalityName)
                                    )
                                ),
                                new XElement("detailed_level",
                                    new XElement("street",
                                        new XElement("type_street", StreetType),
                                        new XElement("name_street", StreetName)
                                    ),
                                    new XElement("level1",
                                        new XElement("type_level1", Level1Type),
                                        new XElement("name_level1", Level1Name)
                                    )
                                )
                            ),
                            new XElement("readable_address", ReadableAddress)
                        ),
                        new XElement("location",
                            new XElement("okato", Okato),
                            new XElement("region",
                                new XElement("code", RegionCode),
                                new XElement("value", RegionValue)
                            ),
                            new XElement("position_descrition", LocationDescription)
                        )
                    ),
                    new XElement("cost",
                        new XElement("value", Cost)
                    )
            )));
            string savePath = Path.Combine("D:", $"ObjectRealty_{dateTime.ToString("yy'-'MM'-'dd'_'HH'-'mm")}.xml");
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
            Area = paramsElement?.Element("area")?.Element("value")?.Value ?? string.Empty;

            XElement? purpose = paramsElement?.Element("purpose");
            PurposeCode = purpose?.Element("code")?.Value ?? string.Empty;
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

            XElement? locality = levelSettlement?.Element("locality");
            LocalityType = locality?.Element("type_locality")?.Value ?? string.Empty;
            LocalityName = locality?.Element("name_locality")?.Value ?? string.Empty;

            XElement? detailedLevel = addressFias?.Element("detailed_level");
            XElement? street = detailedLevel?.Element("street");

            StreetType = street?.Element("type_street")?.Value ?? string.Empty;
            StreetName = street?.Element("name_street")?.Value ?? string.Empty;
            Level1Type = detailedLevel?.Element("level1")?.Element("type_level1")?.Value ?? string.Empty;
            Level1Name = detailedLevel?.Element("level1")?.Element("name_level1")?.Value ?? string.Empty;

            ReadableAddress = address?.Element("readable_address")?.Value ?? string.Empty;

            XElement? location = addressLocation?.Element("location");
            LocationOkato = location?.Element("okato")?.Value ?? string.Empty;

            XElement? locationRegion = location?.Element("region");
            LocationRegionCode = locationRegion?.Element("code")?.Value ?? string.Empty;
            LocationRegionValue = locationRegion?.Element("value")?.Value ?? string.Empty;
            LocationDescription = location?.Element("position_description")?.Value ?? string.Empty;

            Cost = xElement.Element("cost")?.Element("value")?.Value ?? string.Empty;
            if (string.IsNullOrWhiteSpace(Indexer))
                SetRandomIndexInsteadOfCadastralNumber();
        }
    }
}
