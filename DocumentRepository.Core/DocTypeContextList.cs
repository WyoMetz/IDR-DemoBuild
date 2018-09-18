using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentRepository.Core
{
	public class DocTypeContextList
	{
		public static IList<string> DocTypes { get; set; }

		public static async void PrepareList()
		{
			if(DocTypes == null)
			{
				DocTypes = await Repository.GetDocTypesAsync();
			}
		}

		public static void Add(string DocType)
		{
			DocTypes.Add(DocType);
		}
	}
}
