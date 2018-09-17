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

		public static string InsertBulkDiary()
		{
			string Insert = string.Format("INSERT INTO DiaryTable{0}(UDYear, UDNumber, UDDate, CertifierID, CertifierEdipi, " +
				"LastName, CycleDate, CycleNumber, Accepted, Rejected, Total, Uploaded, UploadedBy, UploadedOn, UploadLocation) " +
				"VALUES(@UDYear, @UDNumber, @UDDate, @CertifierID, @CertifierEdipi, @LastName, @CycleDate, @CycleNumber, @Accepted, " +
				"@Rejected, @Total, @Uploaded, @UploadedBy, @UploadedOn, @UploadLocation)", DateContext());
			return Insert;
		}

		public static string InsertCertifiedPackage(int DiaryID, string UDNumber, int MembersEdipi, string MembersLastName, string MembersFirstName, string MembersMI)
		{
			string Insert = string.Format("INSERT INTO CertifiedPackages{0}(DiaryKey, UDNumber, MembersEdipi, MembersLastName," +
				" MembersFirstName, MembersMI) VALUES ('{1}', '{2}', '{3}', '{4}', '{5}', '{6}')", DateContext(), DiaryID, UDNumber, MembersEdipi, MembersLastName, MembersFirstName, MembersMI);
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
	}
}
