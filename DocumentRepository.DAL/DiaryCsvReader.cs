using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentRepository.Models;
using System.IO;
using System.Windows.Forms;

namespace DocumentRepository.DAL
{
	public class DiaryCsvReader
	{
		/// <summary>
		/// Captures the Header of the Input CSV
		/// </summary>
		private static string CsvHeader;

		/// <summary>
		/// Creates a list of UnitDiary Objects from a given CSV file
		/// </summary>
		/// <param name="FullFilePath"></param>
		/// <returns>List of UnitDiary Objects</returns>
		public static IList<UnitDiary> GetUnitDiaries(string FullFilePath)
		{
			using (StreamReader reader = new StreamReader(FullFilePath))
			{
				StringBuilder builder = new StringBuilder();

				IList<UnitDiary> unitDiaries = new List<UnitDiary>();

				try
				{
					CsvHeader = reader.ReadLine();

					while (!reader.EndOfStream)
					{
						var line = reader.ReadLine();
						string[] values = line.Split(',', '\t');

						bool EdipiExists = Int32.TryParse(values[4], out int EdipiParse);
						if (!EdipiExists) { EdipiParse = 0; }

						string CertIdParse;
						bool CertIdDoesNotExists = String.IsNullOrEmpty(values[3]);
						if (CertIdDoesNotExists) { CertIdParse = " "; } else { CertIdParse = values[3]; }

						string LastNameParse;
						bool LastNameDoesNotExist = String.IsNullOrEmpty(values[5]);
						if (LastNameDoesNotExist) { LastNameParse = "System Automatic Transaction"; } else { LastNameParse = values[5]; }

						unitDiaries.Add(new UnitDiary
						{
							UDYear = Int32.Parse(values[0]),
							UDNumber = Int32.Parse(values[1]),
							UDDate = DateTime.Parse(values[2]),
							CertifierID = CertIdParse,
							CertifierEdipi = EdipiParse,
							LastName = LastNameParse,
							CycleDate = DateTime.Parse(values[6]),
							CycleNumber = Int32.Parse(values[7]),
							Accepted = Int32.Parse(values[8]),
							Rejected = Int32.Parse(values[9]),
							Total = Int32.Parse(values[10])
						});
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("An Error has occurred attempting to upload this list. \n Error Message is : " + ex.Message.ToString(), "List Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					reader.Dispose();
				}
				return unitDiaries.OrderBy(o => o.UDNumber).ToList();
			}

		}
	}
}
