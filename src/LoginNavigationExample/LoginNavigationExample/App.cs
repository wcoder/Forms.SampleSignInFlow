using Xamarin.Forms;

namespace LoginNavigationExample
{
	public class App : Application
	{
		public static bool IsLoggedIn { get; set; }

		public App()
		{
			PresentMainPage();
		}

		public void PresentLogin(string message = "")
		{
			MainPage = new LoginPage(message);
		}

		public void PresentMainPage()
		{
			MainPage = !IsLoggedIn
				? (Page)new LoginPage()
				: (Page)new MainPage();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
