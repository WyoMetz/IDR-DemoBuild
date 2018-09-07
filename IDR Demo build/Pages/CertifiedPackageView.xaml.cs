using DocumentRepository.Core;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
namespace IDR_Demo_build.Pages
{
	/// <summary>
	/// Interaction logic for CertifiedPackageView.xaml
	/// </summary>
	public partial class CertifiedPackageView : Page
	{
		public CertifiedPackageView()
		{
			InitializeComponent();
			DiariesGrid.ItemsSource = PackagePager.SetPaging(CertifiedPackageList.Uploaded(), 20).DefaultView;
			PackageSearch.SetRecordsToShow(20);
			int[] RecordsToDisplay = { 20, 50, 100, 500, 1000 };
			foreach (int Record in RecordsToDisplay)
			{
				Records.Items.Add(Record);
			}
			Records.SelectedItem = 20;
			RecordsDisplayed = Convert.ToInt32(Records.SelectedItem);
			PageNumberDisplay();
		}

		int RecordsDisplayed { get; set; }
		string UploadedFile { get; set; }

		private void DiariesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (DiariesGrid.SelectedIndex > -1)
			{
				DataRowView row = (DataRowView)DiariesGrid.SelectedItems[0];
				PdfView.PdfPath = row["UploadLocation"].ToString();
				UploadedFile = row["UploadLocation"].ToString();
			}
		}

		public int Converter(string Number)
		{
			bool sorter = Int32.TryParse(Number, out int number);
			if (!sorter) { number = 0; }
			return number;
		}

		public void PageNumberDisplay()
		{
			int PagedNumber = RecordsDisplayed * (PackagePager.PageIndex + 1);
			if (PagedNumber > CertifiedPackageList.Uploaded().Count)
			{
				PagedNumber = CertifiedPackageList.Uploaded().Count;
			}
			NumberDisplay.Text = "Showing " + PagedNumber + " of " + CertifiedPackageList.Uploaded().Count;
		}

		private void First_Click(object sender, RoutedEventArgs e)
		{
			DiariesGrid.ItemsSource = PackagePager.First(CertifiedPackageList.Uploaded(), RecordsDisplayed).DefaultView;
			PageNumberDisplay();
		}

		private void Previous_Click(object sender, RoutedEventArgs e)
		{
			DiariesGrid.ItemsSource = PackagePager.Previous(CertifiedPackageList.Uploaded(), RecordsDisplayed).DefaultView;
			PageNumberDisplay();
		}

		private void Next_Click(object sender, RoutedEventArgs e)
		{
			DiariesGrid.ItemsSource = PackagePager.Next(CertifiedPackageList.Uploaded(), RecordsDisplayed).DefaultView;
			PageNumberDisplay();
		}

		private void Last_Click(object sender, RoutedEventArgs e)
		{
			DiariesGrid.ItemsSource = PackagePager.Last(CertifiedPackageList.Uploaded(), RecordsDisplayed).DefaultView;
			PageNumberDisplay();
		}

		private void Records_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			RecordsDisplayed = Convert.ToInt32(Records.SelectedItem);
			DiariesGrid.ItemsSource = PackagePager.First(CertifiedPackageList.Uploaded(), RecordsDisplayed).DefaultView;
			PageNumberDisplay();
		}

		private void DiaryNumberSearch_TextChanged(object sender, TextChangedEventArgs e)
		{
			int UDNumber = Converter(DiaryNumberSearch.Text);
			DiariesGrid.ItemsSource = PackageSearch.ByUploaded(UDNumber).DefaultView;
		}

		private void CertEdipiSearch_TextChanged(object sender, TextChangedEventArgs e)
		{
			int CertEdipi = Converter(CertEdipiSearch.Text);
			DiariesGrid.ItemsSource = PackageSearch.ByUploadCert(CertEdipi).DefaultView;
		}

		private void CertLastNameSearch_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (DiaryNumberSearch.Text != "")
			{
				int UDNumber = Converter(DiaryNumberSearch.Text);
				DiariesGrid.ItemsSource = PackageSearch.ByUploaded(UDNumber, CertLastNameSearch.Text).DefaultView;
			}
			else
			{
				DiariesGrid.ItemsSource = PackageSearch.ByUploadCert(CertLastNameSearch.Text).DefaultView;
			}
		}

		private void DownloadButton_Click(object sender, RoutedEventArgs e)
		{
			if (UploadedFile != null)
			{
				Repository.DownloadFile(UploadedFile);
			}
		}

		private void MemberLastNameSearch_TextChanged(object sender, TextChangedEventArgs e)
		{
			DiariesGrid.ItemsSource = PackageSearch.ByMember(MemberLastNameSearch.Text).DefaultView;
		}

		private void MembersEdipiSearch_TextChanged(object sender, TextChangedEventArgs e)
		{
			int Edipi = Converter(MembersEdipiSearch.Text);
			DiariesGrid.ItemsSource = PackageSearch.ByMember(Edipi).DefaultView;
		}
	}
}
