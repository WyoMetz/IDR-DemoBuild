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
			if (Diaries != null)
			{
				Diaries.Clear();
				Diaries = await Repository.GetDiariesAsync();
			}
		}

		public static IList<UnitDiary> NeedUploaded()
		{
			IList<UnitDiary> list = Diaries
				.Where(Uploaded => Uploaded.NotUploaded())
				.Where(UDNumber => UDNumber.RegularDiaries())
				.ToList();
			return list;
		}

		public static IList<UnitDiary> Uploaded()
		{
			IList<UnitDiary> list = Diaries
				.Where(UDNumber => UDNumber.RegularDiaries())
				.Where(Uploaded => !Uploaded.NotUploaded())
				.ToList();
			return list;
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
