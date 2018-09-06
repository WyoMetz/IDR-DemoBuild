using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DocumentRepository.Core
{
	public class CreateDataTable
	{
		public static DataTable FromList<T>(IList<T> SourceList)
		{
			Type columnType = typeof(T);
			DataTable TableToReturn = new DataTable();

			foreach (var Column in columnType.GetProperties())
			{
				TableToReturn.Columns.Add(Column.Name, Column.PropertyType);
			}

			foreach (object item in SourceList)
			{
				DataRow ReturnTableRow = TableToReturn.NewRow();
				foreach (var Column in columnType.GetProperties())
				{
					ReturnTableRow[Column.Name] = Column.GetValue(item);
				}
				TableToReturn.Rows.Add(ReturnTableRow);
			}
			return TableToReturn;
		}
	}
}
