using AnimeApp.Models;

namespace AnimeApp.Pages;

public partial class AnimeDetailsPage : ContentPage
{
    private AnimeContent _anime;
    public AnimeDetailsPage(AnimeContent anime)
	{
    InitializeComponent();
    _anime = anime;
    LoadAnimeDetails();
    }

    private void LoadAnimeDetails()
    {
        CoverImage.Source = _anime.CoverImagePath;
        TitleLabel.Text = _anime.Title;
        DescriptionLabel.Text = _anime.Description;

        if (!string.IsNullOrEmpty(_anime.VideoPath))
        {
            VideoPlayer.Source = _anime.VideoPath;
        }
    }
}