using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentRepository.Models;

namespace DocumentRepository.Core
{
	public class MarineDocumentSearch
	{
		public static IList<Marine> ByEDIPI(int Edipi)
		{
			IList<Marine> list = MarineDocumentList.MarineDocuments
				.Where(o => o.EDIPI == Edipi)
				.ToList();
			return list;
		}

		public static IList<Marine> ByEDIPI(int Edipi, string LastName)
		{
			IList<Marine> list = MarineDocumentList.MarineDocuments
				.Where(o => o.EDIPI <= Edipi)
				.Where(o => o.LastName.Contains(LastName))
				.ToList();
			return list;
		}

		public static IList<Marine> ByEDIPI(int Edipi, string LastName, string FirstName)
		{
			IList<Marine> list = MarineDocumentList.MarineDocuments
				.Where(o => o.EDIPI <= Edipi)
				.Where(o => o.LastName.Contains(LastName))
				.Where(o => o.FirstName.Contains(FirstName))
				.ToList();
			return list;
		}

		public static IList<Marine> ByLastName(string LastName)
		{
			IList<Marine> list = MarineDocumentList.MarineDocuments
				.Where(o => o.LastName.Contains(LastName))
				.ToList();
			return list;
		}

		public static IList<Marine> ByFirstName(string FirstName)
		{
			IList<Marine> list = MarineDocumentList.MarineDocuments
				.Where(o => o.FirstName.Contains(FirstName))
				.ToList();
			return list;
		}

		public static IList<Marine> ByLastName(string LastName, string FirstName)
		{
			IList<Marine> list = MarineDocumentList.MarineDocuments
				.Where(o => o.LastName.Contains(LastName))
				.Where(o => o.FirstName.Contains(FirstName))
				.ToList();
			return list;
		}

		private static IList<Marine> ByDocType(string DocType)
		{
			//Does not work
			IList<Marine> list = MarineDocumentList.MarineDocuments
				.Where(o => o.Documents.Any(d => d.DocType.Contains(DocType)))
				.ToList();
			return list;
		}
	}
}
