namespace LoginNavigationExample
{
	public class Constants
	{
		#region Live Auth Settings

		public static string AppId = "XXXXXXXXXXXXXXXXX";
		public static string AppSecretId = "XXXXXXXXXXXXXXXXXXXXX";
		public static string ExtendedPermissions = "wl.basic"; // help: https://msdn.microsoft.com/en-us/library/office/dn631845.aspx
		public static string AuthorizeUrl = "https://login.live.com/oauth20_authorize.srf";
		public static string RedirectUrl = "https://login.live.com/oauth20_desktop.srf";
		public static string TokenUrl = "https://login.live.com/oauth20_token.srf";

		#endregion
	}
}
