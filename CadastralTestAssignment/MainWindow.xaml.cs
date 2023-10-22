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

        private void EnableButtons()
        {
            SaveSoloSelected.IsEnabled = true;
            if (_viewModel.SelectedModels.Count > 1)
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

            DisplayRecordProperties(item, PropertiesStackPanel);
        }
        private void DisplayRecordProperties(object obj, StackPanel stackPanel)
        {
            if (obj == null)
            {
                return;
            }

            Type objectType = obj.GetType();
            PropertyInfo[] properties = objectType.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                object? propertyValue = property.GetValue(obj);
                string propertyName = property.Name;

                if (propertyValue != null)
                {
                    if (property.PropertyType.IsPrimitive || property.PropertyType == typeof(string))
                    {
                        if (property.Name == "Index" || property.Name == "ModelName" || property.Name == "IsSelected")
                            continue;

                        string? value = propertyValue.ToString();
                        if (!string.IsNullOrEmpty(value))
                        {
                            stackPanel.Children.Add(new Label { Content = propertyName, HorizontalAlignment = HorizontalAlignment.Center });
                            stackPanel.Children.Add(new TextBox { Text = value, Background = Brushes.LightGray, Margin = new Thickness(20, 0, 20, 0), IsReadOnly = true, IsReadOnlyCaretVisible = true });
                        }
                    }
                    else if (propertyValue is System.Collections.IEnumerable enumerable)
                    {
                        ListBox listBox = new ListBox() { Margin = new Thickness(20, 0, 20, 0) };
                        foreach (var item in enumerable)
                        {
                            if (item is Ordinate ordinate)
                            {
                                listBox.Items.Add(ordinate.GetString());
                            }
                            else if (item is SpatialElement spatialElement)
                            {
                                DisplayRecordProperties(spatialElement, stackPanel);
                            }
                            else if (item is Contour contour)
                            {
                                DisplayRecordProperties(contour, stackPanel);
                            }
                            else
                            {
                                listBox.Items.Add(item.ToString());
                            }
                        }
                        listBox.IsEnabled = false;
                        stackPanel.Children.Add(new Label { Content = propertyName, HorizontalAlignment = HorizontalAlignment.Center });
                        stackPanel.Children.Add(listBox);
                    }
                    else
                    {
                        DisplayRecordProperties(propertyValue, stackPanel);
                    }
                }
            }
        }

        private void SaveButtonClicked(object sender, RoutedEventArgs e)
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
                Serializer.Serialize(_viewModel.SelectedItem, saveFileDialog.FileName);
                MessageBox.Show($"Файл успешно сохранен!\n{saveFileDialog.FileName}");
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
                if (_viewModel.MainPlan != null)
                Serializer.Serialize(_viewModel.MainPlan, _viewModel.SelectedModels, saveFileDialog.FileName);
                MessageBox.Show($"Файл успешно сохранен!\n{saveFileDialog.FileName}");
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
                    OpenFile();
            }
        }

        private void OpenFile()
        {
            try
            {
                _viewModel.PropertyChanged -= OnPropertyChanged;
                _viewModel = new DataViewModel(PathToXml);
                DataContext = _viewModel;
                _viewModel.PropertyChanged += OnPropertyChanged;
            }
            catch (Exception e)
            {
                _viewModel.PropertyChanged += OnPropertyChanged;
                MessageBox.Show(e.Message);
            }

        }

    }
}
