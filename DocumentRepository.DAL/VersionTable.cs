using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentRepository.Models;
using System.Data.SQLite;
using System.Windows.Forms;

namespace DocumentRepository.DAL
{
	public class VersionTable
	{
		public static async Task<string> ReadVersion(string SQLCommand)
		{
			string version = "1.0.0";
			SQLiteConnection connection = await Database.Connect();
			try
			{
				using (SQLiteCommand cmd = new SQLiteCommand(SQLCommand, connection))
				{
					using(SQLiteDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							version = reader.GetString(0);
						}
						reader.Close();
					}
					connection.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("An error occured reading the Version: " + ex.Message.ToString(), "Read Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return version;
		}
	}
}
