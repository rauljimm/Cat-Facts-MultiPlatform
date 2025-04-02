Cat Facts Multiplatform

Welcome to Cat Facts Multiplatform, a cross-platform application built with .NET MAUI that brings you fascinating facts about cats and detailed information about cat breeds. Whether you're a cat enthusiast or just curious, this app lets you explore, save, and manage cat facts and breeds with a sleek, user-friendly interface.
Features

    Cat Facts: Fetch and display random cat facts from an external API.
    Cat Breeds: Explore a list of cat breeds with details like origin, coat, and pattern.
    Local Storage: Save your favorite facts and breeds to a local SQLite database.
    Cross-Platform: Runs seamlessly on Android, iOS, Windows, and macOS (thanks to .NET MAUI).
    MVVM Architecture: Built with a clean Model-View-ViewModel structure for maintainability.
    Navigation: Easily switch between the main page, cat facts, and breed pages.

Screenshots
Main Page	Cat Facts	Cat Breeds
Main Page	Cat Facts	Breeds
<!-- Replace with actual screenshots if you have them -->
Getting Started
Prerequisites

    .NET 8 SDK
    Visual Studio 2022 (with .NET MAUI workload installed)
    Git (to clone the repository)
    An emulator or physical device for testing (Android, iOS, Windows, or macOS)

Installation

    Clone the Repository:
    bash

git clone https://github.com/rauljimm/Cat-Facts-MultiPlatform.git
cd Cat-Facts-MultiPlatform
Restore Dependencies: Open the solution (CatFacts.sln) in Visual Studio and restore NuGet packages:
bash

    dotnet restore
    Build and Run:
        Set the target platform (e.g., Android emulator, iOS simulator, or Windows).
        Press F5 to build and run the app.

Configuration

    API: The app uses the Cat Fact API and Cat Breeds API. No API key is required.
    Database: SQLite is configured to store data locally at Environment.SpecialFolder.LocalApplicationData/catfacts.db3.

Project Structure
text
Cat-Facts-MultiPlatform/
├── Models/              # Data models (CatFact, Breed)
├── Services/            # Services (API calls, database, navigation)
├── ViewModels/          # ViewModels for MVVM pattern
├── Views/               # UI pages (MainPage, CatFactPage, BreedPage)
├── Resources/           # Images and other assets
├── App.xaml             # Application entry point
├── MauiProgram.cs       # Dependency injection setup
└── CatFacts.sln         # Solution file
Usage

    Main Page: Start here to navigate to either "Cat Facts" or "Cat Breeds".
    Cat Facts Page: Click "Get New Cat Fact" to fetch a random fact. Use "Eliminar" to delete saved facts.
    Cat Breeds Page: Click "Get New Breed" to add a breed to the list. Use "Eliminar" to remove breeds.
    Navigation: Use the "Go to" buttons to switch between pages.

Dependencies

    CommunityToolkit.Mvvm: For MVVM support (RelayCommand, ObservableObject).
    sqlite-net-pcl: For local database storage.
    Microsoft.Maui.Controls: Core .NET MAUI framework.

Install these via NuGet if needed:
bash
dotnet add package CommunityToolkit.Mvvm
dotnet add package sqlite-net-pcl
dotnet add package Microsoft.Maui.Controls
Contributing

Contributions are welcome! To contribute:

    Fork the repository.
    Create a feature branch:
    bash

git checkout -b feature/your-feature-name
Commit your changes:
bash
git commit -m "Add your feature"
Push to your branch:
bash

    git push origin feature/your-feature-name
    Open a Pull Request.

Please ensure your code follows the existing style and includes unit tests if applicable.
License

This project is licensed under the . See the LICENSE file for details.
Acknowledgments

    Thanks to CatFact.Ninja for providing the APIs.
    Built with ❤️ by rauljimm.

Contact

For questions or feedback, reach out to me at rauljimm.dev@gmail.com or open an issue on GitHub.
