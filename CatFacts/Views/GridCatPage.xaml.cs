using CatFacts.ViewModels;

namespace CatFacts.Views;

public partial class GridCatPage : ContentPage
{
	public GridCatPage(CatListViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		vm.LoadCatsAsync();
    }
}