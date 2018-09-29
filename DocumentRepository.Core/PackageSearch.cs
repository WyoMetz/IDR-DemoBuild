using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentRepository.Models;

namespace DocumentRepository.Core
{
	public class PackageSearch
	{
		private static int Records { get; set; }

		public static void SetRecordsToShow(int Amount)
		{
			Records = Amount;
		}

		public static DataTable ByNotUploaded(int DiaryNumber)
		{
			IList<UnitDiary> list = CertifiedPackageList.NotUploaded()
				.Where(o => o.UDNumber >= DiaryNumber)
				.ToList();
			return DiaryPager.First(list, Records);
		}

		public static DataTable ByNotUploaded(int DiaryNumber, string CertLastName)
		{
			IList<UnitDiary> list = CertifiedPackageList.NotUploaded()
				.Where(o => o.UDNumber >= DiaryNumber)
				.Where(o => o.LastName.Contains(CertLastName.ToUpper()))
				.ToList();
			return DiaryPager.First(list, Records);
		}

		public static DataTable ByNotUploaded(int DiaryNumber, int CertEdipi)
		{
			IList<UnitDiary> list = CertifiedPackageList.NotUploaded()
				.Where(o => o.UDNumber >= DiaryNumber)
				.Where(o => o.CertifierEdipi <= CertEdipi)
				.ToList();
			return DiaryPager.First(list, Records);
		}

		public static DataTable ByNotUploaded(int DiaryNumber, int CertEdipi, string CertLastName)
		{
			IList<UnitDiary> list = CertifiedPackageList.NotUploaded()
				.Where(o => o.UDNumber >= DiaryNumber)
				.Where(o => o.CertifierEdipi <= CertEdipi)
				.Where(o => o.LastName.Contains(CertLastName.ToUpper()))
				.ToList();
			return DiaryPager.First(list, Records);
		}

		public static DataTable ByNotUploadedCert(string CertLastName)
		{
			IList<UnitDiary> list = CertifiedPackageList.NotUploaded()
				.Where(o => o.LastName.Contains(CertLastName.ToUpper()))
				.ToList();
			return DiaryPager.First(list, Records);
		}

		public static DataTable ByNotUploadedCert(int CertEdipi)
		{
			IList<UnitDiary> list = CertifiedPackageList.NotUploaded()
				.Where(o => o.CertifierEdipi <= CertEdipi)
				.ToList();
			return DiaryPager.First(list, Records);
		}

		public static DataTable ByUploadCert(string CertLastName)
		{
			IList<CertifiedPackage> list = CertifiedPackageList.Uploaded()
				.Where(o => o.LastName.Contains(CertLastName.ToUpper()))
				.ToList();
			return PackagePager.First(list, Records);
		}

		public static DataTable ByUploadCert(int CertEdipi)
		{
			IList<CertifiedPackage> list = CertifiedPackageList.Uploaded()
				.Where(o => o.CertifierEdipi <= CertEdipi)
				.ToList();
			return PackagePager.First(list, Records);
		}

		public static DataTable ByUploaded(int DiaryNumber)
		{
			IList<CertifiedPackage> list = CertifiedPackageList.Uploaded()
				.Where(o => o.UDNumber >= DiaryNumber)
				.ToList();
			return PackagePager.First(list, Records);
		}

		public static DataTable ByUploaded(int DiaryNumber, string CertLastName)
		{
			IList<CertifiedPackage> list = CertifiedPackageList.Uploaded()
				.Where(o => o.UDNumber >= DiaryNumber)
				.Where(o => o.LastName.Contains(CertLastName.ToUpper()))
				.ToList();
			return PackagePager.First(list, Records);
		}

		public static DataTable ByUploaded(int DiaryNumber, int CertEdipi)
		{
			IList<CertifiedPackage> list = CertifiedPackageList.Uploaded()
				.Where(o => o.UDNumber >= DiaryNumber)
				.Where(o => o.CertifierEdipi <= CertEdipi)
				.ToList();
			return PackagePager.First(list, Records);
		}

		public static DataTable ByUploaded(int DiaryNumber, string CertLastName, int CertEdipi)
		{
			IList<CertifiedPackage> list = CertifiedPackageList.Uploaded()
				.Where(o => o.UDNumber >= DiaryNumber)
				.Where(o => o.CertifierEdipi <= CertEdipi)
				.Where(o => o.LastName.Contains(CertLastName.ToUpper()))
				.ToList();
			return PackagePager.First(list, Records);
		}

		public static DataTable ByUploader(string UploadUser)
		{
			IList<CertifiedPackage> list = CertifiedPackageList.Uploaded()
				.Where(o => o.UploadedBy.Contains(UploadUser))
				.ToList();
			return PackagePager.First(list, Records);
		}

		public static DataTable ByUploader(string UploadUser, int DiaryNumber)
		{
			IList<CertifiedPackage> list = CertifiedPackageList.Uploaded()
				.Where(o => o.UDNumber >= DiaryNumber)
				.Where(o => o.UploadedBy.Contains(UploadUser))
				.ToList();
			return PackagePager.First(list, Records);
		}

		public static DataTable ByMember(string MemberLastName)
		{
			IList<CertifiedPackage> list = CertifiedPackageList.Uploaded()
				.Where(o => o.MembersLastName.Contains(MemberLastName))
				.ToList();
			return PackagePager.First(list, Records);
		}

		public static DataTable ByMember(int MemberEdipi)
		{
			IList<CertifiedPackage> list = CertifiedPackageList.Uploaded()
				.Where(o => o.MembersEdipi <= MemberEdipi)
				.ToList();
			return PackagePager.First(list, Records);
		}

		public static DataTable ByMember(string MemberLastName, string MemberFirstName)
		{
			IList<CertifiedPackage> list = CertifiedPackageList.Uploaded()
				.Where(o => o.MembersLastName.Contains(MemberLastName))
				.Where(o => o.MembersFirstName.Contains(MemberFirstName))
				.ToList();
			return PackagePager.First(list, Records);
		}

	}
}
