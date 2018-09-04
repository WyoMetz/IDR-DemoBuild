using DocumentRepository.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocumentRepository.DAL
{
	public class DiaryTable
	{
		/// <summary>
		/// Takes in IList of Unit Diaries and pushes it into the Database.
		/// </summary>
		/// <param name="InsertStatement"></param>
		/// <returns>Task Completed</returns>
		public static async Task<string> InsertBulkDiaryInfoAsync(IList<UnitDiary> InsertStatement)
		{
			SQLiteConnection connection = await Database.DbConnect();
			try
			{
				SQLiteCommand cmd = connection.CreateCommand();
				SQLiteTransaction transaction = connection.BeginTransaction();
				cmd.CommandText = CommandModel.InsertBulkDiary();
				cmd.Parameters.AddWithValue("@UDYear", "");
				cmd.Parameters.AddWithValue("@UDNumber", "");
				cmd.Parameters.AddWithValue("@UDDate", "");
				cmd.Parameters.AddWithValue("@CertifierID", "");
				cmd.Parameters.AddWithValue("@CertifierEdipi", "");
				cmd.Parameters.AddWithValue("@LastName", "");
				cmd.Parameters.AddWithValue("@CycleNumber", "");
				cmd.Parameters.AddWithValue("@CycleDate", "");
				cmd.Parameters.AddWithValue("@Accepted", "");
				cmd.Parameters.AddWithValue("@Rejected", "");
				cmd.Parameters.AddWithValue("@Total", "");
				cmd.Parameters.AddWithValue("@Uploaded", "");
				cmd.Parameters.AddWithValue("@UploadedBy", "");
				cmd.Parameters.AddWithValue("@UploadedOn", "");
				cmd.Parameters.AddWithValue("@UploadLocation", "");

				foreach (var diary in InsertStatement)
				{
					cmd.Parameters["@UDYear"].Value = diary.UDYear;
					cmd.Parameters["@UDNumber"].Value = diary.UDNumber;
					cmd.Parameters["@UDDate"].Value = diary.UDDate;
					cmd.Parameters["@CertifierID"].Value = diary.CertifierID;
					cmd.Parameters["@CertifierEdipi"].Value = diary.CertifierEdipi;
					cmd.Parameters["@LastName"].Value = diary.LastName;
					cmd.Parameters["@CycleNumber"].Value = diary.CycleNumber;
					cmd.Parameters["@CycleDate"].Value = diary.CycleDate;
					cmd.Parameters["@Accepted"].Value = diary.Accepted;
					cmd.Parameters["@Rejected"].Value = diary.Rejected;
					cmd.Parameters["@Total"].Value = diary.Total;
					cmd.Parameters["@Uploaded"].Value = "False";
					cmd.Parameters["@UploadedBy"].Value = " ";
					cmd.Parameters["@UploadedOn"].Value = "11/10/1775";
					cmd.Parameters["@UploadLocation"].Value = " ";
					cmd.ExecuteNonQuery();
				}
				transaction.Commit();
				cmd.Dispose();
				connection.Dispose();
			}
			catch (Exception ex)
			{
				MessageBox.Show("An error occured during the Insert Statement: " + ex.StackTrace.ToString() + " /n" + ex.ToString(), "Insert Statement Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return "Failed";
			}
			return "Completed";
		}

		/// <summary>
		/// Provides an Estimated time to Complete for bulk SQL statements.
		/// </summary>
		/// <param name="InsertStatement"></param>
		/// <param name="progress"></param>
		/// <returns>The Diary Number currently being inserted</returns>
		public static Task BulkInsertTimer(IList<UnitDiary> InsertStatement, IProgress<ReportModel> progress)
		{
			ReportModel report = new ReportModel();
			decimal counter = 0;
			string output;

			foreach (var diary in InsertStatement)
			{
				if (diary != null)
				{
					output = diary.UDNumber.ToString();
				}
				else
				{
					output = "Complete";
				}
				counter++;
				decimal totalProgress = (counter / InsertStatement.Count) * 100;
				report.PercentComplete = Convert.ToInt32(totalProgress);
				report.InsertItem = output;
				progress.Report(report);
				Thread.Sleep(1);
			}
			return Task.CompletedTask;

		}

		/// <summary>
		/// Reads the Entire Diarytable
		/// </summary>
		/// <param name="SQLCommand"></param>
		/// <returns>IList<UnitDiary></returns>
		public static async Task<IList<UnitDiary>> ReadDiaryTable(string SQLCommand)
		{
			IList<UnitDiary> diaries = new List<UnitDiary>();
			SQLiteConnection connection = await Database.DbConnect();
			try
			{
				using (SQLiteCommand cmd = new SQLiteCommand(SQLCommand, connection))
				{
					using (SQLiteDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							diaries.Add(new UnitDiary
							{
								DiaryID = reader.GetInt32(0),
								UDYear = reader.GetInt32(1),
								UDNumber = reader.GetInt32(2),
								UDDate = reader.GetDateTime(3),
								CertifierID = reader.GetString(4),
								CertifierEdipi = reader.GetInt32(5),
								LastName = reader.GetString(6),
								CycleDate = reader.GetDateTime(7),
								CycleNumber = reader.GetInt32(8),
								Accepted = reader.GetInt32(9),
								Rejected = reader.GetInt32(10),
								Total = reader.GetInt32(11),
								Uploaded = reader.GetString(12),
								UploadedBy = reader.GetString(13),
								UploadedOn = reader.GetDateTime(14),
								UploadLocation = reader.GetString(15),
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
			return diaries.OrderBy(o => o.DiaryID).ToList();
		}

		public static async Task<string> UpdateUnitDiary(string Command)
		{
			SQLiteConnection connection = await Database.DbConnect();
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
			return "Update Sucessful";
		}
	}
}

