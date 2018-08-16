using System.Web.Mvc;
using Treehouse.FitnessFrog.ViewModels;

namespace Treehouse.FitnessFrog.Controllers
{
	public class AccountController : Controller
	{
		// GET: Account
		public ActionResult Register()
		{
			return View();
		}

		// POST: Account
		[HttpPost]
		public ActionResult Register(AccountRegisterViewModel viewModel)
		{
			return View(viewModel);
		}

		/*		// GET: Account/Details/5
				public ActionResult Details(int id)
				{
					return View();
				}

				// GET: Account/Create
				public ActionResult Create()
				{
					return View();
				}

				// POST: Account/Create
				[HttpPost]
				public ActionResult Create(FormCollection collection)
				{
					try
					{
						// TODO: Add insert logic here

						return RedirectToAction("Index");
					}
					catch
					{
						return View();
					}
				}

				// GET: Account/Edit/5
				public ActionResult Edit(int id)
				{
					return View();
				}

				// POST: Account/Edit/5
				[HttpPost]
				public ActionResult Edit(int id, FormCollection collection)
				{
					try
					{
						// TODO: Add update logic here

						return RedirectToAction("Index");
					}
					catch
					{
						return View();
					}
				}

				// GET: Account/Delete/5
				public ActionResult Delete(int id)
				{
					return View();
				}

				// POST: Account/Delete/5
				[HttpPost]
				public ActionResult Delete(int id, FormCollection collection)
				{
					try
					{
						// TODO: Add delete logic here

						return RedirectToAction("Index");
					}
					catch
					{
						return View();
					}
				}*/
	}
}