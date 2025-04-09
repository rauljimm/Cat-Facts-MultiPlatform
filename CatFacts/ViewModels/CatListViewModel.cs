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
        public async Task DeleteAllCats()
        {
            try
            {
                if (Cats != null && Cats.Any())
                {
                    foreach (var cat in Cats.ToList()) // Usamos ToList() para evitar problemas al modificar la colección mientras iteramos
                    {
                        await _catService.DeleteCatAsync(cat);
                    }
                    Cats.Clear(); // Limpiamos la colección, la UI se actualizará automáticamente
                    MessagingCenter.Send(this, "CatsDeleted"); // Notify the UI
                }
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
                    await _catService.DeleteCatAsync(cat); // Elimina el gato de la base de datos
                    Cats.Remove(cat); // Elimina el gato de la colección, la UI se actualizará automáticamente
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