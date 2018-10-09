using System;

namespace DocumentRepository.Models
{
	public class CommandInsertModel
	{
		private static string DateContext()
		{
			return CommandModel.GetDateContext();
		}

		private static string SectionContext()
		{
			return CommandModel.GetSectionContext();
		}
		
		private static string DocTypeContext()
		{
			return CommandModel.GetDocTypeContext();
		}

		private static string UserContext()
		{
			return CommandModel.GetUserContext();
		}

		private static string UploadDate()
		{
			return DateTime.Now.ToString();
		}

		public static string InsertBulkDiary()
		{
			string Insert = string.Format("INSERT INTO DiaryTable{0}(UDYear, UDNumber, UDDate, CertifierID, CertifierEdipi, " +
				"LastName, CycleDate, CycleNumber, Accepted, Rejected, Total, Section, Uploaded, UploadedBy, UploadedOn, UploadLocation) " +
				"VALUES(@UDYear, @UDNumber, @UDDate, @CertifierID, @CertifierEdipi, @LastName, @CycleDate, @CycleNumber, @Accepted, " +
				"@Rejected, @Total, @Section, @Uploaded, @UploadedBy, @UploadedOn, @UploadLocation)", DateContext());
			return Insert;
		}

		public static string InsertCertifiedPackage(int DiaryID, string UDNumber, int MembersEdipi, string MembersLastName, string MembersFirstName, string MembersMI)
		{
			string Insert = string.Format("INSERT INTO CertifiedPackages{0}(DiaryKey, UDNumber, MembersEdipi, MembersLastName," +
				" MembersFirstName, MembersMI) VALUES ('{1}', '{2}', '{3}', '{4}', '{5}', '{6}')", DateContext(), DiaryID, UDNumber, MembersEdipi, MembersLastName, MembersFirstName, MembersMI);
			return Insert;
		}

		public static string InsertDocument(string MarineEdipi, string DateOfDoc, string UploadLocation)
		{
			string Insert = string.Format("INSERT INTO DocumentTable{0} VALUES ('{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')", DateContext(), MarineEdipi, DocTypeContext(), SectionContext(), UploadDate(), UserContext(), DateOfDoc, UploadLocation);
			return Insert;
		}

		public static string InsertMarine(string MarineEdipi, string MarineLastName, string MarineFirstName, string MI = " ")
		{
			string Insert = string.Format("INSERT INTO MarineInfo{0} VALUES ('{1}', '{2}', '{3}', '{4}')", DateContext(), MarineEdipi, MarineLastName, MarineFirstName, MI);
			return Insert;
		}

		public static string InsertDate()
		{
			string Insert = string.Format("INSERT INTO DateTable VALUES ('{0}')", DateContext());
			return Insert;
		}

		public static string InsertSection()
		{
			string Insert = string.Format("INSERT INTO SectionTable VALUES ('{0}')", SectionContext());
			return Insert;
		}

		public static string InsertDocType()
		{
			string Insert = string.Format("INSERT INTO DocTypeTable VALUES ('{0}')", DocTypeContext());
			return Insert;
		}

		public static string InsertUser()
		{
			string Insert = string.Format("INSERT INTO UserTable (UserName, DateJoined) VALUES ('{0}', '{1}')", UserContext(), UploadDate());
			return Insert;
		}
	}
}
