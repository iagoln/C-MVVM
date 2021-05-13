using System;
using System.Linq;
using System.Windows;

namespace WPFMVVMCRUD
{

    public partial class AcoesWindow : Window
    {
        public AcoesWindow()
        {
            InitializeComponent();
           
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
