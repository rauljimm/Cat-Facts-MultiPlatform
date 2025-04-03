using CatFacts.ViewModels;

namespace CatFacts.Views;

public partial class CreateCatPage : ContentPage
{
	public CreateCatPage(CatViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}