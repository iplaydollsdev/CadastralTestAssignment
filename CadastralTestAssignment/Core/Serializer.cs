using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Serialization;

namespace CadastralTestAssignment.Core
{
    public static class Serializer
    {
        public static CadastralPlanTerritory? Deserialize(string pathToFile)
        {
            XmlSerializer serializer = new(typeof(CadastralPlanTerritory));

            CadastralPlanTerritory? result;

            using (FileStream fs = new(pathToFile, FileMode.OpenOrCreate))
            {
                result = (CadastralPlanTerritory?)serializer.Deserialize(fs);
                return result;
            }
        }

        public static void Serialize(CadastralPlanTerritory cadastralPlanTerritory, List<BaseRecordModel> selectedRecords, string pathToFile)
        {
            XmlSerializer serializer = new(typeof(CadastralPlanTerritory));
            using (FileStream fs = new(pathToFile, FileMode.OpenOrCreate))
            {
                CadastralPlanTerritory cadastralPlanToSave = new CadastralPlanTerritory();
                cadastralPlanToSave.CadastralBlocks = new List<CadastralBlock>();
                cadastralPlanToSave.CadastralBlocks.Add(new CadastralBlock()
                                                        {
                                                            AreaQuarter = cadastralPlanTerritory.CadastralBlocks?.First().AreaQuarter,
                                                            CadastralNumber = cadastralPlanTerritory.CadastralBlocks?.First().CadastralNumber,
                                                            RecordData = new RecordData() { BaseData = new BaseData() }
                                                        });
                foreach (BaseRecordModel record in selectedRecords)
                {
                    if (record is LandRecord landRecord)
                    {
                        (cadastralPlanToSave.CadastralBlocks.First().RecordData!.BaseData!.LandRecords ??= new List<LandRecord>()).Add(landRecord);
                    }
                    else if (record is BuildRecord buildRecord)
                    {
                        (cadastralPlanToSave.CadastralBlocks.First().RecordData!.BaseData!.BuildRecords ??= new List<BuildRecord>()).Add(buildRecord);
                    }
                    else if (record is ConstructionRecord constructionRecord)
                    {
                        (cadastralPlanToSave.CadastralBlocks.First().RecordData!.BaseData!.ConstructionRecords ??= new List<ConstructionRecord>()).Add(constructionRecord);
                    }
                    else if (record is SpatialData spatialData)
                    {
                        cadastralPlanToSave.CadastralBlocks.First().SpatialData = spatialData;
                    }
                    else if (record is MunicipalBoundaryRecord municipalBoundaryRecord)
                    {
                        (cadastralPlanToSave.CadastralBlocks.First().MunicipalBoundaryRecords ??= new List<MunicipalBoundaryRecord>()).Add(municipalBoundaryRecord);
                    }
                    else if (record is ZonesAndTerritoriesRecord zonesAndTerritoriesRecord)
                    {
                        (cadastralPlanToSave.CadastralBlocks.First().ZonesAndTerritoriesRecords ??=new List<ZonesAndTerritoriesRecord>()).Add(zonesAndTerritoriesRecord);
                    }
                }

                serializer.Serialize(fs, cadastralPlanToSave);
            }
        }

        public static void Serialize(BaseRecordModel selectedRecord, string pathToFile)
        {
            XmlSerializer serializer;
            switch (selectedRecord.ModelName)
            {
                case "Parcel":
                    serializer = new(typeof(LandRecord));
                    break;
                case "ObjectRealty":
                    if (selectedRecord is BuildRecord)
                        serializer = new(typeof(BuildRecord));
                    else
                        serializer = new(typeof(ConstructionRecord));
                    break;
                case "SpatialData":
                    serializer = new(typeof(SpatialData));
                    break;
                case "Bound":
                    serializer = new(typeof(MunicipalBoundaryRecord));
                    break;
                case "Zone":
                    serializer = new(typeof(ZonesAndTerritoriesRecord));
                    break;
                default:
                    return;
            }
            
            using (FileStream fs = new(pathToFile, FileMode.OpenOrCreate))
            {
                serializer.Serialize(fs, selectedRecord);
            }
        }
    }

}