using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Treehouse.FitnessFrog.Shared.Models;
using Treehouse.FitnessFrog.Shared.Security;
using Treehouse.FitnessFrog.ViewModels;

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

		[AllowAnonymous]
		public ActionResult SignIn()
		{
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> SignIn(AccountSignInViewModel viewModel)
		{
			if (!ModelState.IsValid)
			{
				return View(viewModel);
			}

			// Sign-in the user
			var result = await _signInManager.PasswordSignInAsync(
				viewModel.Email, viewModel.Password, viewModel.RememberMe, shouldLockout: false);

			// Check the result
			switch (result)
			{
				case SignInStatus.Success:
					return RedirectToAction("Index", "Entries");

				case SignInStatus.Failure:
					ModelState.AddModelError("", "Invalid login attempt.");
					return View(viewModel);

				case SignInStatus.LockedOut:
				case SignInStatus.RequiresVerification:
					throw new NotImplementedException("Identity feature not implemented.");
				default:
					throw new Exception("Unexpected Microsoft.AspNet.Identity.Owin.SignInStatus enum value: " + result);
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult SignOut()
		{
			_authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

			return RedirectToAction("Index", "Entries");
		}

		[AllowAnonymous]
		public ActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Register(AccountRegisterViewModel viewModel)
		{
			// If the ModelState is valid...
			if (ModelState.IsValid)
			{
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

/*
 * The IAuthenticationManager.SignOut() method accepts a single parameter specifying the authentication type that you want the user signed out of.
 * Sign-in and register routes should allow anonymous requests so non-authenticated users can sign-in or create user accounts. The AllowAnonymous attribute allows both authenticated and anonymous requests (i.e. non-authenticated requests) to access the route.
 * The Authorize attribute can be added globally so that every controller in the app will only be accessible to authenticated requests. AllowAnonymous attributes can be added to individual controllers or action methods to override the global Authorize attribute.
 * The Authorize attribute requires that requests be authenticated in order to access the route. If a request isn't authenticated, MVC will return a response with a 401 Unauthorized HTTP status code, which is rewritten by Identity to a 302 Found HTTP status code in order to redirect the browser to the configured login path.
 * Adding Authorize attributes at the controller level restricts every action method to authenticated requests while adding Authorize attributes to individual action methods restricts just those action methods.
 */

/*
 * The User.Identity.GetUserId() method can be used to get the current user's Id. The current user's Id is often used when performing CRUD operations against the database.
 * ASP.NET MVC can be configured to require HTTPS by adding the RequireHttps attribute as a global attribute. When using the RequireHttps attribute, MVC will redirect requests using HTTP to use HTTPS.
 * When using Identity with Entity Framework, the AspNetUsers table is used to persist user records in the database. The AspNetUsers Id table column is the unique identifier for each user in the system.
 * Over-the-wire communications should always be secured using SSL/TLS when implementing authentication in websites or web apps. Self-issued SSL certificates are often used for local development environments, but established certificate authorities should be used for production environments.
 * User passwords are stored in the database as hashes. Identity's UserManager class internally uses an instance of the PasswordHasher class to hash the user's provided clear text password.
 */