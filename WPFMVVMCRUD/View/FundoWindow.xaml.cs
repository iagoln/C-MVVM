using System;
using System.Linq;
using System.Windows;

namespace WPFMVVMCRUD
{

    public partial class FundoWindow : Window
    {
        public FundoWindow()
        {
            InitializeComponent();
            //TipoComboBox.ItemsSource = Enum.GetValues(typeof(Model.Type)).Cast<Model.Type>();
           
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
