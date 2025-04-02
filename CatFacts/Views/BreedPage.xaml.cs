using CatFacts.ViewModels;

namespace CatFacts.Views
{
    public partial class BreedPage : ContentPage
    {
        public BreedPage(BreedViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            Console.WriteLine("BreedPage inicializada.");
        }
    }
}