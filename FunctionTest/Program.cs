using DocumentRepository.Core;
using DocumentRepository.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace FunctionTest
{
	class Program
	{
		private static string AppDataLocal = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
		private static string RepositoryLocation = Path.GetFullPath(@"\\mcuscljnfs44.mcdsus.mcds.usmc.mil\IPAC\IDR\Application");
		private static string AppName = "IDR Demo Build.exe";
		private static string ShortcutTarget = Path.GetFullPath(AppDataLocal + @"\IDR\IDR Demo Build.exe");
		private static string ShortcutLink = Path.GetFullPath(AppDataLocal + @"\IDR\IDR Demo Build.lnk");
		private static string AppDirectory = Path.GetFullPath(AppDataLocal + @"\IDR");
		private static string ShortcutLocation = Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\IDR Demo Build.lnk");

		static void Main(string[] args)
		{
			Console.WriteLine(AppDataLocal + "\n"
				+ AppName + "\n"
				+ ShortcutTarget + "\n"
				+ ShortcutLink + "\n"
				+ AppDirectory + "\n"
				+ ShortcutLocation + "\n"
				+ RepositoryLocation + "\n");
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
			//CommandModel.SetDateContext("2018");
			//DocumentList.PrepareList();
			//MarineList.PrepareList();
			//Console.WriteLine("Should be prepped");
			//Console.ReadLine();
			//MarineDocumentList.PrepareList();

			////foreach (var Marine in MarineList.Marines)
			////{
			////	MarineList.Marines.Add(new Document
			////	{
			////		Documents = DocumentList.Documents.Where(o => o.DocID == Marine.EDIPI).ToList()
			////	});
			////}

			//foreach (var Marine in MarineDocumentList.MarineDocuments)
			//{
			//	Console.WriteLine(
			//		Marine.EDIPI + " " +
			//		Marine.LastName + " "
			//		);
			//	foreach (var Doc in Marine.Documents)
			//	{
			//		Console.WriteLine(Doc.DocType + " " + Doc.Section);
			//	}
			//}
