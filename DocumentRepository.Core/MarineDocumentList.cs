using DocumentRepository.Models;
using System.Collections.Generic;
using System.Linq;

namespace DocumentRepository.Core
{
	public class MarineDocumentList
	{
		public static IList<Marine> MarineDocuments = new List<Marine>();

		public static void PrepareList()
		{
			foreach (var Marine in MarineList.Marines)
			{
				MarineDocuments.Add(new Marine
				{
					EDIPI = Marine.EDIPI,
					LastName = Marine.LastName,
					FirstName = Marine.FirstName,
					MI = Marine.MI,
					Documents = DocumentList.Documents.Where(o => o.DocID == Marine.EDIPI).ToList()
				});
			}
		}
	}
}
