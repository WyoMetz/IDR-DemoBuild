using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocumentRepository.DAL
{
	public class DocTypeTable
	{
		public static async Task<string> InsertDocType(string Command)
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
				MessageBox.Show("An error occured adding the Document Type: " + ex.Message.ToString(), "Insert Statement Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
			return "Insert Successful";
		}

		public static async Task<IList<string>> ReadDocTypeTable(string SQLCommand)
		{
			IList<string> DocList = new List<string>();
			SQLiteConnection connection = await Database.Connect();
			try
			{
				using (SQLiteCommand cmd = new SQLiteCommand(SQLCommand, connection))
				{
					using (SQLiteDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							DocList.Add(reader.GetString(0));
						}
						reader.Close();
					}
					connection.Dispose();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("An error occured reading the DocType Table: " + ex.Message.ToString(), "Insert Statement Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
			return DocList;
		}
	}
}
