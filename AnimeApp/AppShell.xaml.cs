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

            // Устанавливаем TabBar видимым по умолчанию
            Shell.SetTabBarIsVisible(this, true);

            GoToAsync("MainPage");
            //if (Preferences.ContainsKey("user_email"))
            //{
            //    GoToAsync("//MainPage");
            //}
            //else
            //{
            //    GoToAsync("//LoginPage");
            //    // Отключаем TabBar на LoginPage
            //    Shell.SetTabBarIsVisible(this, false);
            //}
        }

        protected override async void OnNavigating(ShellNavigatingEventArgs args)
        {
            base.OnNavigating(args);

            // Проверяем, если пользователь пытается перейти на ProfilePage
            if (args.Target.Location.OriginalString.Contains("ProfilePage"))
            {
                // Если пользователь не авторизован
                if (!Preferences.ContainsKey("user_email"))
                {
                    // Отменяем переход на ProfilePage
                    args.Cancel();
                    // Перенаправляем на LoginPage
                    await GoToAsync("//LoginPage");
                }
            }
        }

        protected override void OnNavigated(ShellNavigatedEventArgs args)
        {
            base.OnNavigated(args);
            // Проверяем текущую страницу
            var currentRoute = Current.CurrentState.Location.ToString();
            System.Diagnostics.Debug.WriteLine($"Current route: {currentRoute}");

            // Скрываем TabBar на LoginPage, показываем на других страницах
            bool isLoginPage = currentRoute.Contains("LoginPage");
            Shell.SetTabBarIsVisible(this, !isLoginPage);
        }
    }
}
