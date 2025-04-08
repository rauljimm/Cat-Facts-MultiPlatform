# 🐱 Cat Facts MultiPlatform

![.NET MAUI](https://img.shields.io/badge/.NET%20MAUI-Framework-blueviolet)
![C#](https://img.shields.io/badge/C%23-Language-blue)
![License: MIT](https://img.shields.io/badge/License-MIT-green)

**Cat Facts MultiPlatform** is a cross-platform application built with **.NET MAUI** and **C#** that retrieves information about cats and their breeds. The app is designed to run seamlessly on **iOS**, **Android**, **Windows**, **macOS**, and **Linux**.


![CatFacts Image](https://github.com/rauljimm/CatFacts/CatFacts/Resources/Images/cat_view.jpg)

## ✨ Features

- **Cross-Platform Compatibility**: Deployable on iOS, Android, Windows, macOS, and Linux using a single codebase.
- **Cat Facts and Breeds**: Fetches and displays a variety of interesting facts about cats and detailed information about different cat breeds.
- **Create and View Cats**: Allows users to create new cat entries and view them.
- **SQLite Database**: Stores all cat information in a SQLite database for persistent storage.
- **Responsive UI**: Utilizes .NET MAUI's capabilities to deliver a consistent and adaptive user interface across platforms.

## 🚀 Installation & Setup

### Prerequisites

- **.NET SDK**: Ensure you have the latest version of the [.NET SDK](https://dotnet.microsoft.com/download/dotnet) installed.
- **.NET MAUI Workload**: Install the .NET MAUI workload by running:
  ```bash
  dotnet workload install maui
  ```

### Clone the Repository

Clone the repository to your local machine using:
```bash
git clone https://github.com/rauljimm/Cat-Facts-MultiPlatform.git
```

### Build and Run

Navigate to the project directory and build the application:
```bash
cd Cat-Facts-MultiPlatform
dotnet build
```

Run the application on your desired platform:
```bash
dotnet run --framework net6.0-android
dotnet run --framework net6.0-ios
dotnet run --framework net6.0-maccatalyst
dotnet run --framework net6.0-windows
dotnet run --framework net6.0-linux
```

## 📜 License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

## 🤝 Contributing

Contributions are welcome! Feel free to submit a pull request or open an issue to improve this project.

## 📧 Contact

For any inquiries or questions, please contact [rauljimm](https://github.com/rauljimm).

---

Happy coding!
