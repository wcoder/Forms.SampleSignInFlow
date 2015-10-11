using System;
using Xamarin.Forms;

namespace LoginNavigationExample
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

		private void Button_OnClicked(object sender, EventArgs e)
		{
			App.IsLoggedIn = false;
			((App)Application.Current).PresentLogin();
		}
	}
}
