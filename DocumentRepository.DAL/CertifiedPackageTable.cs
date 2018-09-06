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
	public class CertifiedPackageTable
	{
		public static async Task<string> Insert(string Command)
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
				MessageBox.Show("An error occured during the Insert Statement: " + ex.StackTrace.ToString() + " /n" + ex.ToString(), "Insert Statement Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
			return "Insert Successful";
		}

		public static async Task<IList<CertifiedPackage>> Read(string Command)
		{
			IList<CertifiedPackage> certifiedPackages = new List<CertifiedPackage>();
			SQLiteConnection connection = await Database.Connect();
			try
			{
				using (SQLiteCommand cmd = new SQLiteCommand(Command, connection))
				{
					using (SQLiteDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							certifiedPackages.Add(new CertifiedPackage
							{
								PackageID = reader.GetInt32(0),
								DiaryID = reader.GetInt32(1),
								UDNumber = reader.GetInt32(2),
								MembersEdipi = reader.GetInt32(3),
								MembersLastName = reader.GetString(4),
								MembersFirstName = reader.GetString(5),
								MembersMI = reader.GetString(6)
							});
						}
						reader.Close();
					}
					connection.Dispose();
					cmd.Dispose();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("An Error has occurred attempting to read from the Database. \n Error Message is : " + ex.Message.ToString(), "List Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			return certifiedPackages.OrderBy(o => o.PackageID).ToList();
		}
	}
}
