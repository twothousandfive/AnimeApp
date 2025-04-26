using AnimeApp.Models;
using AnimeApp.Services;
using System.Collections.ObjectModel;

namespace AnimeApp.Pages;

public partial class ProfilePage : ContentPage
{
    private User _currentUser;

    public ProfilePage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var email = Preferences.Get("user_email", null);
        if (email == null)
        {
            await DisplayAlert("Ошибка", "Вы не авторизованы", "Ок");
            await Navigation.PopToRootAsync();
            return;
        }

        _currentUser = await App.Database.GetUserByEmailAsync(email);
        if (_currentUser == null)
        {
            await DisplayAlert("Ошибка", "Пользователь не найден", "Ок");
            return;
        }

        EmailLabel.Text = _currentUser.Email;
        BirthDateLabel.Text = $"Дата рождения: {_currentUser.BirthDate:dd.MM.yyyy}";
        ProfilePhoto.Source = string.IsNullOrWhiteSpace(_currentUser.ProfilePhotoPath)
            ? "default_profile.png"
            : ImageSource.FromFile(_currentUser.ProfilePhotoPath);

        var myAnime = await App.Database.GetAnimeByUserIdAsync(_currentUser.Id);
        AnimeList.ItemsSource = myAnime;

        // Загружаем избранные аниме
        var favoriteAnime = myAnime.Where(a => a.IsFavorite).ToList();
        FavoriteAnimeList.ItemsSource = favoriteAnime;
    }

    private async void OnAddAnimeClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new UploadAnimePage(_currentUser.Id));
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        Preferences.Remove("user_email");
        await Navigation.PushAsync(new LoginPage());
    }
}