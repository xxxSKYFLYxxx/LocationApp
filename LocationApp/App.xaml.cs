using Xamarin.Forms;

namespace LocationApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Действия при запуске приложения
        }

        protected override void OnSleep()
        {
            // Действия при переходе приложения в спящий режим
        }

        protected override void OnResume()
        {
            // Действия при возобновлении работы приложения
        }
    }
}