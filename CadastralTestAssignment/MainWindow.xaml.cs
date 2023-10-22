using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Windows.Media.Protection.PlayReady;

namespace CadastralTestAssignment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string PathToXml { get; set; } = string.Empty;
        private DataViewModel _viewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new DataViewModel();
            DataContext = _viewModel;
            _viewModel.PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            EnableButtons();
            SetPropertiesView();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedItem is null)
                return;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML Files | *.xml";
            saveFileDialog.DefaultExt = "xml";
            saveFileDialog.FileName = $"{_viewModel.SelectedItem.ModelName}_{DateTime.Now.ToString("yy'-'MM'-'dd'_'HH'-'mm")}.xml";
            bool? success = saveFileDialog.ShowDialog();

            if (success == true)
            {
                //_viewModel.SelectedItem.SoloSerialize(saveFileDialog.FileName);
            }

        }

        private void EnableButtons()
        {
            SaveSoloSelected.IsEnabled = true;
            if (_viewModel.SelectedModels.Count > 0)
            {
                SaveAllSelected.IsEnabled = true;
            }
            else
            {
                SaveAllSelected.IsEnabled = false;
            }
        }

        private void SetPropertiesView()
        {
            PropertiesStackPanel.Children.Clear();
            var item = _viewModel.SelectedItem;
            if (item == null)
                return;

            Serializer.DisplayObjectProperties(item, PropertiesStackPanel);
            //AddLabelAndTextblock("TypeCode", item.)
            //foreach (var property in properties)
            //{

            //}
        }


        private void AddLabelAndTextblock(string label, string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                Label labelElement = new Label { Content = label };
                TextBlock textElement = new TextBlock { Text = text };
                PropertiesStackPanel.Children.Add(labelElement);
                PropertiesStackPanel.Children.Add(textElement);
            }
        }

        private void SaveAllButtonClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML Files | *.xml";
            saveFileDialog.DefaultExt = "xml";
            saveFileDialog.FileName = $"extract_cadastral_plan_territory_{DateTime.Now.ToString("yy'-'MM'-'dd'_'HH'-'mm")}.xml";
            bool? success = saveFileDialog.ShowDialog();

            if (success == true)
            {
                //SERIALIZE
                //LinqToXml.ExportToXml(_viewModel.MainDoc, _viewModel.SelectedModels, saveFileDialog.FileName);
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var item = _viewModel.SelectedItem;
            if (item != null && !_viewModel.SelectedModels.Contains(item))
            {
                _viewModel.SelectedModels.Add(item);
                EnableButtons();
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var item = _viewModel.SelectedItem;
            if (item != null && _viewModel.SelectedModels.Contains(item))
            {
                _viewModel.SelectedModels.Remove(item);
                EnableButtons();
            }
        }

        private void OpenNewFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new();
            fileDialog.Filter = "XML Files | *.xml";
            bool? success = fileDialog.ShowDialog();

            if (success == true)
            {
                string path = fileDialog.FileName;
                    PathToXml = path;
                    OpenNewFile();
            }
        }

        private void OpenNewFile()
        {
            _viewModel.PropertyChanged -= OnPropertyChanged;
            _viewModel = new DataViewModel(PathToXml);
            DataContext = _viewModel;
            _viewModel.PropertyChanged += OnPropertyChanged;
        }
    }
}
