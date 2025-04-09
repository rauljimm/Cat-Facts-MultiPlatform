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
        public async Task DeleteBreed(Breed breed)
        {
            if (breed == null) return;

            bool isConfirmed = await Application.Current.MainPage.DisplayAlert(
                "Confirm Delete",
                $"Are you sure you want to delete the breed \"{breed.BreedName}\"?",
                "Yes",
                "No"
            );

            if (!isConfirmed) return;

            try
            {
                await _databaseService.DeleteBreedAsync(breed);
                Breeds.Remove(breed);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Could not delete the breed: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        public async Task DeleteAllBreeds()
        {
            bool isConfirmed = await Application.Current.MainPage.DisplayAlert(
                "Confirm Delete All",
                "Are you sure you want to delete all breeds?",
                "Yes",
                "No"
            );

            if (!isConfirmed) return;

            try
            {
                foreach (var breed in Breeds.ToList())
                {
                    await _databaseService.DeleteBreedAsync(breed);
                }
                Breeds.Clear();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Could not delete all breeds: {ex.Message}", "OK");
            }
        }


        [RelayCommand]
        private async Task NavigateToHomePage()
        {
            Console.WriteLine("NavigateToCreateCatPage ejecutado desde MainViewModel.");
            await _navigationService.NavigateToAsync<MainPage>();
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
                        Breeds[index] = updatedBreed;
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