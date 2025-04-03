using CatFacts.Services;
using CatFacts.ViewModels;
using CatFacts.Views;
using CatFacts.Views.Popups;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace CatFacts;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .UseMauiCommunityToolkit();

        // Registro de servicios
        builder.Services.AddHttpClient();
        builder.Services.AddSingleton<ICatFactService, CatFactService>();
        builder.Services.AddSingleton<ICatService, CatService>();
        builder.Services.AddSingleton<IBreedService, BreedService>();
        builder.Services.AddSingleton<INavigationService, NavigationService>();
        builder.Services.AddSingleton<IDatabaseService>(provider =>
            new DatabaseService(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "catfacts.db3")));

        // Registro de ViewModels
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddSingleton<CatFactViewModel>();
        builder.Services.AddSingleton<BreedViewModel>();
        builder.Services.AddSingleton<CatViewModel>();
        builder.Services.AddSingleton<CatListViewModel>();

        // Registro de páginas    
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<CatFactPage>();
        builder.Services.AddTransient<BreedPage>();
        builder.Services.AddTransient<CreateCatPage>();
        builder.Services.AddTransient<CatListPage>();
        builder.Services.AddSingleton<EditCatPopup>();
        builder.Services.AddSingleton<AppShell>();

        // Hacer IServiceProvider disponible
        builder.Services.AddSingleton<IServiceProvider>(provider => provider);

        return builder.Build();
    }
}