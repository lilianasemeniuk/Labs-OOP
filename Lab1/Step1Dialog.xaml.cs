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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Step1Dialog : BaseDialogWindow
    {
        public Step1Dialog()
        {
            InitializeComponent();
        }

        private void BtnNextClick(object sender, RoutedEventArgs e)
        {
            this.Hide();

            Step2Dialog step2 = new Step2Dialog();
            step2.Owner = this.Owner;

            bool? result = step2.ShowDialog();

            if (result == true)
            {
                this.DialogResult = true;
                this.Close();
            }
            else if (result == false)
            {
                this.Show();
            }
            else
            {
                this.Close();
            }
        }

        private void BtnCancelClick(object seneder, RoutedEventArgs e)
        {
            this.Close();
        }

        public override string GetDialogResult()
        {
            return DataManager.LastResult;
        }
    }
}
