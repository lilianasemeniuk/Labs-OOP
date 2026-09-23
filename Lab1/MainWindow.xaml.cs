using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItemWork1_Click(object sender, RoutedEventArgs e)
        {
            SliderDialog dialog = new SliderDialog();
            dialog.Owner = this;
            dialog.ShowDialog();
            TxtResult.Text = dialog.GetDialogResult();
        }

        private void MenuItemWork2_Click(object sender, RoutedEventArgs e)
        {
            Step1Dialog step1 = new Step1Dialog();
            step1.Owner = this;

            if (step1.ShowDialog() == true)
            {
                TxtResult.Text = step1.GetDialogResult();
            }
        }
    }
}