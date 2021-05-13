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
           
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
