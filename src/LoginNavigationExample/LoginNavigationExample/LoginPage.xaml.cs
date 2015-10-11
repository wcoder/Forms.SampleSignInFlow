using System;
using Xamarin.Forms;

namespace LoginNavigationExample
{
	public partial class LoginPage : ContentPage
	{
		private readonly LoginService _loginService;

		public LoginPage()
		{
			InitializeComponent();

			_loginService = new LoginService();
			_loginService.Success += _loginService_Success;
			_loginService.Error += _loginService_Error;
		}

		private async void _loginService_Error(object sender, LoginEventArgs e)
		{
			SubmitButton.IsEnabled = true;
			LoginProgress.IsVisible = false;

			await DisplayAlert("Error!", e.Message, "OK");
		}

		private void _loginService_Success(object sender, EventArgs e)
		{
			App.IsLoggedIn = true;
			((App)Application.Current).PresentMainPage();
		}

		private async void SubmitButton_OnClicked(object sender, EventArgs e)
		{
			SubmitButton.IsEnabled = false;
			LoginProgress.IsVisible = true;

			await _loginService.Login(LoginValue.Text, PasswordValue.Text);
		}
	}
}
