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
using System.Windows.Shapes;
using DocumentRepository.Core;

namespace IDR_Demo_build
{
	/// <summary>
	/// Interaction logic for SettingsWindow.xaml
	/// </summary>
	public partial class SettingsWindow : Window
	{
		public SettingsWindow()
		{
			InitializeComponent();
		}

		private async void Save_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(UserSettings.Section))
			{
				await Repository.UpdateUserAsync();
				Close();
			}
			else
			{
				MessageBox.Show("You must at Least choose a section");
			}
		}
	}
}
