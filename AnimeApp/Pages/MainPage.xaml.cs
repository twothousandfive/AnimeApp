using AnimeApp.Models;
using AnimeApp.Services;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AnimeApp.Pages;

public partial class MainPage : ContentPage
{
    private ObservableCollection<AnimeContent> _allAnime = new();
    public MainPage()
	{
		InitializeComponent();
        
        // добавляем обработчик нажатий
        AnimeCollectionView.SelectionChanged += OnAnimeSelected;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var allAnime = await App.Database.GetAllAnimeAsync();
        _allAnime = new ObservableCollection<AnimeContent>(allAnime);
        AnimeCollectionView.ItemsSource = _allAnime;
    }

    private async void OnAnimeSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is AnimeContent selectedAnime)
        {
            await Navigation.PushAsync(new AnimeDetailsPage(selectedAnime));
            ((CollectionView)sender).SelectedItem = null; // убираем выделение
        }
    }

    private async void OnAnimeTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is AnimeContent tappedAnime && sender is Frame frame)
        {
            await frame.FadeTo(0.6, 100); // затемнение
            await frame.FadeTo(1.0, 100); // вернуть норм

            await Navigation.PushAsync(new AnimeDetailsPage(tappedAnime));
        }
    }

    private void OnGenreFilterChanged(object sender, EventArgs e)
    {
        var selectedGenre = GenreFilterPicker.SelectedItem?.ToString();
        if (selectedGenre == null || selectedGenre == "Все жанры")
        {
            AnimeCollectionView.ItemsSource = _allAnime;
        }
        else
        {
            var filtered = _allAnime.Where(a => a.Genre == selectedGenre).ToList();
            AnimeCollectionView.ItemsSource = filtered;
        }
    }

    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        var searchText = e.NewTextValue?.ToLower() ?? "";
        AnimeCollectionView.ItemsSource = _allAnime
            .Where(a => a.Title.ToLower().Contains(searchText))
            .ToList();
    }

    private async void OnFavoriteTapped(object sender, TappedEventArgs e)
    {
        if (e.Parameter is AnimeContent anime)
        {
            // Анимация нажатия
            if (sender is Image heartImage)
            {
                await heartImage.ScaleTo(1.2, 100); // Увеличить
                await heartImage.ScaleTo(1.0, 100); // Вернуть размер
            }

            anime.IsFavorite = !anime.IsFavorite;
            await App.Database.SaveAnimeAsync(anime); // обязательно должен быть метод SaveAnimeAsync в базе!

            // Обновляем отображение
            AnimeCollectionView.ItemsSource = null;
            AnimeCollectionView.ItemsSource = _allAnime;
        }
    }
}