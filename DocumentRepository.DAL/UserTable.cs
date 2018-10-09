using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;
using DocumentRepository.Models;

namespace DocumentRepository.DAL
{
	public class UserTable
	{
		public static async Task<string> UpdateUser(string Command)
		{
			SQLiteConnection connection = await Database.Connect();
			try
			{
				SQLiteCommand cmd = connection.CreateCommand();
				SQLiteTransaction transaction = connection.BeginTransaction();
				cmd.CommandText = Command;
				cmd.ExecuteNonQuery();
				transaction.Commit();
				cmd.Dispose();
				connection.Dispose();
			}
			catch (Exception ex)
			{
				MessageBox.Show("An error occured adding the User Settings: " + ex.Message.ToString(), "Insert Statement Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
			return "Insert Successful";
		}

		public static async Task<User> ReadUserTable(string SQLCommand)
		{
			User userSettings = new User();
			SQLiteConnection connection = await Database.Connect();
			try
			{
				using (SQLiteCommand cmd = new SQLiteCommand(SQLCommand, connection))
				{
					using (SQLiteDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							userSettings.UserName = reader.GetString(0);
							userSettings.Section = reader.GetString(1);
							userSettings.Background = reader.GetString(2);
							userSettings.DateJoined = reader.GetString(3);
							userSettings.PrimaryColor = reader.GetString(4);
							userSettings.SecondaryColor = reader.GetString(5);
							userSettings.UsesLightTheme = reader.GetString(6);	
						}
					}
					connection.Dispose();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("An Error ocurred reading the Database: " + ex.Message.ToString(), "Read Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return userSettings;
		}
	}
}
