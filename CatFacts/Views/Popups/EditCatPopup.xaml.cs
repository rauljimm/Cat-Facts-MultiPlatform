using CatFacts.Models;
using CommunityToolkit.Maui.Views;

namespace CatFacts.Views.Popups
{
    public partial class EditCatPopup : Popup
    {
        public Cat Cat { get; }

        public EditCatPopup(Cat cat)
        {
            InitializeComponent();
            Cat = new Cat
            {
                Id = cat.Id,
                Name = cat.Name,
                Breed = cat.Breed,
                Color = cat.Color,
                Age = cat.Age
            };
            BindingContext = Cat;
        }

        private void OnSaveClicked(object sender, EventArgs e)
        {
            Close(Cat);
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            Close(null);
        }
    }
}