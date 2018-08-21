using System.Threading.Tasks;
using System.Web.Mvc;
using Treehouse.FitnessFrog.Shared.Models;
using Treehouse.FitnessFrog.ViewModels;
using Microsoft.Owin.Security;
using Treehouse.FitnessFrog.Shared.Security;

namespace Treehouse.FitnessFrog.Controllers
{
	public class AccountController : Controller
	{
		private readonly ApplicationUserManager _userManager;
		private readonly ApplicationSignInManager _signInManager;
		private readonly IAuthenticationManager _authenticationManager;

		public AccountController(
			ApplicationUserManager userManager,
			ApplicationSignInManager signInManager,
			IAuthenticationManager authenticationManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_authenticationManager = authenticationManager;
		}

		// GET: Register
		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<ActionResult> Register(AccountRegisterViewModel viewModel)
		{
			// If the ModelState is valid...
			if (ModelState.IsValid)
			{
				// Validate if the provided email address is already in use.
				/*				var existingUser = await UserManager.FindByEmailAsync(viewModel.Email);
								if (existingUser != null)
								{
									ModelState.AddModelError("Email", $"The provided email address '{viewModel.Email}' has already been used to register an account. Please sign-in using your existing account.");
								}*/

				// Instantiate a User object
				var user = new User { UserName = viewModel.Email, Email = viewModel.Email };

				// Create the user
				var result = await _userManager.CreateAsync(user, viewModel.Password);

				// If the user was successfully created...
				if (result.Succeeded)
				{
					// Sign-in the user
					await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

					// Redirect them to the web app's "Home" page
					return RedirectToAction("Index", "Entries");
				}

				// If there were errors...
				// Add model errors
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error);
				}
			}

			return View(viewModel);
		}
	}
}

/*
 To represent users in the database when using ASP.NET Identity with Entity Framework you can inherit the provided "IdentityUser" class.
 Protecting a user's password requires communication between the client and the server to be encrypted

The Identity SignInManager<TIdentityUser, TKey> class provides methods to sign-in a user using an instance of a user model class or a user's key and secret.
The Identity SignInManager<TIdentityUser, TKey> class defines two generic type parameters—TIdentityUser and TKey—one for your user model type, and another for your user key type.
The Request.IsAuthenticated property can be used to detect when the current user has been authenticated.
Checking if the current user is authenticated is commonly done in order to show/hide parts of a page.
The Identity UserManager<TIdentityUser> class provides methods to find, create, update, and delete user accounts.
The Identity UserManager<TIdentityUser> class defines a single generic type parameter—TIdentityUser—that should be set to your user model type.

*/