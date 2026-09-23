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
    /// Interaction logic for Step2Dialog.xaml
    /// </summary>
    public partial class Step2Dialog : BaseDialogWindow
    {
        public Step2Dialog()
        {
            InitializeComponent();
        }

        private void BtnBackClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void BtnYesClick(object sender, RoutedEventArgs e)
        {
            DataManager.LastResult = "Обрано: \"Так\" ";

            this.DialogResult = true;
            this.Close();
        }

        private void BtnCancelClick(object sender, RoutedEventArgs e)
        {
            DataManager.LastResult = "Обрано: \"Відхилити\" ";
            this.DialogResult = null;
            this.Close();
        }

        public override string GetDialogResult()
        {
            return DataManager.LastResult;
        }
    }
}
