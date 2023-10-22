

using CadastralTestAssignment.MVVM.Model;
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
        public DocModel MainDoc { get; private set; } = new();
        public List<BaseModel> Models { get; private set; } = new();
        public List<BaseModel> SelectedModels { get; private set; } = new();

        public DataViewModel()
        {
            try
            {
                Models = LinqToXml.ImportFromXml(filePath: DEFAULT_FILE);
                MainDoc = Models.First() as DocModel ?? new DocModel();
                Models.RemoveAt(0);
            }
            catch (System.Exception)
            {
            }
        }
        public DataViewModel(string pathToXml)
        {
            try
            {
                Models = LinqToXml.ImportFromXml(filePath: pathToXml);
                MainDoc = Models.First() as DocModel ?? new DocModel();
                Models.RemoveAt(0);
            }
            catch (System.Exception)
            {
                MessageBox.Show("Неподходящий файл!");
            }
        }

        private BaseModel? selectedItem;

        public BaseModel? SelectedItem
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

    }
}
