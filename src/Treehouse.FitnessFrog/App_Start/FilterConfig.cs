using System.Web.Mvc;

namespace Treehouse.FitnessFrog
{
	public static class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new RequireHttpsAttribute());
			filters.Add(new AuthorizeAttribute());
		}
	}
}