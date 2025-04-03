using Microsoft.Maui.Controls;
using CatFacts.ViewModels;

namespace CatFacts;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

}