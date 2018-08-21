using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;

namespace Treehouse.FitnessFrog
{
	public partial class Startup
	{
		private void ConfigureAuth(IAppBuilder app)
		{
			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
				LoginPath = new PathString("/Account/SignIn"),
				Provider = new CookieAuthenticationProvider()
			});
		}
	}
}