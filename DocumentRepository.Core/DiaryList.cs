using DocumentRepository.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DocumentRepository.Core
{
	public class DiaryList
	{
		public static IList<UnitDiary> Diaries { get; set; }

		public static async void PrepareList()
		{
			if (Diaries == null)
			{
				Diaries = await Repository.GetDiariesAsync();
			}
		}

		public static IList<UnitDiary> NeedUploaded()
		{
			IList<UnitDiary> list = Diaries.Where(Uploaded => Uploaded.NotUploaded()).ToList();
			return list;
		}

		public static IList<UnitDiary> Uploaded()
		{
			IList<UnitDiary> list = Diaries.Where(Uploaded => !Uploaded.NotUploaded()).ToList();
			return list;
		}

		public static DataTable SearchDiaryNumbers(string SearchString)
		{
			bool sorter = Int32.TryParse(SearchString, out int sortNumber);
			if (!sorter) { sortNumber = 0; }
			IList<UnitDiary> list = Diaries.Where(o => o.UDNumber >= sortNumber).ToList();
			return DiaryPager.SetPaging(list, 10);
		}

		public static DataTable SearchCertifierEDIPI(string SearchString)
		{
			bool sorter = Int32.TryParse(SearchString, out int sortNumber);
			if (!sorter) { sortNumber = 0; }
			IList<UnitDiary> list = Diaries.Where(o => o.CertifierEdipi >= sortNumber).ToList();
			return DiaryPager.SetPaging(list, 10);
		}

		public static DataTable SearchCertifierLastName(string SearchString)
		{
			IList<UnitDiary> list = Diaries.Where(o => o.LastName.StartsWith(SearchString)).ToList();
			return DiaryPager.SetPaging(list, 10);
		}

		public static void UpdateList(int DiaryID, string UserName, string Date, string FileSaveLocation)
		{
			var Diary = Diaries.First(i => i.DiaryID == DiaryID);
			Diary.Uploaded = "True";
			Diary.UploadedBy = UserName;
			Diary.UploadedOn = DateTime.Parse(Date);
			Diary.UploadLocation = FileSaveLocation;
		}
	}
}
