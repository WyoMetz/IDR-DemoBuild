using DocumentRepository.Models;
using System.Collections.Generic;
using System.Linq;

namespace DocumentRepository.Core
{
	public class MarineDocumentList
	{
		private static IList<Marine> listSetup = new List<Marine>();
		public static IList<Marine> MarineDocuments { get; set; }

		public static void PrepareList()
		{
			if (MarineDocuments == null)
			{
				SetUpList();
			}
			if (MarineDocuments != null)
			{
				MarineDocuments.Clear();
				listSetup.Clear();
				SetUpList();
			}
		}
		private static void SetUpList()
		{
			foreach (var Marine in MarineList.Marines)
			{
				listSetup.Add(new Marine
				{
					EDIPI = Marine.EDIPI,
					LastName = Marine.LastName,
					FirstName = Marine.FirstName,
					MI = Marine.MI,
					Documents = DocumentList.Documents.Where(o => o.DocID == Marine.EDIPI).ToList()
				});
			}
			MarineDocuments = listSetup;
		}
	}
}
