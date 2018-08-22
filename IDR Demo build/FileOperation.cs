using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;

namespace IDR_Demo_build
{
	class FileOperation
	{
		public string ChooseFile()
		{
			string filePath = null;

			OpenFileDialog openFile = new OpenFileDialog
			{
				InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
				RestoreDirectory = true
			};

			if (openFile.ShowDialog() != DialogResult.Cancel)
			{
				filePath = openFile.FileName;
			}
			return filePath;
		}

		private static string TargetPath()
		{
			ConnectionReader connectionReader = new ConnectionReader();

			string RepositoryLocation = connectionReader.RepositoryPath;

			return RepositoryLocation;
		}

		public void CopyFile(string NewFileName, string filePath)
		{
			string Year = DateTime.Now.Year.ToString();
			string Repository = TargetPath();

			if (!Directory.Exists(Repository))
			{
				Directory.CreateDirectory(Repository);
			}
			if (filePath != null)
			{
				string fileName = Path.GetFileName(filePath);
				string finalPath = Repository + @"\" + Year + @"\" + NewFileName;
				FileSystem.CopyFile(filePath, finalPath, UIOption.AllDialogs);
			}
			return;
		}

	}
}
