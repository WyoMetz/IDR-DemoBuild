using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocumentRepository.DAL
{
	public class SectionTable
	{
		public static async Task<string> InsertSection(string Command)
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
				MessageBox.Show("An error occured add the section: " + ex.Message.ToString(), "Insert Statement Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return "Insert Successful";
		}

		public static async Task<IList<string>> ReadSectionTable(string SQLCommand)
		{
			IList<string> SectionList = new List<string>();
			SQLiteConnection connection = await Database.Connect();
			try
			{
				using (SQLiteCommand cmd = new SQLiteCommand(SQLCommand, connection))
				{
					using (SQLiteDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							SectionList.Add(reader.GetString(0));
						}
						reader.Close();
					}
					connection.Close();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("An error ocurred: " + ex.Message.ToString(), "Read Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return SectionList;
		}
	}
}
