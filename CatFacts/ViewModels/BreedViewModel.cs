using CatFacts.Models;
using CatFacts.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using CatFacts.Views;

namespace CatFacts.ViewModels
{
    public partial class BreedViewModel : ObservableObject
    {
        private readonly IBreedService _breedService;
        private readonly IDatabaseService _databaseService;
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private ObservableCollection<Breed> breeds = new();

        public BreedViewModel(IBreedService breedService, IDatabaseService databaseService, INavigationService navigationService)
        {
            _breedService = breedService;
            _databaseService = databaseService;
            _navigationService = navigationService;
            LoadBreedsAsync();
        }

        private async void LoadBreedsAsync()
        {
            var breedsList = await _databaseService.GetBreedsAsync();
            foreach (var breed in breedsList)
            {
                Breeds.Add(breed);
            }
        }

        [RelayCommand]
        private async Task GetBreed()
        {
            try
            {
                var breedList = await _breedService.GetBreedsAsync();
                if (breedList != null && breedList.Any())
                {
                    var breedLength = breedList.LongCount();
                    Random rnd = new Random();
                    int randomIndex = rnd.Next((int)breedLength);

                    var newBreed = breedList[randomIndex]; 

                    await _databaseService.SaveBreedAsync(newBreed);
                    Breeds.Add(newBreed);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"No se pudo obtener la raza: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        private async Task DeleteBreed(Breed breed)
        {
            if (breed == null) return;
            try
            {
                await _databaseService.DeleteBreedAsync(breed);
                Breeds.Remove(breed);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"No se pudo eliminar la raza: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        private async Task NavigateToCatFacts()
        {
            Console.WriteLine("NavigateToCatFactsCommand ejecutado desde BreedViewModel.");
            await _navigationService.NavigateToAsync<CatFactPage>();
        }
    }
}