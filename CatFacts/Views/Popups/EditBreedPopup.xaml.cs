using CatFacts.Models;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;

namespace CatFacts.Views.Popups;

public partial class EditBreedPopup : Popup
{
    public Breed Breed { get; }

    public EditBreedPopup(Breed breed)
    {
        InitializeComponent();
        Breed = new Breed
        {
            Id = breed.Id,
            BreedName = breed.BreedName,
            Country = breed.Country,
            Origin = breed.Origin,
            Coat = breed.Coat,
            Pattern = breed.Pattern
        };
        BindingContext = Breed;
    }

    private void OnSaveClicked(object sender, EventArgs e)
    {
        Close(Breed); // Devuelve la raza modificada
    }

    private void OnCancelClicked(object sender, EventArgs e)
    {
        Close(null); // Cierra sin cambios
    }
}