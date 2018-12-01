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
			if (Documents != null)
			{
				Documents.Clear();
				Documents = await Repository.GetDocumentsAsync();
			}
		}

		public static void Add(int EDIPI, string DocType, string Section, DateTime UploadDate, string UploadUser, DateTime DateOfDoc, string UploadLocation)
		{
			Documents.Add(new Document
			{
				DocID = EDIPI,
				DocType = DocType,
				Section = Section,
				UploadDate = UploadDate,
				UploadUser = UploadUser,
				DateOfDoc = DateOfDoc,
				UploadLocation = UploadLocation
			});
		}
	}
}
