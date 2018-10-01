using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentRepository.Models;
using System.Linq;

namespace DocumentRepository.DAL
{
	public class MarineTable
	{
		public static async Task<string> InsertMarine(string Command)
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
				MessageBox.Show("An error occured adding the Marine: " + ex.Message.ToString(), "Insert Statement Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return "Insert Successful";
		}

		public static async Task<IList<Marine>> ReadMarine(string SQLCommand)
		{
			IList<Marine> marines = new List<Marine>();
			SQLiteConnection connection = await Database.Connect();
			try
			{
				using (SQLiteCommand cmd = new SQLiteCommand(SQLCommand, connection))
				{
					using (SQLiteDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							marines.Add(new Marine
							{
								EDIPI = reader.GetInt32(0),
								LastName = reader.GetString(1),
								FirstName = reader.GetString(2),
								MI = reader.GetString(3)
							});
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
			return marines.OrderBy(o => o.EDIPI).ToList();
		}
	}
}
