using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CatFacts.Services;
using System.Collections.ObjectModel;
using CatFacts.Models;
using CommunityToolkit.Mvvm.Input;
using CatFacts.Views;

namespace CatFacts.ViewModels
{
    public partial class CatViewModel : ObservableObject
    {
        private readonly ICatService _catService;
        private readonly IDatabaseService _databaseService;
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private string catName;

        [ObservableProperty]
        private string catBreed;

        [ObservableProperty]
        private string catColor;

        [ObservableProperty]
        private string catAge;

        public CatViewModel(ICatService catService, IDatabaseService databaseService, INavigationService navigationService)
        {
            _catService = catService;
            _databaseService = databaseService;
            _navigationService = navigationService;
        }
    
        [ObservableProperty]
        private ObservableCollection<Cat> cats = new();

        [RelayCommand]
        private async void LoadCatsAsync()
        {
            var catsList = await _databaseService.GetCatsAsync();
            foreach (var cat in catsList)
            {
                Cats.Add(cat);
            }
        }


        [RelayCommand]
        private async Task CreateCat()
        {
            try
            {
                string name = catName;
                string breed = catBreed;
                string color = catColor;
                int age = int.Parse(catAge);

                var newCat = await _catService.CreateCatAsync(name, breed, color, age);

                Cats.Add(newCat);
                
                await Application.Current.MainPage.DisplayAlert("Success", "Cat created successfully!", "OK");

                
                await _navigationService.NavigateToAsync<CatListPage>();
            }
            catch (Exception ex)
            {
                
                await Application.Current.MainPage.DisplayAlert("Error", $"Could not create cat: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        private async Task NavigateToHomePage()
        {
            Console.WriteLine("NavigateToCreateCatPage ejecutado desde MainViewModel.");
            await _navigationService.NavigateToAsync<MainPage>();
        }


    }

}
