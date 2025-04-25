using AnimeApp.Models;
using AnimeApp.Services;

namespace AnimeApp.Pages;

public partial class UploadAnimePage : ContentPage
{
    private readonly int _userId;
    private string _coverImagePath = "";
    private string _videoPath = "";

    public UploadAnimePage(int userId)
	{
		InitializeComponent();
        _userId = userId;
    }

    private async void OnPickCoverClicked(object sender, EventArgs e)
    {
        var result = await FilePicker.PickAsync(new PickOptions
        {
            FileTypes = FilePickerFileType.Images
        });

        if (result != null)
        {
            _coverImagePath = result.FullPath;
            CoverImage.Source = ImageSource.FromFile(_coverImagePath);
        }
    }

    private async void OnPickVideoClicked(object sender, EventArgs e)
    {
        var result = await FilePicker.PickAsync(new PickOptions
        {
            FileTypes = FilePickerFileType.Videos
        });

        if (result != null)
        {
            _videoPath = result.FullPath;
            VideoLabel.Text = System.IO.Path.GetFileName(_videoPath);
        }
    }

    private async void OnUploadClicked(object sender, EventArgs e)
    {
        var title = TitleEntry.Text;
        var description = DescriptionEditor.Text;
        var genre = GenrePicker.SelectedItem?.ToString();

        if (string.IsNullOrWhiteSpace(title) ||
            string.IsNullOrWhiteSpace(description) ||
            string.IsNullOrWhiteSpace(genre) ||
            string.IsNullOrWhiteSpace(_coverImagePath) ||
            string.IsNullOrWhiteSpace(_videoPath))
        {
            await DisplayAlert("Ошибка", "Заполните все поля и выберите файлы", "Ок");
            return;
        }

        var anime = new AnimeContent
        {
            Title = title,
            Description = description,
            Genre = genre,
            CoverImagePath = _coverImagePath,
            VideoPath = _videoPath,
            UploadedByUserId = _userId
        };

        await App.Database.InsertAnimeAsync(anime);
        await DisplayAlert("Успешно", "Аниме загружено", "Ок");
        await Navigation.PopAsync();
    }
}