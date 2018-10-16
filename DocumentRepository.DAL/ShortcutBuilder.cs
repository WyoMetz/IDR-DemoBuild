using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IWshRuntimeLibrary;
using System.IO;

namespace DocumentRepository.DAL
{
	public class ShortcutBuilder
	{
		private static string AppDataLocal = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
		private static string ShortcutTarget = Path.GetFullPath(AppDataLocal + @"\IDR\IDR Demo Build.exe");
		private static string ShortcutLink = Path.GetFullPath(AppDataLocal + @"\IDR\IDR Demo Build.lnk");
		private static string ShortcutLocation = Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Document Repository.lnk");
		private static string AppDirectory = Path.GetFullPath(AppDataLocal + @"\IDR");

		public static Task CreateShortCut()
		{
			WshShell ScriptingShell = new WshShell();
			WshShortcut ShortcutShell = (WshShortcut)ScriptingShell.CreateShortcut(ShortcutLocation);
			ShortcutShell.TargetPath = ShortcutTarget;
			ShortcutShell.IconLocation = ShortcutTarget + ",0";
			ShortcutShell.Description = "Document Repository";
			ShortcutShell.WorkingDirectory = AppDirectory;
			ShortcutShell.Arguments = "";
			ShortcutShell.Save();
			return Task.CompletedTask;
		}
	}
}
