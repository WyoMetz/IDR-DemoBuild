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
using DocumentRepository.Core;
using DocumentRepository.Models;

namespace VisualTests
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			CommandModel.SetDateContext("2018");
			MarineList.PrepareList();
			DocumentList.PrepareList();
			MarineDocumentList.PrepareList();
			TestTree.ItemsSource = MarineDocumentList.MarineDocuments;
		}
	}
}
