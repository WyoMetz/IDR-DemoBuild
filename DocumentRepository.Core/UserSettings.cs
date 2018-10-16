using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentRepository.Models;
using DocumentRepository.DAL;
using System.IO;
using System.Windows.Media.Imaging;
using System.ComponentModel;

namespace DocumentRepository.Core
{
	public class UserSettings
	{
		private static User Settings { get; set; }
		private static string UserName { get; set; }
		public static string BackgroundString { get; set; }
		public static string Section { get; set; }
		public static string PrimaryColor { get; set; }
		public static string SecondaryColor { get; set; }
		public static bool UseDarkTheme { get; set; }
		public static Uri Background;
		public static BitmapImage BackgroundBitmap { get; set; }

		private static async void PrepareList()
		{
			CommandModel.SetUserContext(AppSettings.User);
			if (Settings == null)
			{
				Settings = await Repository.GetUserSettings();
			}
		}

		public static void SettingsRefresh()
		{
			Settings = null;
			PrepareList();
		}

		public static void SetUp()
		{
			PrepareList();
			UserName = Settings.UserName;
			Section = Settings.Section;
			BackgroundString = Settings.Background;
			CommandModel.SetSectionContext(Settings.Section);
			if(!string.IsNullOrWhiteSpace(UserName))
			{
				BackgroundUpdate();
				PrimaryColor = Settings.PrimaryColor;
				SecondaryColor = Settings.SecondaryColor;
				if(Settings.UsesLightTheme == "False")
				{
					UseDarkTheme = false;
				}
				else
				{
					UseDarkTheme = true;
				}
			}
			else
			{
				BackgroundString = AppSettings.ImageUri.ToString();
				Background = AppSettings.ImageUri;
				BackgroundBitmap = new BitmapImage(Background);
				PrimaryColor = "blue";
				SecondaryColor = "red";
				UseDarkTheme = true;
			}
		}

		public static void BackgroundUpdate()
		{
			Background = new Uri(BackgroundString, UriKind.RelativeOrAbsolute);
			BackgroundBitmap = new BitmapImage(Background);
		}

		public static bool NewUser()
		{
			if (string.IsNullOrWhiteSpace(UserName))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
