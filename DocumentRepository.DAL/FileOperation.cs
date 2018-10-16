using DocumentRepository.Models;
using Microsoft.VisualBasic.FileIO;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocumentRepository.DAL
{
	public class FileOperation
	{
		public static string ChooseFile()
		{
			string filePath = null;

			OpenFileDialog openFile = new OpenFileDialog
			{
				RestoreDirectory = false
			};

			if (openFile.ShowDialog() != DialogResult.Cancel)
			{
				filePath = openFile.FileName;
			}
			return filePath;
		}

		private static string TargetPath()
		{
			string RepositoryLocation = AppSettings.RepositoryPath;
			return RepositoryLocation;
		}

		public static string CopyFile(string NewFileName, string FileType, string filePath)
		{
			string Year = CommandModel.GetDateContext();
			string Repository = TargetPath();
			string finalPath = null;

			if (!Directory.Exists(Repository))
			{
				Directory.CreateDirectory(Repository);
			}
			if (filePath != null)
			{
				string fileName = Path.GetFileName(filePath);
				finalPath = Repository + @"\" + FileType + @"\" + Year + @"\" + NewFileName;
				FileSystem.CopyFile(filePath, finalPath, UIOption.OnlyErrorDialogs, UICancelOption.DoNothing);
			}
			return finalPath;
		}

		public static void DownloadFile(string StoredFile)
		{
			string filePath = Path.GetFullPath(StoredFile);
			string fileName = Path.GetFileName(StoredFile);
			string savePath = null;

			FolderBrowserDialog folder = new FolderBrowserDialog()
			{
				Description = "Select a Folder to Download Diary",
				ShowNewFolderButton = true
			};

			if (folder.ShowDialog() == DialogResult.OK)
			{
				savePath = folder.SelectedPath;
				string finalPath = savePath + "\\" + fileName;
				FileSystem.CopyFile(filePath, finalPath);
			}
		}

		public static Task DownloadRepository()
		{
			string AppDataLocal = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
			string RepositoryDownLoad = Path.GetFullPath(AppDataLocal + @"\IDR");
			if (!Directory.Exists(RepositoryDownLoad))
			{
				Directory.CreateDirectory(RepositoryDownLoad);
			}
			string RepositoryLocation = Path.GetFullPath(@"\\mcuscljnfs44.mcdsus.mcds.usmc.mil\IPAC\IDR\Application");

			FileSystem.CopyDirectory(RepositoryLocation, RepositoryDownLoad, true);
			return Task.CompletedTask;
		}
	}
}
