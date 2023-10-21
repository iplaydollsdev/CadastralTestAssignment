using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace CadastralTestAssignment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string PathToXml { get; set; }
        private DataViewModel _viewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            PathToXml = System.IO.Path.GetFullPath(@"Files\24_21_1003001_2017-05-29_kpt11.xml");
            _viewModel = new DataViewModel(PathToXml);
            DataContext = _viewModel;
            _viewModel.PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            EnableButtons();
            SetProperties();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedItem is null)
                return;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML Files | *.xml";
            saveFileDialog.DefaultExt = "xml";
            saveFileDialog.FileName = $"{_viewModel.SelectedItem.Name}_{DateTime.Now.ToString("yy'-'MM'-'dd'_'HH'-'mm")}.xml";
            bool? success = saveFileDialog.ShowDialog();

            if (success == true)
            {
                _viewModel.SelectedItem.SoloSerialize(saveFileDialog.FileName);
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

        private void SetProperties()
        {
            PropertiesStackPanel.Children.Clear();
            if (_viewModel.SelectedItem == null)
                return;

            var properties = _viewModel.SelectedItem.GetType().GetProperties();

            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(string))
                {
                    if (property.Name == "Indexer")
                        continue;

                    var value = property.GetValue(_viewModel.SelectedItem) as string;
                    if (string.IsNullOrWhiteSpace(value) is false)
                    {
                        var label = new Label
                        {
                            Content = property.Name,
                            HorizontalAlignment = HorizontalAlignment.Center
                        };

                        var textBlock = new TextBlock();
                        textBlock.SetBinding(TextBlock.TextProperty, new Binding(property.Name));
                        textBlock.Text = value;
                        textBlock.Background = Brushes.LightGray;
                        textBlock.Margin = new Thickness(20, 0, 20, 0);


                        PropertiesStackPanel.Children.Add(label);
                        PropertiesStackPanel.Children.Add(textBlock);
                    }
                }
                if (property.PropertyType == typeof(List<SpatialElementModel>))
                {
                    var label = new Label
                    {
                        Content = "SpatialElements",
                        HorizontalAlignment = HorizontalAlignment.Center
                    };

                    var spatialElements = property.GetValue(_viewModel.SelectedItem) as List<SpatialElementModel>;

                    var spatialElementsListBox = new ListBox
                    {
                        IsEnabled = false
                    };

                    foreach (var spatialElement in spatialElements!)
                    {
                        var spatialElementLabel = new Label
                        {
                            Content = "Spatial Element",
                            HorizontalAlignment = HorizontalAlignment.Center
                        };

                        var ordinatesListBox = new ListBox
                        {
                            IsEnabled = false
                        };

                        foreach (var ordinate in spatialElement.Ordinates)
                        {
                            var ordinateLabel = new Label
                            {
                                Content = ordinate.StringView,
                            };

                            ordinatesListBox.Items.Add(ordinateLabel);
                        }

                        var spatialElementStackPanel = new StackPanel();
                        spatialElementStackPanel.Children.Add(spatialElementLabel);
                        spatialElementStackPanel.Children.Add(ordinatesListBox);

                        spatialElementsListBox.Items.Add(spatialElementStackPanel);
                    }

                    PropertiesStackPanel.Children.Add(label);
                    PropertiesStackPanel.Children.Add(spatialElementsListBox);
                }
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
                LinqToXml.ExportToXml(_viewModel.MainDoc, _viewModel.SelectedModels, saveFileDialog.FileName);
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
