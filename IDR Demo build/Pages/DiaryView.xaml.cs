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
using System.Windows.Shapes;
using DocumentRepository.Core;

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
			DiariesGrid.ItemsSource = DiaryPager.SetPaging(DiaryList.Uploaded(), 10).DefaultView;
		}

		private void DiariesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (DiariesGrid.SelectedIndex > -1)
			{
				DataRowView row = (DataRowView)DiariesGrid.SelectedItems[0];
				PdfView.PdfPath = row["UploadLocation"].ToString();
			}
		}
	}
}
