using DocumentRepository.Core;
using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace IDR_Demo_build.Pages
{
	/// <summary>
	/// Interaction logic for DiaryUpload.xaml
	/// </summary>
	public partial class DiaryUpload : Page
	{
		string FilePath { get; set; }
		int DiaryId { get; set; }
		string SelectionNumber { get; set; }

		public DiaryUpload()
		{
			InitializeComponent();

			DiaryGroup.ItemsSource = DiaryPager.SetPaging(DiaryList.NeedUploaded(), 15).DefaultView;
			DiaryList.SetRecordsToShow(15);
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

		private void ChosenDiary_TextChanged(object sender, TextChangedEventArgs e)
		{
			DiaryGroup.ItemsSource = DiaryList.SearchNotUploadedDiaryNumber(ChosenDiary.Text).DefaultView;
		}

		private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			DiaryGroup.ItemsSource = DiaryList.SearchNotUploadedDiaryNumber(SearchBox.Text).DefaultView;
		}

		private void SelectedDiary_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (FilePath != null)
			{
				UploadDiary.IsEnabled = true;
			}
		}

		private void DiaryGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (DiaryGroup.SelectedIndex > -1)
			{
				DataRowView row = (DataRowView)DiaryGroup.SelectedItems[0];
				DiaryId = Int32.Parse(row["DiaryID"].ToString());
				SelectionNumber = row["UDNumber"].ToString();
				SelectedDiary.Text = SelectionNumber;
			}
			if(FilePath != null)
			{
				UploadDiary.IsEnabled = true;
			}
		}

		private async void UploadDiary_Click(object sender, RoutedEventArgs e)
		{
			await Repository.UpdateRegularDiaryAsync(DiaryId, SelectionNumber, FilePath);
			SnackbarThree.MessageQueue.Enqueue("Diary " + SelectionNumber + " has been updated");
			SelectedDiary.Clear();
			SearchBox.Clear();
			ChosenDiary.Clear();
			UploadDiary.IsEnabled = false;
			DiaryGroup.ItemsSource = DiaryPager.SetPaging(DiaryList.NeedUploaded(), 10).DefaultView;
		}
	}
}
