using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocumentRepository.DAL
{
	public class DateTable
	{
		public static async Task<string> InsertDate(string Command)
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
				MessageBox.Show("An error occured adding the date: " + ex.Message.ToString(), "Insert Statement Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
			return "Insert Successful";
		}

		public static async Task<IList<string>> ReadDateTable(string SQLCommand)
		{
			IList<string> DateList = new List<string>();
			SQLiteConnection connection = await Database.Connect();
			try
			{
				using (SQLiteCommand cmd = new SQLiteCommand(SQLCommand, connection))
				{
					using (SQLiteDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							DateList.Add(reader.GetInt32(0).ToString());
						}
						reader.Close();
					}
					connection.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("An Error ocurred reading the Database: " + ex.Message.ToString(), "Read Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return DateList;
		}
	}
}
