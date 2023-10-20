using CadastralTestAssignment.MVVM.Model;
using CadastralTestAssignment.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Xml;
using System.ComponentModel;
using WinRT;
using System.Reflection;

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
            if (_viewModel.SelectedItem != null)
                _viewModel.SelectedItem.SoloSerialize();
        }

        private void EnableButtons()
        {
            SaveSoloSelected.IsEnabled = true;
            if (MainDataGrid.SelectedCells.Count > 0)
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
    }
}
