using CatFacts.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CatFacts
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            ServiceProvider = serviceProvider;
            MainPage = new NavigationPage(serviceProvider.GetRequiredService<MainPage>());
        }
    }
}