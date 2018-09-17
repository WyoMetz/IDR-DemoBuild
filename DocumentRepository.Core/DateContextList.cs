using System.Collections.Generic;

namespace DocumentRepository.Core
{
	public class DateContextList
	{
		public static IList<string> Dates { get; set; }

		public static async void PrepareDateContext()
		{
			if (Dates == null)
			{
				Dates = await Repository.GetDatesAsync();
			}
		}

		public static void Add(string Date)
		{
			Dates.Add(Date);
		}
	}
}
