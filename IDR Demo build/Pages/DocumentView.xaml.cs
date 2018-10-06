using DocumentRepository.Core;
using System;
using System.Windows;
using System.Windows.Controls;

namespace IDR_Demo_build.Pages
{
	/// <summary>
	/// Interaction logic for DocumentView.xaml
	/// </summary>
	public partial class DocumentView : Page
	{
		string UploadedFile { get; set; }
		string MarineFirstName { get; set; }
		string MarineLastName { get; set; }
		int MarineEDIPI { get; set; }
		string TestSource { get; set; }

		public DocumentView()
		{
			InitializeComponent();
			MarineList.PrepareList();
			DocumentList.PrepareList();
			MarineDocumentList.PrepareList();
			DocTypeContextList.PrepareList();
			MarineTree.ItemsSource = MarineDocumentList.MarineDocuments;
		}

		private void Update()
		{
			MarineFirstName = FirstNameSearch.Text;
			MarineLastName = LastNameSearch.Text;
			bool EdipiNumber = Int32.TryParse(EdipiSearch.Text, out int ParsedEDIPI);
			if (!EdipiNumber) { ParsedEDIPI = 0; }
			MarineEDIPI = ParsedEDIPI;
		}

		private void GetItem()
		{
			UploadedFile = TextValue.Text.ToString();
			if (!string.IsNullOrWhiteSpace(UploadedFile))
			{
				PdfView.OpenFile(UploadedFile);
			}
		}

		private void Search()
		{
			MarineTree.ItemsSource = null;
			if (MarineEDIPI != 0)
			{
				if (string.IsNullOrWhiteSpace(MarineLastName) && string.IsNullOrWhiteSpace(MarineFirstName))
				{
					MarineTree.ItemsSource = MarineDocumentSearch.ByEDIPI(MarineEDIPI);
				}
				if (!string.IsNullOrWhiteSpace(MarineLastName) && string.IsNullOrWhiteSpace(MarineFirstName))
				{
					MarineTree.ItemsSource = MarineDocumentSearch.ByEDIPI(MarineEDIPI, MarineLastName);
				}
				if (!string.IsNullOrWhiteSpace(MarineLastName) && !string.IsNullOrWhiteSpace(MarineFirstName))
				{
					MarineTree.ItemsSource = MarineDocumentSearch.ByEDIPI(MarineEDIPI, MarineLastName, MarineFirstName);
				}
			}
			if (MarineEDIPI == 0)
			{
				if (!string.IsNullOrWhiteSpace(MarineLastName) && string.IsNullOrWhiteSpace(MarineFirstName))
				{
					MarineTree.ItemsSource = MarineDocumentSearch.ByLastName(MarineLastName);
				}
				if (!string.IsNullOrWhiteSpace(MarineFirstName) && string.IsNullOrWhiteSpace(MarineLastName))
				{
					MarineTree.ItemsSource = MarineDocumentSearch.ByFirstName(MarineFirstName);
				}
				if (!string.IsNullOrWhiteSpace(MarineLastName) && !string.IsNullOrWhiteSpace(MarineFirstName))
				{
					MarineTree.ItemsSource = MarineDocumentSearch.ByLastName(MarineLastName, MarineFirstName);
				}
			}
		}

		private void DownloadButton_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(UploadedFile))
			{
				Repository.DownloadFile(UploadedFile);
			}
		}

		private void FirstNameSearch_TextChanged(object sender, TextChangedEventArgs e)
		{
			Update();
			Search();
		}

		private void LastNameSearch_TextChanged(object sender, TextChangedEventArgs e)
		{
			Update();
			Search();
		}

		private void EdipiSearch_TextChanged(object sender, TextChangedEventArgs e)
		{
			Update();
			Search();
		}

		private void MarineTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			GetItem();
		}

		private void MarineTree_Selected(object sender, RoutedEventArgs e)
		{
			TestSource = TextValue.Text.ToString();
		}
	}
}
