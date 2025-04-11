using CatFacts.Models;
using CatFacts.Services;
using CatFacts.Views.Popups;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CatFacts.Views;

namespace CatFacts.ViewModels
{
    public partial class CatFactViewModel : ObservableObject
    {
        private readonly ICatFactService _catFactService;
        private readonly IDatabaseService _databaseService;
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private ObservableCollection<CatFact> catFacts = new();

        public CatFactViewModel(ICatFactService catFactService, IDatabaseService databaseService, INavigationService navigationService)
        {
            _catFactService = catFactService;
            _databaseService = databaseService;
            _navigationService = navigationService;
            LoadCatFactsAsync();
        }

        private async void LoadCatFactsAsync()
        {
            var facts = await _databaseService.GetCatFactsAsync();
            foreach (var fact in facts)
            {
                CatFacts.Add(fact);
            }
        }

        [RelayCommand]
        private async Task GetCatFact()
        {
            try
            {
                var catFact = await _catFactService.GetCatFactAsync();
                if (catFact != null && !string.IsNullOrEmpty(catFact.Fact))
                {
                    await _databaseService.SaveCatFactAsync(catFact);
                    CatFacts.Add(catFact);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"No se pudo obtener el CatFact: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        private async Task DeleteCatFact(CatFact catFact)
        {
            if (catFact == null) return;

            bool isConfirmed = await Application.Current.MainPage.DisplayAlert(
                "Confirm Delete",
                $"Are you sure you want to delete this cat fact?",
                "Yes",
                "No"
            );

            if (!isConfirmed) return;

            try
            {
                await _databaseService.DeleteCatFactAsync(catFact);
                CatFacts.Remove(catFact);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Could not delete the cat fact: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        private async Task DeleteAllCatFact()
        {
            bool isConfirmed = await Application.Current.MainPage.DisplayAlert(
                "Confirm Delete All",
                "Are you sure you want to delete all cat facts?",
                "Yes",
                "No"
            );

            if (!isConfirmed) return;

            try
            {
                foreach (var fact in CatFacts.ToList())
                {
                    await _databaseService.DeleteCatFactAsync(fact);
                }
                CatFacts.Clear();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Could not delete all cat facts: {ex.Message}", "OK");
            }
        }


        [RelayCommand]
        private async Task NavigateToHomePage()
        {
            Console.WriteLine("NavigateToCreateCatPage ejecutado desde MainViewModel.");
            await _navigationService.NavigateToAsync<MainPage>();
        }

        [RelayCommand]
        private async Task EditCatFact(CatFact catFact)
        {
            if (catFact == null) return;

            var popup = new EditCatFactPopup(catFact);
            var result = await Application.Current.MainPage.ShowPopupAsync(popup);

            if (result is CatFact updatedCatFact)
            {
                try
                {
                    await _databaseService.SaveCatFactAsync(updatedCatFact);
                    var index = CatFacts.IndexOf(catFact);
                    if (index != -1)
                    {
                        CatFacts[index] = updatedCatFact;
                    }
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"No se pudo guardar el CatFact: {ex.Message}", "OK");
                }
            }
        }
    }
}