using System.ComponentModel.DataAnnotations;

namespace Treehouse.FitnessFrog.ViewModels
{
	public class AccountRegisterViewModel
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[StringLength(100, MinimumLength = 6, ErrorMessage = "the {0} must be at least {2} characters long.")]
		public string Password { get; set; }

		[Display(Name = "Confirm Password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }
	}
}