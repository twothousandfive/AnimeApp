namespace AnimeApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("MainPage", typeof(Pages.MainPage));
            Routing.RegisterRoute("ProfilePage", typeof(Pages.ProfilePage));
            Routing.RegisterRoute("LoginPage", typeof(Pages.LoginPage));

            if (Preferences.ContainsKey("user_email"))
            {
                GoToAsync("//MainPage");
            }
            else
            {
                GoToAsync("//LoginPage");
                // Отключаем TabBar на LoginPage
                Shell.SetTabBarIsVisible(this, false);
            }
        }
        protected override void OnNavigated(ShellNavigatedEventArgs args)
        {
            base.OnNavigated(args);
            // Скрываем TabBar на LoginPage
            bool isLoginPage = Current.CurrentState.Location.ToString().Contains("LoginPage");
            Shell.SetTabBarIsVisible(this, !isLoginPage);
        }
    }
}
