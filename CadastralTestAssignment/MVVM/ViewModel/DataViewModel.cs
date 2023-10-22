using CadastralTestAssignment.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace CadastralTestAssignment.MVVM.ViewModel
{
    internal class DataViewModel : ViewModelBase
    {
        private const string DEFAULT_FILE = @"Files\24_21_1003001_2017-05-29_kpt11.xml";

        public CadastralPlanTerritory? MainPlan { get; private set; }
        public List<BaseRecordModel>? Parcels { get; private set; }
        public List<BaseRecordModel>? ObjectRealties { get; private set; }
        public BaseRecordModel? SpatialData { get; private set; }
        public List<BaseRecordModel>? Bounds { get; private set; }
        public List<BaseRecordModel>? Zones { get; private set; }

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
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
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
                Parcels = block.RecordData?.BaseData?.LandRecords?.Cast<BaseRecordModel>().ToList();
                ObjectRealties = block.RecordData?.BaseData?.BuildRecords?.Cast<BaseRecordModel>().ToList();
                ObjectRealties?.AddRange(block.RecordData?.BaseData?.BuildRecords?.Cast<BaseRecordModel>().ToList() ?? new List<BaseRecordModel>());
                SpatialData = block.SpatialData;
                Bounds = block.MunicipalBoundaryRecords?.Cast<BaseRecordModel>().ToList();
                Zones = block.ZonesAndTerritoriesRecords?.Cast<BaseRecordModel>().ToList();

                AllRecords.AddRange(Parcels ?? new List<BaseRecordModel>());
                AllRecords.AddRange(ObjectRealties ?? new List<BaseRecordModel>());
                if (SpatialData != null) AllRecords.Add(SpatialData);
                AllRecords.AddRange(Bounds ?? new List<BaseRecordModel>());
                AllRecords.AddRange(Zones ?? new List<BaseRecordModel>());

                foreach (var record in AllRecords)
                {
                    if (record is LandRecord landRecord)
                    {
                        record.Index = landRecord.Object.CommonData.CadNumber;
                        record.ModelName = "Parcel";
                    }
                    else if (record is BuildRecord buildRecord)
                    {
                        record.Index = buildRecord.Object.CommonData.CadNumber;
                        record.ModelName = "ObjectRealty";
                    }
                    else if (record is ConstructionRecord constructionRecord)
                    {
                        record.Index = constructionRecord.Object.CommonData.CadNumber;
                        record.ModelName = "ObjectRealty";
                    }else if (record is SpatialData spatialData)
                    {
                        record.Index = "SpatialData";
                        record.ModelName = "SpatialData";
                    }else if (record is MunicipalBoundaryRecord municipalBoundaryRecord)
                    {
                        record.Index = municipalBoundaryRecord.BObjectMunicipalBoundary.BObject.RegNumberBorder;
                        record.ModelName = "Bound";
                    }
                    else if (record is ZonesAndTerritoriesRecord zonesAndTerritoriesRecord)
                    {
                        record.Index = zonesAndTerritoriesRecord.BObjectZonesAndTerritories.BObject.RegNumberBorder;
                        record.ModelName = "Zones";
                    }
                }
            }
        }
    }
}
