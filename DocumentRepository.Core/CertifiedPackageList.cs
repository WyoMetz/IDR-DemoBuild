using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentRepository.Models;

namespace DocumentRepository.Core
{
	public class CertifiedPackageList
	{
		public static IList<CertifiedPackage> Packages { get; set; }

		public static async void PreparePackages()
		{
			if (Packages == null)
			{
				Packages = await Repository.GetPackagesAsync();
			}
		}

		//TODO: Search Methods for the Packages e.g. EDIPI, LastName, Etc.
	}
}
