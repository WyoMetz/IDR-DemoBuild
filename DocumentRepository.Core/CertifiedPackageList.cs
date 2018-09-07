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

		public static IList<UnitDiary> NotUploaded()
		{
			IList<UnitDiary> list = DiaryList.Diaries
				.Where(UDNumber => UDNumber.CertifiedPackages())
				.Where(Uploaded => Uploaded.NotUploaded())
				.ToList();
			return list;
		}

		public static IList<CertifiedPackage> Uploaded()
		{
			IList<CertifiedPackage> list = Packages;
			return list;
		}

		public static void UpdateList(int DiaryId, string UserName, string Date, string FileSaveLocation, int MembersEdipi, string MembersLastName, string MembersFirstName, string MembersMI)
		{
			var Diary = DiaryList.Diaries.First(i => i.DiaryID == DiaryId);
			Diary.Uploaded = "True";
			Diary.UploadedBy = UserName;
			Diary.UploadedOn = DateTime.Parse(Date);
			Diary.UploadLocation = FileSaveLocation;
			Packages.Add(new CertifiedPackage
			{
				 DiaryID = Diary.DiaryID,
				 UDYear = Diary.UDYear,
				 UDNumber = Diary.UDNumber,
				 UDDate = Diary.UDDate,
				 CertifierID = Diary.CertifierID,
				 CertifierEdipi = Diary.CertifierEdipi,
				 LastName = Diary.LastName,
				 CycleDate = Diary.CycleDate,
				 CycleNumber = Diary.CycleNumber,
				 Accepted = Diary.Accepted,
				 Rejected = Diary.Rejected,
				 Total = Diary.Total,
				 Uploaded = Diary.Uploaded,
				 UploadedBy = Diary.UploadedBy,
				 UploadedOn = Diary.UploadedOn,
				 UploadLocation = Diary.UploadLocation,
				 PackageID = DiaryId,
				 MembersEdipi = MembersEdipi,
				 MembersLastName = MembersLastName,
				 MembersFirstName = MembersFirstName,
				 MembersMI = MembersMI
			});
		}
	}
}
