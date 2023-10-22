using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CadastralTestAssignment.MVVM.ViewModel
{
    internal class DataViewModel : ViewModelBase
    {
        private const string DEFAULT_FILE = @"Files\24_21_1003001_2017-05-29_kpt11.xml";

        public CadastralPlanTerritory? MainPlan { get; private set; }
        public List<BaseRecordModel> AllRecords { get; private set; } = new();
        public List<BaseRecordModel> SelectedModels { get; private set; } = new();

        public DataViewModel()
        {
            try
            {
                MainPlan = Serializer.Deserialize(DEFAULT_FILE);

                SetLists();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public DataViewModel(string pathToXml)
        {
            try
            {
                MainPlan = Serializer.Deserialize(pathToXml);

                SetLists();
            }
            catch
            {
                throw new Exception("Невозможно открыть файл!");
            }
        }

        private BaseRecordModel? selectedItem;

        public BaseRecordModel? SelectedItem
        {
            get { return selectedItem; }
            set 
            { 
                if (selectedItem != value)
                {
                    selectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
                }
            }
        }

        private void SetLists()
        {
            if (MainPlan == null)
                throw new Exception("Cannot find CadastralPlan");

            var blocks = MainPlan.CadastralBlocks ?? throw new Exception("Cannot find CadastralBlocks");

            foreach (var block in blocks)
            {
                AllRecords.AddRange(block.RecordData?.BaseData?.LandRecords?.Cast<BaseRecordModel>().ToList() ?? new());
                AllRecords.AddRange(block.RecordData?.BaseData?.BuildRecords?.Cast<BaseRecordModel>().ToList() ?? new());
                AllRecords.AddRange(block.RecordData?.BaseData?.ConstructionRecords?.Cast<BaseRecordModel>().ToList() ?? new());
                if (block.SpatialData != null && block.SpatialData.EntitySpatial != null) AllRecords.Add(block.SpatialData);
                AllRecords.AddRange(block.MunicipalBoundaryRecords?.Cast<BaseRecordModel>().ToList() ?? new());
                AllRecords.AddRange(block.ZonesAndTerritoriesRecords?.Cast<BaseRecordModel>().ToList()?? new());

                foreach (var record in AllRecords)
                {
                    if (record is LandRecord landRecord)
                    {
                        record.Index = landRecord.Object!.CommonData!.CadNumber!;
                        record.ModelName = "Parcel";
                    }
                    else if (record is BuildRecord buildRecord)
                    {
                        record.Index = buildRecord.Object!.CommonData!.CadNumber!;
                        record.ModelName = "ObjectRealty";
                    }
                    else if (record is ConstructionRecord constructionRecord)
                    {
                        record.Index = constructionRecord.Object!.CommonData!.CadNumber!;
                        record.ModelName = "ObjectRealty";
                    }else if (record is SpatialData)
                    {
                        record.Index = "SpatialData";
                        record.ModelName = "SpatialData";
                    }else if (record is MunicipalBoundaryRecord municipalBoundaryRecord)
                    {
                        record.Index = municipalBoundaryRecord.BObjectMunicipalBoundary!.BObject!.RegNumberBorder!;
                        record.ModelName = "Bound";
                    }
                    else if (record is ZonesAndTerritoriesRecord zonesAndTerritoriesRecord)
                    {
                        record.Index = zonesAndTerritoriesRecord.BObjectZonesAndTerritories!.BObject!.RegNumberBorder!;
                        record.ModelName = "Zones";
                    }
                }
            }
        }
    }
}
