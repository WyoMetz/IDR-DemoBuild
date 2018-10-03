using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentRepository.Core;
using DocumentRepository.Models;
using DocumentRepository.DAL;
using System.Globalization;

namespace FunctionTest
{
	class Program
	{
		static void Main(string[] args)
		{
            Console.WriteLine(DateTime.Now.ToLongDateString());
            Console.WriteLine(DateTime.Now.ToShortDateString());

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
