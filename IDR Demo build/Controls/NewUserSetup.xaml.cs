using DocumentRepository.Core;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using IDR_Demo_build;
using DocumentRepository.Models;

namespace IDR_Demo_build.Controls
{
	/// <summary>
	/// Interaction logic for NewUserSetup.xaml
	/// </summary>
	public partial class NewUserSetup : UserControl
	{
		private static string ImageName { get; set; }
		public static string PrimaryColor { get; set; }
		public static string SecondaryColor { get; set; }
		public static bool UseLightTheme { get; set; }
		public static string Section { get; set; }

		public NewUserSetup()
		{
			InitializeComponent();
			BackgroundSet();
			SectionContextList.PrepareSectionContext();
			SectionBox.ItemsSource = SectionContextList.Sections;
			if (!UserSettings.NewUser())
			{
				HeaderBlock.Text = "Settings Menu";
				SubheaderBlock.Text = "Some Settings may not take effect until the App is refreshed.";
			}
		}

		private void BackgroundSet()
		{
			BitmapImage newImage = new BitmapImage(UserSettings.Background);
			BackgroundImage.Source = newImage;
		}

		private void ImageSelect_Click(object sender, RoutedEventArgs e)
		{
			string filePath = Repository.GetFile();
			if(filePath != null)
			{
				UserSettings.BackgroundString = filePath;
				UserSettings.Background = new Uri(filePath, UriKind.RelativeOrAbsolute);
				BackgroundSet();
				UserSettings.BackgroundUpdate();
			}
		}

		private void updateText()
		{
			SectionText.Text = UserSettings.Section;
			PrimaryColorText.Text = UserSettings.PrimaryColor;
			SecondaryColorText.Text = UserSettings.SecondaryColor;
		}

		private void setPalette()
		{
			if (string.IsNullOrWhiteSpace(PrimaryColor))
			{
				PrimaryColor = UserSettings.PrimaryColor;
			}
			if (string.IsNullOrEmpty(SecondaryColor))
			{
				SecondaryColor = UserSettings.SecondaryColor;
			}
			var PrimarySwatch = new MaterialDesignColors.SwatchesProvider().Swatches.Single(o => o.Name == PrimaryColor);
			new PaletteHelper().ReplacePrimaryColor(PrimarySwatch);
			var SecondarySwatch = new MaterialDesignColors.SwatchesProvider().Swatches.Single(o => o.Name == SecondaryColor);
			new PaletteHelper().ReplaceAccentColor(SecondarySwatch);
		}

		private void ThemeSwitch_Click(object sender, RoutedEventArgs e)
		{
			if (ThemeSwitch.IsChecked == true)
			{
				new PaletteHelper().SetLightDark(false);
				UserSettings.UseDarkTheme = true;
			}
			if (ThemeSwitch.IsChecked == false)
			{
				new PaletteHelper().SetLightDark(true);
				UserSettings.UseDarkTheme = false;
			}
		}

		private void SectionBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Section = SectionBox.SelectedItem.ToString();
			UserSettings.Section = Section;
			CommandModel.SetSectionContext(Section);
			updateText();
		}

		private void SecondaryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			string Item = SecondaryList.SelectedItem.ToString();
			var pieces = Item.Split(':');
			SecondaryColor = pieces[1].TrimStart(' ');
			UserSettings.SecondaryColor = SecondaryColor;
			setPalette();
			updateText();
		}

		private void PrimaryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			string Item = PrimaryList.SelectedItem.ToString();
			var pieces = Item.Split(':');
			PrimaryColor = pieces[1].TrimStart(' ');
			UserSettings.PrimaryColor = PrimaryColor;
			setPalette();
			updateText();
		}

		private async void Save_Click(object sender, RoutedEventArgs e)
		{
			await Repository.UpdateUserAsync();
		}
	}
}
