

using CadastralTestAssignment.MVVM.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace CadastralTestAssignment.MVVM.ViewModel
{
    internal class DataViewModel : ViewModelBase
    {
        public DocModel MainDoc { get; private set; }
        public List<BaseModel> Models { get; private set; }
        public List<BaseModel> SelectedModels { get; private set; } = new();

        public DataViewModel(string pathToXml)
        {
            Models = LinqToXml.ImportFromXml(filePath: pathToXml);
            MainDoc = Models.First() as DocModel ?? new DocModel();
            Models.RemoveAt(0);
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
