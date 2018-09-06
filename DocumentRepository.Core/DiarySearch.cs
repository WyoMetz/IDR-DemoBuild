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

		public static DataTable ByUploaded(int DiaryNumber)
		{
			IList<UnitDiary> list = DiaryList.Uploaded()
				.Where(o => o.UDNumber >= DiaryNumber)
				.ToList();
			return DiaryPager.First(list, Records);
		}

		public static DataTable ByUploaded(int DiaryNumber, string CertLastName)
		{
			IList<UnitDiary> list = DiaryList.Uploaded()
				.Where(o => o.UDNumber >= DiaryNumber)
				.Where(o => o.LastName.Contains(CertLastName))
				.ToList();
			return DiaryPager.First(list, Records);
		}

		public static DataTable ByUploaded(int DiaryNumber, int CertEdipi)
		{
			IList<UnitDiary> list = DiaryList.Uploaded()
				.Where(o => o.UDNumber >= DiaryNumber)
				.Where(o => o.CertifierEdipi <= CertEdipi)
				.ToList();
			return DiaryPager.First(list, Records);
		}

		public static DataTable ByUploaded(int DiaryNumber, string CertLastName, int CertEdipi)
		{
			IList<UnitDiary> list = DiaryList.Uploaded()
				.Where(o => o.UDNumber >= DiaryNumber)
				.Where(o => o.CertifierEdipi <= CertEdipi)
				.Where(o => o.LastName.Contains(CertLastName))
				.ToList();
			return DiaryPager.First(list, Records);
		}

		public static DataTable ByUploader(string Uploader)
		{
			IList<UnitDiary> list = DiaryList.Uploaded()
				.Where(o => o.UploadedBy.Contains(Uploader))
				.ToList();
			return DiaryPager.First(list, Records);
		}

		public static DataTable ByUploader(int DiaryNumber, string Uploader)
		{
			IList<UnitDiary> list = DiaryList.Uploaded()
				.Where(o => o.UDNumber >= DiaryNumber)
				.Where(o => o.UploadedBy.Contains(Uploader))
				.ToList();
			return DiaryPager.First(list, Records);
		}

		public static DataTable ByUploadedCert(int CertEdipi)
		{
			IList<UnitDiary> list = DiaryList.Uploaded()
				.Where(o => o.CertifierEdipi <= CertEdipi)
				.ToList();
			return DiaryPager.First(list, Records);
		}

		public static DataTable ByUploadedCert(string CertLastName)
		{
			IList<UnitDiary> list = DiaryList.Uploaded()
				.Where(o => o.LastName.Contains(CertLastName))
				.ToList();
			return DiaryPager.First(list, Records);
		}


	}
}
