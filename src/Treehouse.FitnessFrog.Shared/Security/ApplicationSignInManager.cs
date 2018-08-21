using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Treehouse.FitnessFrog.Shared.Models;

namespace Treehouse.FitnessFrog.Shared.Security
{
	public class ApplicationSignInManager : SignInManager<User, string>
	{
		public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
			: base(userManager, authenticationManager)
		{
		}
	}
}