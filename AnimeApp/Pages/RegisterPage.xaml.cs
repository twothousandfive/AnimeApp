using AnimeApp.Models;
using AnimeApp.Services;
using Microsoft.Maui.Storage;
using System.Security.Cryptography;
using System.Text;

namespace AnimeApp.Pages;

public partial class RegisterPage : ContentPage
{
    private string _selectedPhotoPath = "";

    public RegisterPage()
    {
        InitializeComponent();
    }

    private async void OnSelectPhotoClicked(object sender, EventArgs e)
    {
        var result = await FilePicker.PickAsync(new PickOptions
        {
            FileTypes = FilePickerFileType.Images
        });

        if (result != null)
        {
            _selectedPhotoPath = result.FullPath;
            ProfileImage.Source = ImageSource.FromFile(_selectedPhotoPath);
        }
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        var email = EmailEntry.Text;
        var password = PasswordEntry.Text;
        var birthDate = BirthDatePicker.Date;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Ошибка", "Заполните все поля", "Ок");
            return;
        }

        var existingUser = await App.Database.GetUserByEmailAsync(email);
        if (existingUser != null)
        {
            await DisplayAlert("Ошибка", "Пользователь уже существует", "Ок");
            return;
        }

        var newUser = new User
        {
            Email = email,
            PasswordHash = HashPassword(password),
            BirthDate = birthDate,
            ProfilePhotoPath = _selectedPhotoPath
        };

        await App.Database.RegisterUserAsync(newUser);

        Preferences.Set("user_email", email); // сохранить сессию
        await Navigation.PushAsync(new MainPage());
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }
}