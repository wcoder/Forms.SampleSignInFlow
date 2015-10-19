using System;
using Android.App;
using LoginNavigationExample.CustomPages;
using LoginNavigationExample.Droid.Renderers;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LiveLoginPage), typeof(LiveLoginPageDroid))]

namespace LoginNavigationExample.Droid.Renderers
{
	public class LiveLoginPageDroid : PageRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
		{
			base.OnElementChanged(e);
			LoginToFacebook(true);
		}

		void LoginToFacebook(bool allowCancel)
		{
			var activity = this.Context as Activity;

			var auth = new OAuth2Authenticator(
				clientId: Constants.AppId,
				clientSecret: Constants.AppSecretId,
				scope: Constants.ExtendedPermissions,
				authorizeUrl: new Uri(Constants.AuthorizeUrl),
				redirectUrl: new Uri(Constants.RedirectUrl),
				accessTokenUrl: new Uri(Constants.TokenUrl));

			auth.AllowCancel = allowCancel;

			// If authorization succeeds or is canceled, .Completed will be fired.
			auth.Completed += async (s, eargs) =>
			{
				if (!eargs.IsAuthenticated)
				{
					App.IsLoggedIn = false;
					((App)App.Current).PresentLogin("Canceled!");

					return;
				}

				var token = eargs.Account.Properties["access_token"];

				await App.Current.MainPage.Navigation.PopModalAsync();
				App.IsLoggedIn = true;
				((App)App.Current).PresentMainPage();
			};

			//auth.Error += async (sender, args) =>
			//{
				
			//};

			var intent = auth.GetUI(activity);
			activity.StartActivity(intent);
		}
	}
}