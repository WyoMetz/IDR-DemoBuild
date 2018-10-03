using System;
using System.Collections.Generic;
using System.Data;
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
using DocumentRepository.Core;
using DocumentRepository.Models;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;

namespace IDR_Demo_build.Pages
{
    /// <summary>
    /// Interaction logic for DocumentUpload.xaml
    /// </summary>
    public partial class DocumentUpload : Page
    {
        private static int MarineEDIPI { get; set; }
        private static string MarineLastName { get; set; }
        private static string MarineFirstName { get; set; }
        private static string MarineMI { get; set; }
        private static string DocumentDate { get; set; }

        private static string FilePath { get; set; }

        public DocumentUpload()
        {
            InitializeComponent();
            DocumentList.PrepareList();
            MarineList.PrepareList();
            GetList();
            DocTypeContextList.PrepareList();
            SectionContextList.PrepareSectionContext();

            DocType.ItemsSource = DocTypeContextList.DocTypes;
            Section.ItemsSource = SectionContextList.Sections;

            CheckExecutable();
        }

        private void DialogHost_DialogClosing(object sender, MaterialDesignThemes.Wpf.DialogClosingEventArgs eventArgs)
        {
            if (Equals(eventArgs.Parameter, true))
            {
                MarineEDIPI = Int32.Parse(InputEDIPI.Text);
                MarineLastName = InputLastName.Text;
                MarineFirstName = InputFirstName.Text;
                MarineMI = InputMI.Text;

                MarineList.Add(MarineEDIPI, MarineLastName, MarineFirstName, MarineMI);
                GetList();
                MarineUpdate();
                InsertMarine();
            }
        }

        private async void InsertMarine()
        {
            await Repository.InsertMarineInfoAsync(MarineEDIPI.ToString(), MarineLastName, MarineFirstName, MarineMI);
        }

        private void GetList()
        {
            MarineListBox.ItemsSource = null;
            MarineListBox.ItemsSource = MarineList.Marines;
        }

        private void MarineListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Marine marine = MarineListBox.SelectedItem as Marine;
            MarineEDIPI = marine.EDIPI;
            MarineFirstName = marine.FirstName;
            MarineLastName = marine.LastName;
            MarineMI = marine.MI;
            MarineUpdate();
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            FilePath = Repository.GetFile();
            if (FilePath != null)
            {
                string fullPath = Path.GetFullPath(FilePath);
                SelectedDocument.Text = Path.GetFileName(FilePath);
                PdfView.OpenFile(fullPath);
                CheckExecutable();
            }
        }

        private void DocType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CommandModel.SetDocTypeContext(DocType.SelectedItem.ToString());
            CheckExecutable();
        }

        private void Section_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CommandModel.SetSectionContext(Section.SelectedItem.ToString());
            CheckExecutable();
        }

        private void DocDate_CalendarClosed(object sender, RoutedEventArgs e)
        {
            CheckDate();
            CheckExecutable();
        }

        private async void Upload_Click(object sender, RoutedEventArgs e)
        {
            await Repository.InsertDocumentAsync(DocumentDate, MarineEDIPI.ToString(), FilePath);
            SnackbarThree.MessageQueue.Enqueue("The Document for " + MarineLastName + ",  " + MarineFirstName + " has been Uploaded.");
            ClearValues();
        }

        private void CheckExecutable()
        {
            if(MarineEDIPI != 0 
                && MarineFirstName != null 
                && MarineLastName != null 
                && FilePath != null
                && DocumentDate != null)
            {
                Upload.IsEnabled = true;
            }
            else
            {
                Upload.IsEnabled = false;
            }
        }

        private void ClearValues()
        {
            MarineEDIPI = 0;
            MarineFirstName = null;
            MarineLastName = null;
            MarineMI = null;
            FilePath = null;
            DocumentDate = null;
            MarineUpdate();
            CheckExecutable();
        }

        private void CheckDate()
        {
            try
            {
                DateTime time = DateTime.Parse(DocDate.Text);
                DocumentDate = time.ToShortDateString();
            }
            catch (Exception)
            {
                MessageBox.Show("The Value entered is not a valid Date. Please enter date format as MM/DD/YYYY or use the calendar.");
                DocDate.Text = null;
            }
        }

        private void MarineUpdate()
        {
            MarineEDIPIText.Text = MarineEDIPI.ToString();
            MarineLastNameText.Text = MarineLastName;
            MarineFirstNameText.Text = MarineFirstName;
            MarineMIText.Text = MarineMI;
        }

    }
}
