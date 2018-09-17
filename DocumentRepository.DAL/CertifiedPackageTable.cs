using DocumentRepository.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
				MessageBox.Show("An error occured during the Insert Statement: " + ex.Message.ToString(), "Insert Statement Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
								DiaryID = reader.GetInt32(16),
								UDYear = reader.GetInt32(0),
								UDNumber = reader.GetInt32(1),
								UDDate = reader.GetDateTime(2),
								CertifierID = reader.GetString(3),
								CertifierEdipi = reader.GetInt32(4),
								LastName = reader.GetString(5),
								CycleDate = reader.GetDateTime(6),
								CycleNumber = reader.GetInt32(7),
								Accepted = reader.GetInt32(8),
								Rejected = reader.GetInt32(9),
								Total = reader.GetInt32(10),
								Uploaded = reader.GetString(11),
								UploadedBy = reader.GetString(12),
								UploadedOn = reader.GetDateTime(13),
								UploadLocation = reader.GetString(14),
								PackageID = reader.GetInt32(15),
								MembersEdipi = reader.GetInt32(17),
								MembersLastName = reader.GetString(18),
								MembersFirstName = reader.GetString(19),
								MembersMI = reader.GetString(20)
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
