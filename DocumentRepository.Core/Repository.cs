using DocumentRepository.DAL;
using DocumentRepository.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocumentRepository.Core
{
	public static class Repository
	{
		/// <summary>
		/// Gets the Unit Diaries from the Database
		/// </summary>
		/// <returns>IList UnitDiary</returns>
		public static async Task<IList<UnitDiary>> GetDiariesAsync()
		{
			IList<UnitDiary> unitDiaries = new List<UnitDiary>();
			unitDiaries = await DiaryTable.ReadDiaryTable(CommandModel.SelectDiaries());
			return unitDiaries;
		}
		/// <summary>
		/// Inserts all Unit Diaries from the CSV into the Database
		/// </summary>
		/// <param name="UnitDiariesList"></param>
		/// <param name="progress"></param>
		/// <returns>Task Complete</returns>
		public static async Task InsertDiariesAsync(IList<UnitDiary> UnitDiariesList, IProgress<ReportModel> progress)
		{
			Task<string> InsertTask = Task.Run(() => DiaryTable.InsertBulkDiaryInfoAsync(UnitDiariesList));
			Task TimerTask = Task.Run(() => DiaryTable.BulkInsertTimer(UnitDiariesList, progress));
			await Task.WhenAll(InsertTask, TimerTask);
		}
		/// <summary>
		/// Updates the Selected Diary in the Table
		/// </summary>
		/// <param name="DiaryID"></param>
		/// <param name="UDNumber"></param>
		/// <param name="FilePath"></param>
		/// <returns></returns>
		public static async Task UpdateRegularDiaryAsync(int DiaryID, string UDNumber, string FilePath)
		{
			string UserName = AppSettings.User;
			string Date = DateTime.Now.ToShortDateString();
			string fileName = UDNumber + ".pdf";
			Task<string> SaveFile = Task.Run(() => FileOperation.CopyFile(fileName, "Diary", FilePath));
			string FileSaveLocation = await SaveFile;
			Task UpdateDatabase = Task.Run(() => DiaryTable.UpdateUnitDiary(CommandModel.DiaryUpdate(UserName, Date, FileSaveLocation, DiaryID)));
			await UpdateDatabase;
			return;
		}

		public static string GetFile()
		{
			string FilePath = FileOperation.ChooseFile();
			return FilePath;
		}
	}
}
