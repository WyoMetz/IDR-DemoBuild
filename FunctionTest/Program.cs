using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentRepository.Core;
using DocumentRepository.Models;
using DocumentRepository.DAL;

namespace FunctionTest
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Press enter to begin test");
			Console.ReadLine();
			DiaryTable.ReadDiaryTable(CommandReadModel.SelectDiaries());
		}
	}
}
