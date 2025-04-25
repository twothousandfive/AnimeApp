namespace AnimeApp
{
    public partial class App : Application
    {
        public static Services.AppDatabase Database { get; private set; }
        public App()
        {
            InitializeComponent();
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "animeApp.db3");
            Database = new Services.AppDatabase(dbPath);

            if (Preferences.ContainsKey("user_email"))
            {
                MainPage = new NavigationPage(new Pages.MainPage());
            }
            else
            {
                MainPage = new NavigationPage(new Pages.LoginPage());
            }
        }

        //protected override Window CreateWindow(IActivationState? activationState)
        //{
        //    return new Window(new AppShell());
        //}
    }
}