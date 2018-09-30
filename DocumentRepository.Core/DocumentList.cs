using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentRepository.Models;

namespace DocumentRepository.Core
{
	public class DocumentList
	{
		public static IList<Document> Documents { get; set; }

		public static async void PrepareList()
		{
			if (Documents == null)
			{
				Documents = await Repository.GetDocumentsAsync();
			}
		}
	}
}
