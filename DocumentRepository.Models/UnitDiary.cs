using System;

namespace DocumentRepository.Models
{
	public class UnitDiary
	{
		public int DiaryID { get; set; }
		public int UDYear { get; set; }
		public int UDNumber { get; set; }
		public DateTime UDDate { get; set; }
		public string CertifierID { get; set; }
		public int CertifierEdipi { get; set; }
		public string LastName { get; set; }
		public DateTime CycleDate { get; set; }
		public int CycleNumber { get; set; }
		public int Accepted { get; set; }
		public int Rejected { get; set; }
		public int Total { get; set; }
		public string Uploaded { get; set; }
		public string UploadedBy { get; set; }
		public DateTime UploadedOn { get; set; }

		/// <summary>
		/// This should never be shown to the user
		/// </summary>
		public string UploadLocation { get; set; }


		/// <summary>
		/// Used to sort the UnitDiary Class to output only Typed Unit Diaries
		/// </summary>
		/// <returns>Unit Diaries</returns>
		public bool RegularDiaries()
		{
			if (UDNumber < 89999 || UDNumber != 88888)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Used to sort the UnitDiary Class to output only automatic Diary Transactions due to Certified Packages
		/// </summary>
		/// <returns>Certified Packages</returns>
		public bool CertifiedPackages()
		{
			if (UDNumber > 89999)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Used to Sort the UnitDiary Class to output only System Automatic transactions
		/// </summary>
		/// <returns>System Automatic Transactions</returns>
		public bool SystemAutomaticTransaction()
		{
			if (UDNumber == 88888)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Used to Sort the UnitDiary Class to show Diaries that have not been uploaded
		/// </summary>
		/// <returns>NotUploaded</returns>
		public bool NotUploaded()
		{
			if(Uploaded == "False")
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
