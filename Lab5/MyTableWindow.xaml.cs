using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lab5
{
   public partial class MyTableWindow : Window
    {
        public class TableRowItem
        {
            public string Name { get; set; }
            public int X1 { get; set; }
            public int Y1 { get; set; }
            public int X2 { get; set; }
            public int Y2 { get; set; }
        }

        private ObservableCollection<TableRowItem> items= new ObservableCollection<TableRowItem>();

        public event Action<int> OnRowSelected;
        public event Action<int> OnRowDeleted;

        public MyTableWindow()
        {
            InitializeComponent();
            ObjectsGrid.ItemsSource = items;
        }

        public void AddRow(string name, double x1, double y1, double x2, double y2)
        {
            items.Add(new TableRowItem
            {
                Name = name,
                X1 = (int)Math.Round(x1),
                Y1 = (int)Math.Round(y1),
                X2 = (int)Math.Round(x2),
                Y2 = (int)Math.Round(y2)
            });
        }

        public void ClearTable()
        {
            items.Clear();
        }

        private void ObjectsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OnRowSelected?.Invoke(ObjectsGrid.SelectedIndex);
        }

        private void DeleteSelectedRow()
        {
            int index = ObjectsGrid.SelectedIndex;
            if (index >= 0 && index < items.Count)
            {
                items.RemoveAt(index);
                OnRowDeleted?.Invoke(index);
            }
        }

        private void  DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteSelectedRow();
        }


        private void ObjectsGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
              if (e.Key == Key.Delete)
              {
                DeleteSelectedRow();
                e.Handled = true;
              }
        }
    }
}
