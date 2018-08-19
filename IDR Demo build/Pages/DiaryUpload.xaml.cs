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
			FileView.Source = new Uri("C:\\Users\\metzc\\Documents\\Adobe\\Print Release\\PrintReleaseTemplate.pdf", UriKind.Absolute);

		}
    }
}
