using DocumentRepository.Core;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace IDR_Demo_build.Pages
{
	/// <summary>
	/// Interaction logic for DiaryView.xaml
	/// </summary>
	public partial class DiaryView : Page
	{
		public DiaryView()
		{
			InitializeComponent();
			DiariesGrid.ItemsSource = DiaryPager.SetPaging(DiaryList.Uploaded(), 20).DefaultView;
			DiarySearch.SetRecordsToShow(20);
			int[] RecordsToDisplay = { 20, 50, 100, 500, 1000 };
			foreach (int RecordGroup in RecordsToDisplay)
			{
				Records.Items.Add(RecordGroup);
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
				PdfView.OpenFile(row["UploadLocation"].ToString());
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
			int PagedNumber = RecordsDisplayed * (DiaryPager.PageIndex + 1);
			if (PagedNumber > DiaryList.Uploaded().Count)
			{
				PagedNumber = DiaryList.Uploaded().Count;
			}
			NumberDisplay.Text = "Showing " + PagedNumber + " of " + DiaryList.Uploaded().Count;
		}

		private void First_Click(object sender, RoutedEventArgs e)
		{
			DiariesGrid.ItemsSource = DiaryPager.First(DiaryList.Uploaded(), RecordsDisplayed).DefaultView;
			PageNumberDisplay();
		}

		private void Previous_Click(object sender, RoutedEventArgs e)
		{
			DiariesGrid.ItemsSource = DiaryPager.Previous(DiaryList.Uploaded(), RecordsDisplayed).DefaultView;
			PageNumberDisplay();
		}

		private void Next_Click(object sender, RoutedEventArgs e)
		{
			DiariesGrid.ItemsSource = DiaryPager.Next(DiaryList.Uploaded(), RecordsDisplayed).DefaultView;
			PageNumberDisplay();
		}

		private void Last_Click(object sender, RoutedEventArgs e)
		{
			DiariesGrid.ItemsSource = DiaryPager.Last(DiaryList.Uploaded(), RecordsDisplayed).DefaultView;
			PageNumberDisplay();
		}

		private void Records_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			RecordsDisplayed = Convert.ToInt32(Records.SelectedItem);
			DiariesGrid.ItemsSource = DiaryPager.First(DiaryList.Uploaded(), RecordsDisplayed).DefaultView;
			PageNumberDisplay();
		}

		private void DiaryNumberSearch_TextChanged(object sender, TextChangedEventArgs e)
		{
			int UDNumber = Converter(DiaryNumberSearch.Text);
			DiariesGrid.ItemsSource = DiarySearch.ByUploaded(UDNumber).DefaultView;
		}

		private void CertEdipiSearch_TextChanged(object sender, TextChangedEventArgs e)
		{
			int CertEdipi = Converter(CertEdipiSearch.Text);
			DiariesGrid.ItemsSource = DiarySearch.ByUploadedCert(CertEdipi).DefaultView;
		}

		private void CertLastNameSearch_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (DiaryNumberSearch.Text != "")
			{
				int UDNumber = Converter(DiaryNumberSearch.Text);
				DiariesGrid.ItemsSource = DiarySearch.ByUploaded(UDNumber, CertLastNameSearch.Text).DefaultView;
			}
			else
			{
				DiariesGrid.ItemsSource = DiarySearch.ByUploadedCert(CertLastNameSearch.Text).DefaultView;
			}
		}

		private void UploaderNameSearch_TextChanged(object sender, TextChangedEventArgs e)
		{
			if(DiaryNumberSearch.Text != "")
			{
				int UDNumber = Converter(DiaryNumberSearch.Text);
				DiariesGrid.ItemsSource = DiarySearch.ByUploader(UDNumber, UploaderNameSearch.Text).DefaultView;
			}
			else
			{
				DiariesGrid.ItemsSource = DiarySearch.ByUploader(UploaderNameSearch.Text).DefaultView;
			}
		}

		private void DownloadButton_Click(object sender, RoutedEventArgs e)
		{
			if(UploadedFile != null)
			{
				Repository.DownloadFile(UploadedFile);
			}
		}
	}
}
