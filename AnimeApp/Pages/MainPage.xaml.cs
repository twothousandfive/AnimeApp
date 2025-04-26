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
        if (e.Parameter is AnimeContent tappedAnime)
        {
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
}