using System;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocumentRepository.DAL
{
	public class Database
	{
		public static async Task<SQLiteConnection> Connect()
		{
			string DbLocation = AppSettings.DatabasePath;
			SQLiteConnection DbConnection = new SQLiteConnection("Data Source=" + DbLocation + ";Version=3;new=False;datetimeformat=CurrentCulture;");
			try
			{
				await DbConnection.OpenAsync();
			}
			catch (Exception ex)
			{
				MessageBox.Show("The Connection to the Database Failed: " + ex.ToString(), "Database Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			return new SQLiteConnection(DbConnection);
		}

		/// <summary>
		/// Internal Method to Create Tables. Should only be used in Intialization.
		/// </summary>
		/// <returns>string statement on Success or Error statement on failure.</returns>
		public static async Task<string> CreateTable(string Table)
		{
			SQLiteConnection connection = await Connect();
			try
			{
				using (SQLiteCommand cmd = new SQLiteCommand(Table, connection))
				{
					cmd.ExecuteNonQuery();
					connection.Close();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("An Error ocurred while attempting to create the Table " + ex.ToString(), "Error Creating Table", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return "Table Creation Failed.";
			}
			return "Table Created Successfully";
		}
	}
}
