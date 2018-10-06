using DocumentRepository.Core;
using DocumentRepository.DAL;
using DocumentRepository.Models;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows;

namespace IDR_Demo_build
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		static string CurrentVersion = "1.0.1";
		/// <summary>
		/// Loads the Background image and Stores the Left Menu Navigation Items
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();

			//Defines and sets the Image block with the Tag BackgroundImage
			BitmapImage newImage = new BitmapImage(AppSettings.ImageUri);
			Image outputImage = new Image
			{
				Source = newImage
			};
			BackgroundImage.Source = newImage;

			string UserName = AppSettings.User;
			CommandModel.SetUserContext(AppSettings.User);
			CommandModel.SetDateContext(DateTime.Now.Year.ToString());
			DiaryList.PrepareList();
			CertifiedPackageList.PreparePackages();
			CheckVersion();

			//Navigates to the Welcome Page
			ContentFrame.Navigate(new Uri("Pages/WelcomePage.xaml", UriKind.Relative));
			HeaderBlock.Text = "Welcome " + UserName;
		}

		private async void CheckVersion()
		{
			string version = await Repository.GetVersionAsync();
			if (CurrentVersion != version)
			{
				MessageBox.Show("An Updated Version has been released. \n Please download the newest version using the Document Repository Downloader provided by Quality Control. \n If you experience any issues or believe you are seeing this message in error, Please contact the Quality Control Branch.",
					"New Version Available",
					MessageBoxButton.OK,
					MessageBoxImage.Stop);
				this.Close();
			}
		}

		private void HomeNav_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			ContentFrame.Navigate(new Uri("Pages/WelcomePage.xaml", UriKind.Relative));
			HeaderBlock.Text = "Main Menu";
			LeftMenu.IsLeftDrawerOpen = false;
		}

		private void UploadPackage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			ContentFrame.Navigate(new Uri("Pages/CertifiedPackageUpload.xaml", UriKind.Relative));
			HeaderBlock.Text = "Upload a New Certified Package";
			LeftMenu.IsLeftDrawerOpen = false;
		}

		private void UploadDiary_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			ContentFrame.Navigate(new Uri("Pages/DiaryUpload.xaml", UriKind.Relative));
			HeaderBlock.Text = "Upload a New Diary";
			LeftMenu.IsLeftDrawerOpen = false;
		}

		private void ViewDiary_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			ContentFrame.Navigate(new Uri("Pages/DiaryView.xaml", UriKind.Relative));
			HeaderBlock.Text = "View Uploaded Diaries";
			LeftMenu.IsLeftDrawerOpen = false;
		}

		private void ViewPackages_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			ContentFrame.Navigate(new Uri("Pages/CertifiedPackageView.xaml", UriKind.Relative));
			HeaderBlock.Text = "View Uploaded Packages";
			LeftMenu.IsLeftDrawerOpen = false;
		}

		private void DocumentUpload_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			ContentFrame.Navigate(new Uri("Pages/DocumentUpload.xaml", UriKind.Relative));
			HeaderBlock.Text = "Upload Member Documents";
			LeftMenu.IsLeftDrawerOpen = false;
		}

		private void ViewDocuments_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			ContentFrame.Navigate(new Uri("Pages/DocumentView.xaml", UriKind.RelativeOrAbsolute));
			HeaderBlock.Text = "View Member Documents";
			LeftMenu.IsLeftDrawerOpen = false;
		}

		private void TopBar_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if(e.ChangedButton == MouseButton.Left)
			{
				this.DragMove();
			}
		}

		private void Minimize_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.WindowState = WindowState.Minimized;
		}

		private void Restore_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.WindowState = WindowState.Normal;
			Restore.Visibility = Visibility.Collapsed;
			Maximize.Visibility = Visibility.Visible;
		}

		private void Maximize_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.WindowState = WindowState.Maximized;
			Maximize.Visibility = Visibility.Collapsed;
			Restore.Visibility = Visibility.Visible;
		}

		private void CloseButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.Close();
		}

	}
}
