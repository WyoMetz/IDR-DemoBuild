using DocumentRepository.Core;
using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace IDR_Demo_build.Pages
{
	/// <summary>
	/// Interaction logic for CertifiedPackageUpload.xaml
	/// </summary>
	public partial class CertifiedPackageUpload : Page
	{
		string FilePath { get; set; }
		int DiaryId { get; set; }
		string SelectionNumber { get; set; }

		public CertifiedPackageUpload()
		{
			InitializeComponent();

			CertifiedPackageList.SetRecordsToShow(15);
			DiaryGroup.ItemsSource = DiaryPager.SetPaging(CertifiedPackageList.NotUploaded(), 15).DefaultView;
		}

		private void ChooseDiary_Click(object sender, RoutedEventArgs e)
		{
			FilePath = Repository.GetFile();
			if (FilePath != null)
			{
				string fullPath = Path.GetFullPath(FilePath);
				ChosenDiary.Text = Path.GetFileName(FilePath);
				PdfView.PdfPath = fullPath;
			}
		}

		public int Converter(string Text)
		{
			bool sorter = Int32.TryParse(Text, out int number);
			if (!sorter) { number = 0; }
			return number;
		}

		private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			DiaryGroup.ItemsSource = PackageSearch.ByNotUploaded(Converter(SearchBox.Text)).DefaultView;
		}

		private void EdipiSearch_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (SearchBox.Text == "")
			{
				DiaryGroup.ItemsSource = PackageSearch.ByNotUploadedCert(Converter(EdipiSearch.Text)).DefaultView;
			}
			else
			{
				DiaryGroup.ItemsSource = PackageSearch.ByNotUploaded(Converter(SearchBox.Text), Converter(EdipiSearch.Text)).DefaultView;
			}
		}

		private void LastNameSearch_TextChanged(object sender, TextChangedEventArgs e)
		{
			DiaryGroup.ItemsSource = PackageSearch.ByNotUploadedCert(LastNameSearch.Text).DefaultView;
		}

		private void DiaryGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			try
			{
				if (DiaryGroup.SelectedIndex > -1)
				{
					DataRowView row = (DataRowView)DiaryGroup.SelectedItems[0];
					DiaryId = Int32.Parse(row["DiaryID"].ToString());
					SelectionNumber = row["UDNumber"].ToString();
					SelectedDiary.Text = SelectionNumber;
				}
				if (FilePath != null && MembersEDIPI.Text != "" && MembersFirstName.Text != "" && MembersLastName.Text != "")
				{
					UploadDiary.IsEnabled = true;
				}
			}
			catch (Exception ex)
			{
				SelectedDiary.Text = "";
			}
		}

		private void SelectedDiary_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (FilePath != null && MembersEDIPI.Text != "" && MembersFirstName.Text != "" && MembersLastName.Text != "")
			{
				UploadDiary.IsEnabled = true;
			}
		}

		private async void UploadDiary_Click(object sender, RoutedEventArgs e)
		{
			int EDIPI = Int32.Parse(MembersEDIPI.Text);
			string lastName = MembersLastName.Text;
			string firstName = MembersFirstName.Text;
			string MI = MembersMI.Text;
			await Repository.UpdateCertifiedPackageAsync(DiaryId, SelectionNumber, FilePath, EDIPI, firstName, lastName, MI);
			SnackbarThree.MessageQueue.Enqueue("Package " + SelectionNumber + " has been updated");
			FilePath = null;
			MembersEDIPI.Clear();
			MembersFirstName.Clear();
			MembersLastName.Clear();
			MembersMI.Clear();
			UploadDiary.IsEnabled = false;
			DiaryGroup.ItemsSource = DiaryPager.SetPaging(CertifiedPackageList.NotUploaded(), 15).DefaultView;
		}

		private void MembersEDIPI_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (FilePath != null && MembersEDIPI.Text != "" && MembersFirstName.Text != "" && MembersLastName.Text != "")
			{
				UploadDiary.IsEnabled = true;
			}
		}

		private void MembersLastName_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (FilePath != null && MembersEDIPI.Text != "" && MembersFirstName.Text != "" && MembersLastName.Text != "")
			{
				UploadDiary.IsEnabled = true;
			}
		}

		private void MembersFirstName_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (FilePath != null && MembersEDIPI.Text != "" && MembersFirstName.Text != "" && MembersLastName.Text != "")
			{
				UploadDiary.IsEnabled = true;
			}
		}
	}
}
