﻿using DocumentRepository.DAL;
using DocumentRepository.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocumentRepository.Core
{
	//TODO: Strip down Repository into separate classes
	public static class Repository
	{
		private static string UserName { get; set; } = AppSettings.User;
		private static string InsertDate { get; set; } = DateTime.Now.ToShortDateString();

		/// <summary>
		/// Gets the Unit Diaries from the Database
		/// </summary>
		/// <returns>IList UnitDiary</returns>
		public static async Task<IList<UnitDiary>> GetDiariesAsync()
		{
			IList<UnitDiary> unitDiaries = new List<UnitDiary>();
			unitDiaries = await DiaryTable.ReadDiaryTable(CommandReadModel.SelectDiaries());
			return unitDiaries;
		}

		/// <summary>
		/// Gets all Certified Packages from the Database.
		/// </summary>
		/// <returns>IList CertifiedPackage</returns>
		public static async Task<IList<CertifiedPackage>> GetPackagesAsync()
		{
			IList<CertifiedPackage> certifiedPackages = new List<CertifiedPackage>();
			certifiedPackages = await CertifiedPackageTable.Read(CommandReadModel.SelectCertifiedPackages());
			return certifiedPackages;
		}

		public static async Task<IList<Document>> GetDocumentsAsync()
		{
			IList<Document> documents = new List<Document>();
			documents = await DocumentTable.ReadDocumentTable(CommandReadModel.SelectDocument());
			return documents;
		}

		public static async Task<IList<string>> GetDatesAsync()
		{
			IList<string> Dates = new List<string>();
			Dates = await DateTable.ReadDateTable(CommandReadModel.ReadDateTable());
			return Dates;
		}

		public static async Task<User> GetUserSettings()
		{
			User Settings = new User();
			Settings = await UserTable.ReadUserTable(CommandReadModel.ReadUserSettings());
			return Settings;
		}

		public static async Task<IList<string>> GetSectionsAsync()
		{
			IList<string> Sections = new List<string>();
			Sections = await SectionTable.ReadSectionTable(CommandReadModel.ReadSectionTable());
			return Sections;
		}

		public static async Task<IList<string>> GetDocTypesAsync()
		{
			IList<string> DocTypes = new List<string>();
			DocTypes = await DocTypeTable.ReadDocTypeTable(CommandReadModel.ReadDocTable());
			return DocTypes;
		}

		public static async Task<IList<Marine>> GetMarinesAsync()
		{
			IList<Marine> Marines = new List<Marine>();
			Marines = await MarineTable.ReadMarine(CommandReadModel.SelectMarine());
			return Marines;
		}

		public static async Task<string> GetVersionAsync()
		{
			string version = await VersionTable.ReadVersion(CommandReadModel.ReadVersionTable());
			return version;
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
		/// <returns>Task Complete</returns>
		public static async Task UpdateRegularDiaryAsync(int DiaryID, string UDNumber, string FilePath)
		{
			string fileName = UDNumber + ".pdf";
			Task<string> SaveFile = Task.Run(() => FileOperation.CopyFile(fileName, "Diary", FilePath));
			string FileSaveLocation = await SaveFile;
			Task UpdateDatabase = Task.Run(() => DiaryTable.UpdateUnitDiary(CommandUpdateModel.DiaryUpdate(FileSaveLocation, DiaryID)));
			await UpdateDatabase;
			DiaryList.UpdateList(DiaryID, UserName, InsertDate, FileSaveLocation);
			return;
		}

		/// <summary>
		/// Updates the Associated Diary and adds a Record to the Certified Package Table
		/// </summary>
		/// <param name="DiaryID"></param>
		/// <param name="UDNumber"></param>
		/// <param name="FilePath"></param>
		/// <param name="MembersEdipi"></param>
		/// <param name="MembersFirstName"></param>
		/// <param name="MembersLastName"></param>
		/// <param name="MembersMI"></param>
		/// <returns></returns>
		public static async Task UpdateCertifiedPackageAsync(int DiaryID, string UDNumber, string FilePath, int MembersEdipi, string MembersFirstName, string MembersLastName, string MembersMI)
		{
			string fileName = UDNumber + '.' + MembersEdipi + ".pdf";
			Task<string> SaveFile = Task.Run(() => FileOperation.CopyFile(fileName, "Certified Package", FilePath));
			string FileSaveLocation = await SaveFile;
			Task UpdateDiaryTable = Task.Run(() => DiaryTable.UpdateUnitDiary(CommandUpdateModel.DiaryUpdate(FileSaveLocation, DiaryID)));
			Task InsertCertifiedPackage = Task.Run(() => CertifiedPackageTable.Insert(CommandInsertModel.InsertCertifiedPackage(DiaryID, UDNumber, MembersEdipi, MembersLastName, MembersFirstName, MembersMI)));
			await UpdateDiaryTable;
			await InsertCertifiedPackage;
			CertifiedPackageList.UpdateList(DiaryID, UserName, InsertDate, FileSaveLocation, MembersEdipi, MembersLastName, MembersFirstName, MembersMI);
			return;
		}

		public static async Task UpdateUserAsync()
		{
			Task UserUpdate = Task.Run (() => UserTable.UpdateUser(CommandUpdateModel.UserUpdate(UserSettings.BackgroundString, UserSettings.PrimaryColor, UserSettings.SecondaryColor, UserSettings.UseDarkTheme.ToString())));
			await UserUpdate;
			UserSettings.SettingsRefresh();
		}

		public static async Task InsertMarineInfoAsync(string MembersEdipi, string MembersLastName, string MembersFirstName, string MembersMI)
		{
			Task InsertMarineInfo = Task.Run(() => MarineTable.InsertMarine(CommandInsertModel.InsertMarine(MembersEdipi, MembersLastName, MembersFirstName, MembersMI)));
			await InsertMarineInfo;
		}

		public static async Task InsertDocumentAsync(string DateOfDoc, string MembersEdipi, string FilePath)
		{
			string fileName = CommandModel.GetDocTypeContext() + "." + MembersEdipi + "." + CommandModel.GetSectionContext() + "." + DateOfDoc + ".pdf";
			Task<string> SaveFile = Task.Run(() => FileOperation.CopyFile(fileName, "Member Documents"+ @"\" + MembersEdipi, FilePath));
			string FileSaveLocation = await SaveFile;
			Task InsertDocument = Task.Run(() => DocumentTable.InsertDocument(CommandInsertModel.InsertDocument(MembersEdipi, DateOfDoc, FileSaveLocation)));
			await InsertDocument;
		}

		public static async Task InsertDateAsync()
		{
			await DateTable.InsertDate(CommandInsertModel.InsertDate());
			DateContextList.Add(CommandModel.GetDateContext());
			return;
		}

		public static async Task InsertSectionAsync()
		{
			await SectionTable.InsertSection(CommandInsertModel.InsertSection());
			SectionContextList.Add(CommandModel.GetSectionContext());
			return;
		}

		public static async Task InsertDocTypesAsync()
		{
			await DocTypeTable.InsertDocType(CommandInsertModel.InsertDocType());
			DocTypeContextList.Add(CommandModel.GetDocTypeContext());
			return;
		}

		public static async Task InsertUserAsync()
		{
			await UserTable.UpdateUser(CommandInsertModel.InsertUser());
			return;
		}

		/// <summary>
		/// User Chooses a file
		/// </summary>
		/// <returns>File Path</returns>
		public static string GetFile()
		{
			string FilePath = FileOperation.ChooseFile();
			return FilePath;
		}

		/// <summary>
		/// Downloads the file to the chosen location
		/// </summary>
		/// <param name="File"></param>
		public static void DownloadFile(string File)
		{
			FileOperation.DownloadFile(File);
		}

		public static void PrepDatabase()
		{
			CreateDiaryTable();
			CreateCertifiedPackageTable();
			CreateDocumentTable();
			CreateMarineTable();
			CreateDateTable();
			CreateSectionTable();
			CreateDocTypeTable();
			CreateUserTable();
		}

		private static async void CreateDiaryTable()
		{
			await Database.CreateTable(CommandCreateModel.CreateDiaryTable());
		}

		private static async void CreateCertifiedPackageTable()
		{
			await Database.CreateTable(CommandCreateModel.CreateCertifiedPackageTable());
		}

		private static async void CreateDocumentTable()
		{
			await Database.CreateTable(CommandCreateModel.CreateDocumentTable());
		}

		private static async void CreateMarineTable()
		{
			await Database.CreateTable(CommandCreateModel.CreateMarineTable());
		}

		private static async void CreateDateTable()
		{
			await Database.CreateTable(CommandCreateModel.CreateDateTable());
		}

		private static async void CreateSectionTable()
		{
			await Database.CreateTable(CommandCreateModel.CreateSectionTable());
		}

		private static async void CreateDocTypeTable()
		{
			await Database.CreateTable(CommandCreateModel.CreateDocTypeTable());
		}

		private static async void CreateUserTable()
		{
			await Database.CreateTable(CommandCreateModel.CreateUserTable());
		}

	}
}
