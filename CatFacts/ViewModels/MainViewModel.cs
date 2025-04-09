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
            
        }

        [RelayCommand]
        private async Task NavigateToBreeds()
        {
            
            await _navigationService.NavigateToAsync<BreedPage>();
        }

        [RelayCommand]
        private async Task NavigateToCatFacts()
        {
            
            await _navigationService.NavigateToAsync<CatFactPage>();
        }

        [RelayCommand]
        private async Task NavigateToCreateCatPage()
        {
            
            await _navigationService.NavigateToAsync<CreateCatPage>();
        }

        [RelayCommand]
        private async Task NavigateToCatsPage()
        {
            
            await _navigationService.NavigateToAsync<CatListPage>();
        }

        [RelayCommand]
        private async Task NavigateToGridCatPage()
        {
            await _navigationService.NavigateToAsync<GridCatPage>();
        }

    }
}