using DocumentRepository.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DocumentRepository.Core
{
	public class DiarySearch
	{
		private static int Records { get; set; }

		public static void SetRecordsToShow(int Amount)
		{
			Records = Amount;
		}

		/// <summary>
		/// Search uploaded Diaries by Diary Number
		/// </summary>
		/// <param name="DiaryNumber"></param>
		/// <returns>DataTable</returns>
		public static DataTable ByUploaded(int DiaryNumber)
		{
			IList<UnitDiary> list = DiaryList.Uploaded()
				.Where(o => o.UDNumber >= DiaryNumber)
				.ToList();
			return DiaryPager.First(list, Records);
		}

		/// <summary>
		/// Search uploaded Diaries by Diary Number and Certifier Last Name
		/// </summary>
		/// <param name="DiaryNumber"></param>
		/// <param name="CertLastName"></param>
		/// <returns>DataTable</returns>
		public static DataTable ByUploaded(int DiaryNumber, string CertLastName)
		{
			IList<UnitDiary> list = DiaryList.Uploaded()
				.Where(o => o.UDNumber >= DiaryNumber)
				.Where(o => o.LastName.Contains(CertLastName))
				.ToList();
			return DiaryPager.First(list, Records);
		}

		/// <summary>
		/// Search uploaded Diaries by Diary Number and Certifier EDIPI
		/// </summary>
		/// <param name="DiaryNumber"></param>
		/// <param name="CertEdipi"></param>
		/// <returns>DataTable</returns>
		public static DataTable ByUploaded(int DiaryNumber, int CertEdipi)
		{
			IList<UnitDiary> list = DiaryList.Uploaded()
				.Where(o => o.UDNumber >= DiaryNumber)
				.Where(o => o.CertifierEdipi <= CertEdipi)
				.ToList();
			return DiaryPager.First(list, Records);
		}

		/// <summary>
		/// Search Uploaded Diaries by Diary Number and Certifier EDIPI and Certifier Last Name
		/// </summary>
		/// <param name="DiaryNumber"></param>
		/// <param name="CertLastName"></param>
		/// <param name="CertEdipi"></param>
		/// <returns>DataTable</returns>
		public static DataTable ByUploaded(int DiaryNumber, string CertLastName, int CertEdipi)
		{
			IList<UnitDiary> list = DiaryList.Uploaded()
				.Where(o => o.UDNumber >= DiaryNumber)
				.Where(o => o.CertifierEdipi <= CertEdipi)
				.Where(o => o.LastName.Contains(CertLastName))
				.ToList();
			return DiaryPager.First(list, Records);
		}

		/// <summary>
		/// Search by User who uploaded
		/// </summary>
		/// <param name="Uploader"></param>
		/// <returns>DataTable</returns>
		public static DataTable ByUploader(string Uploader)
		{
			IList<UnitDiary> list = DiaryList.Uploaded()
				.Where(o => o.UploadedBy.Contains(Uploader))
				.ToList();
			return DiaryPager.First(list, Records);
		}

		/// <summary>
		/// Search by user who uploaded and Diary Number
		/// </summary>
		/// <param name="DiaryNumber"></param>
		/// <param name="Uploader"></param>
		/// <returns></returns>
		public static DataTable ByUploader(int DiaryNumber, string Uploader)
		{
			IList<UnitDiary> list = DiaryList.Uploaded()
				.Where(o => o.UDNumber >= DiaryNumber)
				.Where(o => o.UploadedBy.Contains(Uploader))
				.ToList();
			return DiaryPager.First(list, Records);
		}

		/// <summary>
		/// Search uploaded Diaries by Certifier EDIPI
		/// </summary>
		/// <param name="CertEdipi"></param>
		/// <returns></returns>
		public static DataTable ByUploadedCert(int CertEdipi)
		{
			IList<UnitDiary> list = DiaryList.Uploaded()
				.Where(o => o.CertifierEdipi <= CertEdipi)
				.ToList();
			return DiaryPager.First(list, Records);
		}

		/// <summary>
		/// Search Uploaded Diaries by Certifier Last Name
		/// </summary>
		/// <param name="CertLastName"></param>
		/// <returns>DataTable</returns>
		public static DataTable ByUploadedCert(string CertLastName)
		{
			IList<UnitDiary> list = DiaryList.Uploaded()
				.Where(o => o.LastName.Contains(CertLastName))
				.ToList();
			return DiaryPager.First(list, Records);
		}

		/// <summary>
		/// Search by Not Uploaded Diary Numbers
		/// </summary>
		/// <param name="DiaryNumber"></param>
		/// <returns>DataTable</returns>
		public static DataTable ByNotUploaded(int DiaryNumber)
		{
			IList<UnitDiary> list = DiaryList.NeedUploaded()
				.Where(o => o.UDNumber >= DiaryNumber)
				.ToList();
			return DiaryPager.First(list, Records);
		}

		/// <summary>
		/// Search by Not Uploaded Diary Numbers and Certifier Last Name
		/// </summary>
		/// <param name="DiaryNumber"></param>
		/// <param name="CertLastName"></param>
		/// <returns>DataTable</returns>
		public static DataTable ByNotUploaded(int DiaryNumber, string CertLastName)
		{
			IList<UnitDiary> list = DiaryList.NeedUploaded()
				.Where(o => o.LastName.Contains(CertLastName))
				.Where(o => o.UDNumber >= DiaryNumber)
				.ToList();
			return DiaryPager.First(list, Records);
		}

		/// <summary>
		/// Search by not Uploaded Diary Number and Certifier EDIPI
		/// </summary>
		/// <param name="DiaryNumber"></param>
		/// <param name="CertEdipi"></param>
		/// <returns></returns>
		public static DataTable ByNotUploaded(int DiaryNumber, int CertEdipi)
		{
			IList<UnitDiary> list = DiaryList.NeedUploaded()
				.Where(o => o.UDNumber >= DiaryNumber)
				.Where(o => o.CertifierEdipi <= CertEdipi)
				.ToList();
			return DiaryPager.First(list, Records);
		}

		/// <summary>
		/// Search by Not Uploaded Diary Number, Certifier EDIPI, and Certifier Last Name
		/// </summary>
		/// <param name="DiaryNumber"></param>
		/// <param name="CertEdipi"></param>
		/// <param name="CertLastName"></param>
		/// <returns>DataTable</returns>
		public static DataTable ByNotUploaded(int DiaryNumber, int CertEdipi, string CertLastName)
		{
			IList<UnitDiary> list = DiaryList.NeedUploaded()
				.Where(o => o.UDNumber >= DiaryNumber)
				.Where(o => o.CertifierEdipi <= CertEdipi)
				.Where(o => o.LastName.Contains(CertLastName))
				.ToList();
			return DiaryPager.First(list, Records);
		}
	}
}
