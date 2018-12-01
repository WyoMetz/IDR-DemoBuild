using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionTest
{
	class ExpnadedCsvReader
	{
		private static string CsvHeader;

		public static Task ReadCsv(string FullFilePath)
		{
			using(StreamReader reader = new StreamReader(FullFilePath))
			{

			}
		}
	}
}
