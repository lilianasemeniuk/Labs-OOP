using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lab1
{
    /// <summary>
    /// Interaction logic for SliderDialog.xaml
    /// </summary>
    public partial class SliderDialog : BaseDialogWindow
    {
        public SliderDialog()
        {
            InitializeComponent();
        }

        private void SliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (TxtValue != null)
            {
                int value = (int)e.NewValue;
                TxtValue.Text = value.ToString();
            }
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            DataManager.LastResult = $"Вибране значення: {TxtValue.Text}";
            this.Close();
        }

        public override string GetDialogResult()
        {
            return DataManager.LastResult;
        }
    }
}
