using CatFacts.Models;
using CatFacts.Services;
using CatFacts.Views.Popups;
using CommunityToolkit.Maui.Views;
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
        private async Task DeleteAllBreeds()
        {
            try
            {
                foreach (var breed in Breeds.ToList()) // Usar ToList para evitar problemas de modificación durante iteración
                {
                    await _databaseService.DeleteBreedAsync(breed);
                }
                Breeds.Clear();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"No se pudo eliminar todas las razas: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        private async Task NavigateToCatFacts()
        {
            Console.WriteLine("NavigateToCatFactsCommand ejecutado desde BreedViewModel.");
            await _navigationService.NavigateToAsync<CatFactPage>();
        }

        [RelayCommand]
        private async Task EditBreed(Breed breed)
        {
            if (breed == null) return;

            var popup = new EditBreedPopup(breed);
            var result = await Application.Current.MainPage.ShowPopupAsync(popup);

            if (result is Breed updatedBreed)
            {
                try
                {
                    await _databaseService.SaveBreedAsync(updatedBreed);
                    var index = Breeds.IndexOf(breed);
                    if (index != -1)
                    {
                        Breeds[index] = updatedBreed; // Actualiza en la colección observable
                    }
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"No se pudo guardar la raza: {ex.Message}", "OK");
                }
            }
        }
    }
}