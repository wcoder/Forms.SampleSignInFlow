using System;
using System.Threading.Tasks;
using LoginNavigationExample.CustomPages;
using UIKit;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using LoginNavigationExample.iOS.Renderers;

[assembly: ExportRenderer(typeof(LiveLoginPage), typeof(LiveLoginPageIOS))]

namespace LoginNavigationExample.iOS.Renderers
{
	public class LiveLoginPageIOS : PageRenderer
	{
		private readonly TaskScheduler uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();

		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);

			var auth = new OAuth2Authenticator(
				clientId: Constants.AppId,
				clientSecret: Constants.AppSecretId,
				scope: Constants.ExtendedPermissions,
				authorizeUrl: new Uri(Constants.AuthorizeUrl),
				redirectUrl: new Uri(Constants.RedirectUrl),
				accessTokenUrl: new Uri(Constants.TokenUrl));

			auth.AllowCancel = false;

			// If authorization succeeds or is canceled, .Completed will be fired.
			auth.Completed += async (s, eargs) =>
			{
				if (!eargs.IsAuthenticated)
				{
					App.IsLoggedIn = false;
					((App) App.Current).PresentLogin("Canceled!");

					return;
				}

				var token = eargs.Account.Properties["access_token"];

				await App.Current.MainPage.Navigation.PopModalAsync();
				App.IsLoggedIn = true;
				((App) App.Current).PresentMainPage();
			};

			UIViewController vc = auth.GetUI();

			ViewController.AddChildViewController(vc);
			ViewController.View.Add(vc.View);

			vc.ChildViewControllers[0].NavigationItem.LeftBarButtonItem = new UIBarButtonItem(
				UIBarButtonSystemItem.Cancel, async (o, eargs) => await App.Current.MainPage.Navigation.PopModalAsync()
			);
		}
	}
}
