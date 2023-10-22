using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Controls;
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

        public static void DisplayObjectProperties(object obj, StackPanel stackPanel)
        {
            if (obj == null)
            {
                return;
            }

            Type objectType = obj.GetType();
            PropertyInfo[] properties = objectType.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                object propertyValue = property.GetValue(obj);
                string propertyName = property.Name;

                if (propertyValue != null)
                {
                    if (property.PropertyType.IsPrimitive || property.PropertyType == typeof(string))
                    {
                        // Это примитивное значение или строка, выводим
                        string value = propertyValue.ToString();
                        if (!string.IsNullOrEmpty(value))
                        {
                            stackPanel.Children.Add(new Label { Content = propertyName });
                            stackPanel.Children.Add(new TextBlock { Text = value });
                        }
                    }
                    else if (propertyValue is System.Collections.IEnumerable enumerable)
                    {
                        // Если это коллекция, добавляем в ListBox
                        ListBox listBox = new ListBox();
                        foreach (var item in enumerable)
                        {
                            if (item is Ordinate ordinate)
                            {
                                // Если элемент - Ordinate, используем метод GetString()
                                listBox.Items.Add(ordinate.GetString());
                            }
                            else if (item is SpatialElement spatialElement)
                            {
                                DisplayObjectProperties(spatialElement, stackPanel);
                            }
                            else if (item is Contour contour)
                            {
                                DisplayObjectProperties(contour, stackPanel);
                            }
                            else
                            {
                                listBox.Items.Add(item.ToString());
                            }
                        }
                        //stackPanel.Children.Add(new Label { Content = propertyName });
                        stackPanel.Children.Add(listBox);
                    }
                    else
                    {
                        // Обходим вложенные объекты
                        //stackPanel.Children.Add(new Label { Content = propertyName });
                        DisplayObjectProperties(propertyValue, stackPanel);
                    }
                }
            }
        }
        //public static void DisplayObjectProperties(object obj, StackPanel stackPanel, int depth = 0)
        //{
        //    if (obj == null || depth >= 8)
        //    {
        //        return;
        //    }

        //    Type objectType = obj.GetType();
        //    PropertyInfo[] properties = objectType.GetProperties();

        //    foreach (PropertyInfo property in properties)
        //    {
        //        object propertyValue = property.GetValue(obj);
        //        string propertyName = property.Name;

        //        if (propertyValue != null)
        //        {
        //            if (property.PropertyType == typeof(DateTime))
        //            {
        //                // Если это свойство типа DateTime, выводим его как строку
        //                string formattedDate = ((DateTime)propertyValue).ToString("yyyy-MM-dd HH:mm:ss");
        //                stackPanel.Children.Add(new Label { Content = propertyName });
        //                stackPanel.Children.Add(new TextBlock { Text = formattedDate });
        //            }
        //            else if (property.PropertyType.IsPrimitive || property.PropertyType == typeof(string))
        //            {
        //                // Это примитивное значение или строка, выводим
        //                string value = propertyValue.ToString();
        //                if (!string.IsNullOrEmpty(value))
        //                {
        //                    stackPanel.Children.Add(new Label { Content = propertyName });
        //                    stackPanel.Children.Add(new TextBlock { Text = value });
        //                }
        //            }
        //            else if (propertyValue is System.Collections.IEnumerable enumerable)
        //            {
        //                if (depth == 5 && property.Name == "Ordinates")
        //                {
        //                    // Максимальная глубина достигнута, вывести содержимое List<Ordinates>
        //                    ListBox listBox = new ListBox();
        //                    foreach (var item in enumerable)
        //                    {
        //                        if (item is Ordinate ordinate)
        //                        {
        //                            listBox.Items.Add(ordinate.GetString());
        //                        }
        //                    }
        //                    stackPanel.Children.Add(new Label { Content = propertyName });
        //                    stackPanel.Children.Add(listBox);
        //                }
        //                else
        //                {
        //                    // Продолжаем обход для других коллекций
        //                    foreach (var item in enumerable)
        //                    {
        //                        DisplayObjectProperties(item, stackPanel, depth + 1);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                // Обходим вложенные объекты
        //                stackPanel.Children.Add(new Label { Content = propertyName });
        //                DisplayObjectProperties(propertyValue, stackPanel, depth + 1);
        //            }
        //        }
        //    }
        //}
    }

}