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
    public class ParcelModel : BaseModel
    {
        public string? TypeCode { get; set; }
        public string? TypeValue { get; set; }
        public string? SubtypeCode { get; set; }
        public string? SubtypeValue { get; set; }
        public string? CategoryTypeCode { get; set; }
        public string? CategoryTypeValue { get; set; }
        public string? PermittedUseByDocument { get; set; }
        public string? PermittedUseCode { get; set; }
        public string? PermittedUseValue { get; set; }
        public string? Area { get; set; }
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
        public string? Level1Name { get; set; }
        public string? ReadableAddress { get; set; }
        public string? InBoundariesMark { get; set; }
        public string? Cost { get; set; }


        public ParcelModel(XElement parcel) 
        {
            Name = "Parcel";
            Deserialize(parcel);
        }

        protected override void Deserialize(XElement parcel)
        {
            XElement? objectElement = parcel.Element("object");
            XElement? commonData = objectElement?.Element("common_data");
            XElement? type = commonData?.Element("type");
            TypeCode = type?.Element("code")?.Value ?? string.Empty;
            TypeValue = type?.Element("value")?.Value ?? string.Empty;
            Indexer = commonData?.Element("cad_number")?.Value ?? string.Empty;

            XElement? subtype = objectElement?.Element("subtype");
            SubtypeCode = subtype?.Element("code")?.Value ?? string.Empty;
            SubtypeValue = subtype?.Element("value")?.Value ?? string.Empty;

            XElement? paramsElement = parcel.Element("params");
            XElement? category = paramsElement?.Element("category");
            XElement? categoryType = category?.Element("type");
            CategoryTypeCode = categoryType?.Element("code")?.Value ?? string.Empty;
            CategoryTypeValue = categoryType?.Element("value")?.Value ?? string.Empty;

            XElement? permittedUse = paramsElement?.Element("permitted_use")?.Element("permitted_use_established");
            PermittedUseByDocument = permittedUse?.Element("by_document")?.Value ?? string.Empty;
            PermittedUseCode = permittedUse?.Element("land_use")?.Element("code")?.Value ?? string.Empty;
            PermittedUseValue = permittedUse?.Element("land_use")?.Element("value")?.Value ?? string.Empty;

            Area = paramsElement?.Element("area")?.Element("value")?.Value ?? string.Empty;

            XElement? addressLocation = parcel.Element("address_location");
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
            Level1Name = detailedLevel?.Element("level1")?.Element("name_level1")?.Value ?? string.Empty;

            ReadableAddress = address?.Element("readable_address")?.Value ?? string.Empty;

            XElement? relPosition = addressLocation?.Element("rel_position");
            InBoundariesMark = relPosition?.Element("in_boundaries_mark")?.Value ?? string.Empty;

            Cost = parcel.Element("cost")?.Element("value")?.Value ?? string.Empty;
            if (string.IsNullOrWhiteSpace(Indexer))
                SetRandomIndexInsteadOfCadastralNumber();
        }
        public override XElement Serialize()
        {
            var parsel = new XElement("land_record",
                    new XElement("object",
                        new XElement("common_data",
                            new XElement("type",
                                new XElement("code", TypeCode),
                                new XElement("value", TypeValue)
                            ),
                            new XElement("cad_number", Indexer)
                        ),
                        new XElement("subtype",
                            new XElement("code", SubtypeCode),
                            new XElement("value", SubtypeValue)
                        )
                    ),
                    new XElement("params",
                        new XElement("category",
                            new XElement("type",
                                new XElement("code", CategoryTypeCode),
                                new XElement("value", CategoryTypeValue)
                            )
                        ),
                        new XElement("permitted_use",
                            new XElement("permitted_use_established",
                                new XElement("by_document", PermittedUseByDocument),
                                new XElement("land_use",
                                    new XElement("code", PermittedUseCode),
                                    new XElement("value", PermittedUseValue)
                                )
                            )
                        ),
                        new XElement("area",
                            new XElement("value", Area)
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
                                        new XElement("name_level1", Level1Name)
                                    )
                                )
                            ),
                            new XElement("readable_address", ReadableAddress)
                        ),
                        new XElement("rel_position",
                            new XElement("in_boundaries_mark", InBoundariesMark)
                        )
                    ),
                    new XElement("cost",
                        new XElement("value", Cost)
                    )
            );
            return parsel;
        }
        public override void SoloSerialize(string path)
        {
            DateTime dateTime = DateTime.Now;

            XDocument xDoc = new XDocument(
                new XElement("Parcel",
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
                        new XElement("subtype",
                            new XElement("code", SubtypeCode),
                            new XElement("value", SubtypeValue)
                        )
                    ),
                    new XElement("params",
                        new XElement("category",
                            new XElement("type",
                                new XElement("code", CategoryTypeCode),
                                new XElement("value", CategoryTypeValue)
                            )
                        ),
                        new XElement("permitted_use",
                            new XElement("permitted_use_established",
                                new XElement("by_document", PermittedUseByDocument),
                                new XElement("land_use",
                                    new XElement("code", PermittedUseCode),
                                    new XElement("value", PermittedUseValue)
                                )
                            )
                        ),
                        new XElement("area",
                            new XElement("value", Area)
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
                                        new XElement("name_level1", Level1Name)
                                    )
                                )
                            ),
                            new XElement("readable_address", ReadableAddress)
                        ),
                        new XElement("rel_position",
                            new XElement("in_boundaries_mark", InBoundariesMark)
                        )
                    ),
                    new XElement("cost",
                        new XElement("value", Cost)
                    )
                )
            );
            string savePath = Path.Combine(path);
            xDoc.Save(savePath);
            MessageBox.Show($"File saved at: {savePath}");
        }
    }
}
