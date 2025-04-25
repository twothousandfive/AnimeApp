using AnimeApp.Services;
using System.Security.Cryptography;
using System.Text;

namespace AnimeApp.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var email = EmailEntry.Text;
        var password = PasswordEntry.Text;

        var user = await App.Database.GetUserByEmailAsync(email);
        if (user == null || user.PasswordHash != HashPassword(password))
        {
            await DisplayAlert("Ошибка", "Неверный логин или пароль", "Ок");
            return;
        }

        Preferences.Set("user_email", email); // сохранить сессию
        await Navigation.PushAsync(new MainPage());
    }

    private async void OnRegisterTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }
}