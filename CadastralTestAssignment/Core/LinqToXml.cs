using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CadastralTestAssignment.Core
{
    
    public class LinqToXml
    {
        public List<BaseModel> Models { get; private set; } = new List<BaseModel>();

        public void GetModels(string filePath) {

            XDocument xdoc = XDocument.Load(filePath);

            XElement? extractCadastralPlanTerritory = xdoc.Element("extract_cadastral_plan_territory");
            XElement? landRecords = xdoc?.Element("extract_cadastral_plan_territory")?
                                         .Element("cadastral_blocks")?
                                         .Element("cadastral_block")?
                                         .Element("record_data")?
                                         .Element("base_data")?
                                         .Element("land_records");
            if (landRecords is not null )
            {
                foreach (XElement landRecord in landRecords.Elements("land_record"))
                {
                    Models.Add(new ParcelModel(landRecord));
                }
            }

            XElement? buildReconds = xdoc?.Element("extract_cadastral_plan_territory")?
                             .Element("cadastral_blocks")?
                             .Element("cadastral_block")?
                             .Element("record_data")?
                             .Element("base_data")?
                             .Element("build_records");
            if (buildReconds is not null)
            {
                foreach (XElement buildRecord in buildReconds.Elements("build_record"))
                {
                    Models.Add(new ObjectRealtyModel(buildRecord));
                }
            }

            XElement? constructionRecords = xdoc?.Element("extract_cadastral_plan_territory")?
                 .Element("cadastral_blocks")?
                 .Element("cadastral_block")?
                 .Element("record_data")?
                 .Element("base_data")?
                 .Element("construction_records");
            if (constructionRecords is not null)
            {
                foreach (XElement constructionRecord in constructionRecords.Elements("construction_record"))
                {
                    Models.Add(new ObjectRealtyModel(constructionRecord));
                }
            }

            XElement? spatialRecords = xdoc?.Element("extract_cadastral_plan_territory")?
                 .Element("cadastral_blocks")?
                 .Element("cadastral_block")?
                 .Element("spatial_data");
            if (spatialRecords is not null)
            {
                foreach (XElement spatialRecord in spatialRecords.Elements("entity_spatial"))
                {
                    Models.Add(new SpatialDataModel(spatialRecord));
                }
            }

            XElement? boundaryRecords = xdoc?.Element("extract_cadastral_plan_territory")?
                 .Element("cadastral_blocks")?
                 .Element("cadastral_block")?
                 .Element("municipal_boundaries");
            if (boundaryRecords is not null)
            {
                foreach (XElement boudaryRecord in boundaryRecords.Elements("municipal_boundary_record"))
                {
                    Models.Add(new BoundaryModel(boudaryRecord));
                }
            }

        }
    }
}
