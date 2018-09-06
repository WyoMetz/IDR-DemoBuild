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
				FileSystem.CopyFile(filePath, finalPath, UIOption.OnlyErrorDialogs);
			}
			return finalPath;
		}
	}
}
