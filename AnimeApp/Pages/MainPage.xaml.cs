using AnimeApp.Models;
using AnimeApp.Services;
using System.Collections.ObjectModel;

namespace AnimeApp.Pages;

public partial class MainPage : ContentPage
{
    private ObservableCollection<AnimeContent> _allAnime = new();
    public MainPage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var allAnime = await App.Database.GetAllAnimeAsync();
        _allAnime = new ObservableCollection<AnimeContent>(allAnime);
        AnimeCollectionView.ItemsSource = _allAnime;
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
}