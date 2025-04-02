using CatFacts.ViewModels;

namespace CatFacts.Views
{
    public partial class CatFactPage : ContentPage
    {
        public CatFactPage(CatFactViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            Console.WriteLine("CatFactPage inicializada.");
        }
    }
}