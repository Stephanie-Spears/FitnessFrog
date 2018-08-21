using Microsoft.AspNet.Identity;
using Treehouse.FitnessFrog.Shared.Models;

namespace Treehouse.FitnessFrog.Shared.Security
{
	public class ApplicationUserManager : UserManager<User>
	{
		public ApplicationUserManager(IUserStore<User> userStore)
			: base(userStore)
		{
		}
	}
}