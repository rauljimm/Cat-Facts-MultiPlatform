using CatFacts.Models;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;

namespace CatFacts.Views.Popups;

public partial class EditCatFactPopup : Popup
{
    public CatFact CatFact { get; }

    public EditCatFactPopup(CatFact catFact)
    {
        InitializeComponent();
        CatFact = new CatFact { Id = catFact.Id, Fact = catFact.Fact, Length = catFact.Length };
        BindingContext = CatFact;
    }

    private void OnSaveClicked(object sender, EventArgs e)
    {
        CatFact.Length = CatFact.Fact.Length; 
        Close(CatFact); 
    }

    private void OnCancelClicked(object sender, EventArgs e)
    {
        Close(null);
    }
}