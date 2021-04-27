using System.Windows;

namespace WPFMVVMCRUD
{
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel.ProdutosViewModel();
        }
    }
}
