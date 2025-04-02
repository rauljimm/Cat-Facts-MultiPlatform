using CatFacts.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CatFacts.Views;

namespace CatFacts.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;

        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            Console.WriteLine("MainViewModel inicializado.");
        }

        [RelayCommand]
        private async Task NavigateToBreeds()
        {
            Console.WriteLine("NavigateToBreedsCommand ejecutado.");
            await _navigationService.NavigateToAsync<BreedPage>();
        }

        [RelayCommand]
        private async Task NavigateToCatFacts()
        {
            Console.WriteLine("NavigateToCatFactsCommand ejecutado.");
            await _navigationService.NavigateToAsync<CatFactPage>();
        }
    }
}