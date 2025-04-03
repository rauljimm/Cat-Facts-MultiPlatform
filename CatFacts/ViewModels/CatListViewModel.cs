using CatFacts.Models;
using CatFacts.Services;
using CatFacts.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
            Cats = catsFromDatabase;
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
                foreach (var cat in Cats.ToList())
                {
                    await _catService.DeleteCatAsync(cat);
                }
                Cats.Clear();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"No se pudo eliminar todos los gatos: {ex.Message}", "OK");
            }
        }
    }
}
