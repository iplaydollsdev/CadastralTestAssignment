using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace CadastralTestAssignment.Core
{
    
    public static class LinqToXml
    {
        public static List<BaseModel> ImportFromXml(string filePath) {

            List<BaseModel> models = new();
            XDocument xdoc = XDocument.Load(filePath);

            XElement? baseData = xdoc?.Element("extract_cadastral_plan_territory")?
                                      .Element("cadastral_blocks")?
                                      .Element("cadastral_block")?
                                      .Element("record_data")?
                                      .Element("base_data");
            XElement? cadastralBlock = xdoc?.Element("extract_cadastral_plan_territory")?
                                            .Element("cadastral_blocks")?
                                            .Element("cadastral_block");

            XElement? extractCadastralPlanTerritory = xdoc?.Element("extract_cadastral_plan_territory");
            if (extractCadastralPlanTerritory != null)
                models.Add(new DocModel(extractCadastralPlanTerritory));

            XElement? landRecords = baseData?.Element("land_records");
            if (landRecords is not null )
            {
                foreach (XElement landRecord in landRecords.Elements("land_record"))
                {
                    models.Add(new ParcelModel(landRecord));
                }
            }

            XElement? buildReconds = baseData?.Element("build_records");
            if (buildReconds is not null)
            {
                foreach (XElement buildRecord in buildReconds.Elements("build_record"))
                {
                    models.Add(new ObjectRealtyModel(buildRecord));
                }
            }

            XElement? constructionRecords = baseData?.Element("construction_records");
            if (constructionRecords is not null)
            {
                foreach (XElement constructionRecord in constructionRecords.Elements("construction_record"))
                {
                    models.Add(new ObjectRealtyModel(constructionRecord));
                }
            }

            XElement? spatialRecords = cadastralBlock?.Element("spatial_data");
            if (spatialRecords is not null)
            {
                foreach (XElement spatialRecord in spatialRecords.Elements("entity_spatial"))
                {
                    models.Add(new SpatialDataModel(spatialRecord));
                }
            }

            XElement? boundaryRecords = cadastralBlock?.Element("municipal_boundaries");
            if (boundaryRecords is not null)
            {
                foreach (XElement boudaryRecord in boundaryRecords.Elements("municipal_boundary_record"))
                {
                    models.Add(new BoundaryModel(boudaryRecord));
                }
            }

            XElement? zoneRecords = cadastralBlock?.Element("zones_and_territories_boundaries");
            if (zoneRecords is not null)
            {
                foreach (XElement zoneRecord in zoneRecords.Elements("zones_and_territories_record"))
                {
                    models.Add(new ZoneModel(zoneRecord));
                }
            }

            return models;
        }
        public static void ExportToXml(DocModel docModel, List<BaseModel> models, string filePath)
        {
            DateTime dateTime = DateTime.Now;
            XDocument xDoc = new XDocument();
            var extractCadastralPlanTerritory = new XElement("extract_cadastral_plan_territory");
            var detailsStatement = new XElement("details_statement",
                                        new XElement("group_top_requisites",
                                                new XElement("organ_registr_rights", docModel.OrganRegistrRights),
                                                new XElement("date_formation", docModel.DateFormation),
                                                new XElement("registration_number", docModel.RegistrationNumber)));
            var detailsRequest = new XElement("details_request",
                                    new XElement("date_received_request", docModel.DateReceivedRequest),
                                    new XElement("date_receipt_request_reg_authority_rights", docModel.DateReceiptRequestRegAuthorityRights));

            var cadastralBlocks = new XElement("cadastral_blocks");
            var cadastralBlock =  new XElement("cadastral_block",
                                        new XElement("cadastral_number", docModel.Indexer),
                                        new XElement("area_qurter",
                                            new XElement("area", docModel.Area),
                                            new XElement("unit", docModel.Unit)));
            var recordData = new XElement("record_data");
            var baseData = new XElement("base_data");

            var landRecords = new XElement("land_records");
            var buildRecords = new XElement("build_records");
            var constructionRecords = new XElement("construction_records");

            var spatialData = new XElement("spatial_data");
            var municipalBoundaries = new XElement("municipal_boundaries");
            var zonesAndTerritoriesBoundaries = new XElement("zones_and_territories_boundaries");

            foreach (BaseModel model in models)
            {
                var record = model.Serialize();

                if (model is ParcelModel)
                {
                    landRecords.Add(record);
                }
                if (model is ObjectRealtyModel)
                {
                    buildRecords.Add(record);
                }
                if (model is ObjectRealtyConstructModel)
                {
                    constructionRecords.Add(record);
                }
                if (model is SpatialDataModel)
                {
                    spatialData.Add(record);
                }
                if (model is BoundaryModel)
                {
                    municipalBoundaries.Add(record);
                }
                if (model is ZoneModel) 
                {
                    zonesAndTerritoriesBoundaries.Add(record);
                }

            }

            baseData.Add(landRecords, buildRecords, constructionRecords);
            recordData.Add(baseData);

            cadastralBlock.Add(recordData, spatialData, municipalBoundaries, zonesAndTerritoriesBoundaries);
            cadastralBlocks.Add(cadastralBlock);
            extractCadastralPlanTerritory.Add(detailsStatement, detailsRequest, cadastralBlocks);

            xDoc.Add(extractCadastralPlanTerritory);

            string savePath = Path.Combine(filePath);
            xDoc.Save(savePath);
            MessageBox.Show($"File saved at: {savePath}");

        }
    }
}
