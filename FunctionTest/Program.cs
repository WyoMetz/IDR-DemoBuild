using DocumentRepository.Core;
using DocumentRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionTest
{
	class Program
	{
		static void Main(string[] args)
		{
			CommandModel.SetDateContext("2018");
			DocumentList.PrepareList();
			MarineList.PrepareList();
			Console.WriteLine("Should be prepped");
			Console.ReadLine();
			MarineDocumentList.PrepareList();

			//foreach (var Marine in MarineList.Marines)
			//{
			//	MarineList.Marines.Add(new Document
			//	{
			//		Documents = DocumentList.Documents.Where(o => o.DocID == Marine.EDIPI).ToList()
			//	});
			//}

			foreach (var Marine in MarineDocumentList.MarineDocuments)
			{
				Console.WriteLine(
					Marine.EDIPI + " " +
					Marine.LastName + " "
					);
				foreach (var Doc in Marine.Documents)
				{
					Console.WriteLine(Doc.DocType + " " + Doc.Section);
				}
			}

			Console.WriteLine("Done \n Press any key to exit...");
			Console.ReadLine();
		}
	}
}
//CommandModel.SetDateContext("2018");
//Console.WriteLine("Dropping Table");
//DocumentTable.InsertDocument("DROP TABLE DocumentTable2018");
//Console.WriteLine("Done");
//Console.WriteLine("Creating Table");
//Database.CreateTable(CommandCreateModel.CreateDocumentTable());
//Console.WriteLine("Done \n Press any key to exit...");
//Console.ReadLine();
