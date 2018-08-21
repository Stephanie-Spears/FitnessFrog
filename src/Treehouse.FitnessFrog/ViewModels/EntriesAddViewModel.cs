using System;

namespace Treehouse.FitnessFrog.ViewModels
{
	public class EntriesAddViewModel
		: EntriesBaseViewModel
	{
		public EntriesAddViewModel()
		{
			Entry.Date = DateTime.Today;
		}
	}
}