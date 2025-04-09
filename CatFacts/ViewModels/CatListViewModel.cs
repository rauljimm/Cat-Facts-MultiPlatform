using CatFacts.Models;
using CatFacts.Services;
using CatFacts.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel; // Necesario para ObservableCollection
using System.Threading.Tasks;

namespace CatFacts.ViewModels
{
    public partial class CatListViewModel : ObservableObject
    {
        private readonly ICatService _catService;
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private ObservableCollection<Cat> cats; // Cambiado de List<Cat> a ObservableCollection<Cat>

        public CatListViewModel(ICatService catService, INavigationService navigationService)
        {
            _catService = catService;
            _navigationService = navigationService;
            Cats = new ObservableCollection<Cat>(); // Inicializamos la colección
        }

        public async Task LoadCatsAsync()
        {
            var catsFromDatabase = await _catService.GetCatsAsync();
            Cats = new ObservableCollection<Cat>(catsFromDatabase); // Asignamos una nueva colección
        }

        [RelayCommand]
        private async Task NavigateToHomePage()
        {
            Console.WriteLine("NavigateToCreateCatPage ejecutado desde MainViewModel.");
            await _navigationService.NavigateToAsync<MainPage>();
        }

        [RelayCommand]
        public async Task DeleteCat(Cat cat)
        {
            if (cat == null) return;

            bool isConfirmed = await Application.Current.MainPage.DisplayAlert(
                "Confirm Delete",
                $"Are you sure you want to delete the cat \"{cat.Name}\"?",
                "Yes",
                "No"
            );

            if (!isConfirmed) return;

            try
            {
                await _catService.DeleteCatAsync(cat);
                Cats.Remove(cat);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Could not delete the cat: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        public async Task DeleteAllCats()
        {
            bool isConfirmed = await Application.Current.MainPage.DisplayAlert(
                "Confirm Delete All",
                "Are you sure you want to delete all cats?",
                "Yes",
                "No"
            );

            if (!isConfirmed) return;

            try
            {
                foreach (var cat in Cats.ToList())
                {
                    await _catService.DeleteCatAsync(cat);
                }
                Cats.Clear();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Could not delete all cats: {ex.Message}", "OK");
            }
        }
    }
}