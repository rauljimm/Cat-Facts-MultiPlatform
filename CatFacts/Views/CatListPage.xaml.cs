using CatFacts.ViewModels;
using CatFacts.Views.Popups;
using CatFacts.Services;
using Microsoft.Maui.Controls;
using System;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using CatFacts.Models;
using CommunityToolkit.Mvvm.Input;

namespace CatFacts.Views
{
    public partial class CatListPage : ContentPage
    {
        private readonly IDatabaseService _databaseService;
        private readonly INavigationService _navigationService;

        public CatListPage(CatListViewModel viewModel, IDatabaseService databaseService, INavigationService navigationService)
        {
            InitializeComponent();

            if (viewModel == null)
            {
                throw new ArgumentNullException(nameof(viewModel), "ViewModel cannot be null.");
            }

            BindingContext = viewModel;
            _databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService), "El servicio de base de datos no puede ser null.");
            _navigationService = navigationService;

            // Suscribirse a los mensajes del ViewModel
            MessagingCenter.Subscribe<CatListViewModel>(this, "CatsDeleted", (sender) =>
            {
                RefreshCatGrid(); // Refresca el Grid cuando se borran todos los gatos
            });

            MessagingCenter.Subscribe<CatListViewModel>(this, "CatDeleted", (sender) =>
            {
                RefreshCatGrid(); // Refresca el Grid cuando se borra un gato individual
            });
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext == null)
            {
                await DisplayAlert("Error", "The BindingContext is not initialized.", "OK");
                return;
            }

            await ((CatListViewModel)BindingContext).LoadCatsAsync();
            RefreshCatGrid(); // Refresca el Grid al aparecer la página
        }

        private void RefreshCatGrid()
        {
            if (CatGrid == null)
            {
                DisplayAlert("Error", "CatGrid is not initialized in the XAML.", "OK");
                return;
            }

            CatGrid.Children.Clear();
            CatGrid.RowDefinitions.Clear();

            var viewModel = (CatListViewModel)BindingContext;
            if (viewModel.Cats != null && viewModel.Cats.Any())
            {
                int totalCats = viewModel.Cats.Count;
                for (int i = 0; i < totalCats; i++)
                {
                    CatGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                }

                int row = 0;
                foreach (var cat in viewModel.Cats)
                {
                    if (cat == null) continue;

                    var catFrame = new Frame
                    {
                        Style = (Style)Resources["CatFrameStyle"]
                    };

                    var horizontalLayout = new HorizontalStackLayout
                    {
                        Spacing = 10,
                        VerticalOptions = LayoutOptions.Center
                    };

                    var catImage = new Image
                    {
                        Source = "cat_view.jpg",
                        Style = (Style)Resources["CatImageStyle"]
                    };

                    var detailsLayout = new VerticalStackLayout
                    {
                        Spacing = 5,
                        VerticalOptions = LayoutOptions.Center
                    };

                    var nameLabel = new Label
                    {
                        Text = cat.Name ?? "No name",
                        FontSize = 16,
                        FontAttributes = FontAttributes.Bold,
                        TextColor = Color.FromHex("#6200EE")
                    };

                    var breedLabel = new Label { Text = $"Breed: {cat.Breed ?? "Unknown"}", Style = (Style)Resources["CatTextStyle"] };
                    var colorLabel = new Label { Text = $"Pattern: {cat.Color ?? "Unknown"}", Style = (Style)Resources["CatTextStyle"] };
                    var ageLabel = new Label { Text = $"Age: {(cat.Age > 0 ? cat.Age.ToString() : "Unknown")}", Style = (Style)Resources["CatTextStyle"] };

                    detailsLayout.Children.Add(nameLabel);
                    detailsLayout.Children.Add(breedLabel);
                    detailsLayout.Children.Add(colorLabel);
                    detailsLayout.Children.Add(ageLabel);

                    // Botón de eliminar
                    var deleteButton = new Button
                    {
                        Text = "Eliminar",
                        BackgroundColor = Color.FromHex("#FF5555"), // Rojo para indicar eliminación
                        TextColor = Colors.White,
                        FontSize = 12,
                        Padding = 5,
                        CornerRadius = 5,
                        WidthRequest = 80,
                        HeightRequest = 30,
                        VerticalOptions = LayoutOptions.Center,
                        Command = ((CatListViewModel)BindingContext).DeleteCatCommand,
                        CommandParameter = cat
                    };

                    horizontalLayout.Children.Add(catImage);
                    horizontalLayout.Children.Add(detailsLayout);
                    horizontalLayout.Children.Add(deleteButton); // Añadimos el botón al layout

                    catFrame.Content = horizontalLayout;

                    var tapGestureRecognizer = new TapGestureRecognizer
                    {
                        NumberOfTapsRequired = 2
                    };
                    tapGestureRecognizer.Tapped += async (s, e) => await EditCat(cat);
                    catFrame.GestureRecognizers.Add(tapGestureRecognizer);

                    CatGrid.Children.Add(catFrame);
                    Grid.SetRow(catFrame, row);
                    Grid.SetColumn(catFrame, 0);

                    row++;
                }
            }
        }

        private async Task EditCat(Cat cat)
        {
            if (cat == null)
            {
                await DisplayAlert("Error", "The selected cat is null.", "OK");
                return;
            }

            var popup = new EditCatPopup(cat);
            var result = await this.ShowPopupAsync(popup);

            if (result is Cat updatedCat)
            {
                await _databaseService.SaveCatAsync(updatedCat);

                var cats = ((CatListViewModel)BindingContext).Cats;
                var index = cats.IndexOf(cat);
                if (index != -1)
                {
                    cats[index] = updatedCat;
                }

                await ((CatListViewModel)BindingContext).LoadCatsAsync();
                RefreshCatGrid(); // Refresca el Grid después de editar
            }
        }
    }
}