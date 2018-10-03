using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentRepository.Models;

namespace DocumentRepository.Core
{
	public class MarineList
	{
		public static IList<Marine> Marines { get; set; }

		public static async void PrepareList()
		{
			if (Marines == null)
			{
				Marines = await Repository.GetMarinesAsync();
			}
		}

		public static void Add(int EDIPI, string LastName, string FirstName, string MI = " ")
		{
			Marines.Add(new Marine
			{
				EDIPI = EDIPI,
				LastName = LastName,
				FirstName = FirstName,
				MI = MI
			});
		}


	}
}
