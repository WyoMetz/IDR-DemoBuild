using DocumentRepository.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DocumentRepository.Core
{
	public class CertifiedPackageList
	{
		public static IList<CertifiedPackage> Packages { get; set; }
		private static int Records { get; set; }

		public static async void PreparePackages()
		{
			if (Packages == null)
			{
				Packages = await Repository.GetPackagesAsync();
			}
		}

		public static void SetRecordsToShow(int Amount)
		{
			Records = Amount;
		}

		public static IList<UnitDiary> NeedUploaded()
		{
			IList<UnitDiary> list = DiaryList.Diaries
				.Where(UDNumber => UDNumber.CertifiedPackages())
				.ToList();
			return list;
		}

		public static DataTable SearchNotUploadedUDNumber(string SearchString)
		{
			bool sorter = Int32.TryParse(SearchString, out int sortNumber);
			if (!sorter) { sortNumber = 0; }
			IList<UnitDiary> list = DiaryList.Diaries
				.Where(o => o.UDNumber >= sortNumber)
				.Where(UDNumber => UDNumber.CertifiedPackages())
				.ToList();
			return DiaryPager.SetPaging(list, Records);
		}

		public static DataTable SearchNotUploadedUDNumber(string DiaryNumber, string CertEdipi)
		{
			bool sorter = Int32.TryParse(DiaryNumber, out int sortNumber);
			if (!sorter) { sortNumber = 0; }
			bool sorter2 = Int32.TryParse(CertEdipi, out int sortNumber2);
			if (!sorter2) { sortNumber2 = 0; }
			IList<UnitDiary> list = DiaryList.Diaries
				.Where(o => o.UDNumber >= sortNumber)
				.Where(o => o.CertifierEdipi <= sortNumber2)
				.Where(UDNumber => UDNumber.CertifiedPackages())
				.ToList();
			return DiaryPager.SetPaging(list, Records);
		}

		public static DataTable SearchNotUploadedUDNumber(string DiaryNumber, string CertEdipi, string CertLastName)
		{
			bool sorter = Int32.TryParse(DiaryNumber, out int sortNumber);
			if (!sorter) { sortNumber = 0; }
			bool sorter2 = Int32.TryParse(CertEdipi, out int sortNumber2);
			if (!sorter2) { sortNumber2 = 0; }
			IList<UnitDiary> list = DiaryList.Diaries
				.Where(o => o.UDNumber >= sortNumber)
				.Where(o => o.CertifierEdipi <= sortNumber2)
				.Where(o => o.LastName.Contains(CertLastName.ToUpper()))
				.ToList();
			return DiaryPager.SetPaging(list, Records);
		}

		public static DataTable SearchNotUploadedCertEdipi(string SearchString)
		{
			bool sorter = Int32.TryParse(SearchString, out int sortNumber);
			if (!sorter) { sortNumber = 0; }
			IList<UnitDiary> list = DiaryList.Diaries
				.Where(o => o.CertifierEdipi <= sortNumber)
				.Where(UDNumber => UDNumber.CertifiedPackages())
				.ToList();
			return DiaryPager.SetPaging(list, Records);
		}

		public static DataTable SearchNotUploadedCertLastName(string SearchString)
		{
			IList<UnitDiary> list = DiaryList.Diaries
				.Where(o => o.LastName.Contains(SearchString.ToUpper()))
				.Where(UDNumber => UDNumber.CertifiedPackages())
				.ToList();
			return DiaryPager.SetPaging(list, Records);
		}

		public static DataTable SearchNotUploadedCertLastName(string CertLastName, string DiaryNumber)
		{
			bool sorter = Int32.TryParse(DiaryNumber, out int sortNumber);
			if (!sorter) { sortNumber = 0; }
			IList<UnitDiary> list = DiaryList.Diaries
				.Where(o => o.LastName.Contains(CertLastName.ToUpper()))
				.Where(o => o.UDNumber >= sortNumber)
				.Where(UDNumber => UDNumber.CertifiedPackages())
				.ToList();
			return DiaryPager.SetPaging(list, Records);
		}
		//TODO: Search Methods for the Packages e.g. EDIPI, LastName, Etc.
	}
}
