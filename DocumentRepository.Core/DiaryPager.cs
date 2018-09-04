using DocumentRepository.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DocumentRepository.Core
{
	public class DiaryPager
	{
		private static int PageIndex { get; set; } = 0;

		static DataTable PagedList = new DataTable();

		public static DataTable SetPaging(IList<UnitDiary> ListToPage, int RecordsPerPage)
		{
			int PageGroup = PageIndex * RecordsPerPage;
			IList<UnitDiary> PagedList = new List<UnitDiary>();
			PagedList = ListToPage.Skip(PageGroup).Take(RecordsPerPage).ToList();
			DataTable FinalPaging = PagedTable(PagedList);
			return FinalPaging;
		}

		private static DataTable PagedTable<T>(IList<T> SourceList)
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

		public static DataTable Next(IList<UnitDiary> ListToPage, int RecordsPerPage)
		{
			PageIndex++;
			if (PageIndex >= ListToPage.Count / RecordsPerPage)
			{
				PageIndex = ListToPage.Count / RecordsPerPage;
			}
			PagedList = SetPaging(ListToPage, RecordsPerPage);
			return PagedList;
		}

		public static DataTable Previous(IList<UnitDiary> ListToPage, int RecordsPerPage)
		{
			PageIndex--;
			if (PageIndex <= 0)
			{
				PageIndex = 0;
			}
			PagedList = SetPaging(ListToPage, RecordsPerPage);
			return PagedList;
		}

		public static DataTable First(IList<UnitDiary> ListToPage, int RecordsPerPage)
		{
			PageIndex = 0;
			PagedList = SetPaging(ListToPage, RecordsPerPage);
			return PagedList;
		}

		public static DataTable Last(IList<UnitDiary> ListToPage, int RecordsPerPage)
		{
			PageIndex = ListToPage.Count / RecordsPerPage;
			PagedList = SetPaging(ListToPage, RecordsPerPage);
			return PagedList;
		}
	}
}
