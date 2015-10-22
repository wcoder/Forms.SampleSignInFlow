using System;
using System.Threading.Tasks;
using LoginNavigationExample.CustomPages;
using Xamarin.Forms;

namespace LoginNavigationExample
{
	public partial class LoginPage : ContentPage
	{
		private LoginService _loginService;

		public LoginPage()
		{
			Init();
		}

		public LoginPage(string message)
		{
			Init();

			ShowErrorMessage(message);
		}

		private void Init()
		{
			InitializeComponent();

			_loginService = new LoginService();
			_loginService.Success += _loginService_Success;
			_loginService.Error += _loginService_Error;
		}

		private void ShowErrorMessage(string message)
		{
			SubmitButton.IsEnabled = true;
			LiveAuthButton.IsEnabled = true;
			LoginProgress.IsVisible = false;

			ErrorLabel.IsVisible = true;

			if (!string.IsNullOrEmpty(message))
			{
				ErrorLabel.Text = string.Format("Error! {0}", message);
			}
		}

		private void _loginService_Error(object sender, LoginEventArgs e)
		{
			ShowErrorMessage(e.Message);
		}

		private void _loginService_Success(object sender, EventArgs e)
		{
			App.IsLoggedIn = true;
			((App)Application.Current).PresentMainPage();
		}

		private async void SubmitButton_OnClicked(object sender, EventArgs e)
		{
			ControlsBlocked();

			await _loginService.Login(LoginValue.Text, PasswordValue.Text);
		}

		private async void LiveAuthButton_OnClicked(object sender, EventArgs e)
		{
			await LiveLogin();
		}

		protected async Task LiveLogin()
		{
			await App.Current.MainPage.Navigation.PushModalAsync(new LiveLoginPage());
		}

		private void ControlsBlocked()
		{
			SubmitButton.IsEnabled = false;
			LiveAuthButton.IsEnabled = false;
			LoginProgress.IsVisible = true;
			ErrorLabel.IsVisible = false;
		}
	}
}
