using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LoginNavigationExample
{
	public class LoginService
	{
		public event EventHandler Success;
		public event EventHandler<LoginEventArgs> Error;

		public async Task Login(string name, string pass)
		{
			if (string.IsNullOrWhiteSpace(name)
			    || string.IsNullOrWhiteSpace(pass))
			{
				OnError("Empty fields!");
				return;
			}

			await Task.Delay(5000);

			if (name.Equals("wcoder")
				&& pass.Equals("123"))
			{
				OnSuccess();
			}
			else
			{
				OnError("Incorrect credentials!");
			}
		}

		protected virtual void OnSuccess()
		{
			Success?.Invoke(this, EventArgs.Empty);
		}

		protected virtual void OnError(string message)
		{
			Error?.Invoke(this, new LoginEventArgs { Message = message });
		}
	}

	public class LoginEventArgs : EventArgs
	{
		public string Message { get; set; }
	}
}
