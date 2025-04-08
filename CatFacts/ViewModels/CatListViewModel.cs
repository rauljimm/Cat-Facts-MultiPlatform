using CatFacts.Models;
using CatFacts.Services;
using CatFacts.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatFacts.ViewModels
{
    public partial class CatListViewModel : ObservableObject
    {
        private readonly ICatService _catService;
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private List<Cat> cats;

        public CatListViewModel(ICatService catService, INavigationService navigationService)
        {
            _catService = catService;
            _navigationService = navigationService;
        }

        public async Task LoadCatsAsync()
        {
            var catsFromDatabase = await _catService.GetCatsAsync();
            Cats = catsFromDatabase; // Asigna la lista cargada
        }

        [RelayCommand]
        private async Task NavigateToHomePage()
        {
            Console.WriteLine("NavigateToCreateCatPage ejecutado desde MainViewModel.");
            await _navigationService.NavigateToAsync<MainPage>();
        }

        [RelayCommand]
        public async Task DeleteAllCats()
        {
            try
            {
                if (Cats != null && Cats.Any())
                {
                    foreach (var cat in Cats.ToList())
                    {
                        await _catService.DeleteCatAsync(cat);
                    }
                    Cats.Clear();
                }
                await LoadCatsAsync(); // Reload the list (should be empty)
                MessagingCenter.Send(this, "CatsDeleted"); // Notify the UI
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Could not delete all cats: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        public async Task DeleteCat(Cat cat)
        {
            try
            {
                if (cat != null)
                {
                    await _catService.DeleteCatAsync(cat); // Delete the cat from the database
                    Cats.Remove(cat); // Remove the cat from the list
                    MessagingCenter.Send(this, "CatDeleted"); // Notify the UI that a cat was deleted
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Could not delete the cat: {ex.Message}", "OK");
            }
        }
    }
}