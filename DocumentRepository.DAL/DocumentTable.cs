using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentRepository.Models;
using System.Linq;

namespace DocumentRepository.DAL
{
	public class DocumentTable
	{
		public static async Task<string> InsertDocument(string Command)
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
				MessageBox.Show("An error occured adding the Document: " + ex.Message.ToString(), "Insert Statement Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return "Insert Successful";
		}

		public static async Task<IList<Document>> ReadDocumentTable(string SQLCommand)
		{
			IList<Document> documents = new List<Document>();
			SQLiteConnection connection = await Database.Connect();
			try
			{
				using (SQLiteCommand cmd = new SQLiteCommand(SQLCommand, connection))
				{
					using (SQLiteDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							documents.Add(new Document
							{
								EDIPI = reader.GetInt32(0),
								DocType = reader.GetString(1),
								Section = reader.GetString(2),
								UploadDate = reader.GetDateTime(3),
								UploadUser = reader.GetString(4),
								DateOfDoc = reader.GetDateTime(5),
								UploadLocation = reader.GetString(6),
								//skip 7 as its an EDIPI Duplicate
								LastName = reader.GetString(8),
								FirstName = reader.GetString(9),
								MI = reader.GetString(10)
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
			return documents.OrderBy(o => o.DateOfDoc).ToList();
		}
	}
}
