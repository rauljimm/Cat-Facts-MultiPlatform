using CatFacts.Models; // Necesario para Cat
using CatFacts.ViewModels;

namespace CatFacts.Views
{
    public partial class GridCatPage : ContentPage
    {
        private readonly CatListViewModel _vm;

        public GridCatPage(CatListViewModel vm)
        {
            InitializeComponent();
            _vm = vm;
            BindingContext = _vm;
            Loaded += async (s, e) => await _vm.LoadCatsAsync();
        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.BindingContext is Cat cat)
            {
                await _vm.DeleteCat(cat);
            }
        }
    }
}