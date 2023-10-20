

using CadastralTestAssignment.MVVM.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace CadastralTestAssignment.MVVM.ViewModel
{
    internal class DataViewModel : ViewModelBase
    {
        public List<BaseModel> Models { get; private set; }


        public DataViewModel(string pathToXml)
        {
            LinqToXml linqToXml = new LinqToXml();
            linqToXml.GetModels(filePath: pathToXml);
            Models = linqToXml.Models;
            selectedItem = Models.First();
        }

        private BaseModel selectedItem;

        public BaseModel SelectedItem
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
