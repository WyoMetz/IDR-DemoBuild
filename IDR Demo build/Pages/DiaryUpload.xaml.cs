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
using System.IO;
using System.Windows.Forms;

namespace IDR_Demo_build.Pages
{
    /// <summary>
    /// Interaction logic for DiaryUpload.xaml
    /// </summary>
    public partial class DiaryUpload : Page
    {
        public DiaryUpload()
        {
            InitializeComponent();
		}

		private void GetDiary_Click(object sender, RoutedEventArgs e)
		{

			FileOperation fileOperation = new FileOperation();

			string FilePath = fileOperation.ChooseFile();
			string fullPath = Path.GetFullPath(FilePath);

			DiaryFilePath.Text = Path.GetFileName(FilePath);

			FileView.Source = new Uri(fullPath, UriKind.Absolute);

		}
	}
}
