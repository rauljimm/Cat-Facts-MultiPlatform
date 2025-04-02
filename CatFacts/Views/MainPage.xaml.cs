using CatFacts.ViewModels;

namespace CatFacts
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
            Console.WriteLine("MainPage inicializada.");
        }
    }
}