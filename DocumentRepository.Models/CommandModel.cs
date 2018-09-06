namespace DocumentRepository.Models
{
	public class CommandModel
	{
		private static string DateContext { get; set; }
		public static void SetDateContext(string Date)
		{
			DateContext = Date;
		}

		public static string GetDateContext()
		{
			return DateContext;
		}

		public static string CreateDiaryTable()
		{
			string Create = string.Format("CREATE TABLE IF NOT EXISTS DiaryTable{0} (UDYear INTEGER, UDNumber INTEGER, UDDate DATE," +
				" CertifierID VARCHAR(10), CertifierEDIPI INTEGER, LastName VARCHAR(50), CycleDate DATE, CycleNumber INTEGER," +
				" Accepted INTEGER, Rejected INTEGER, Total INTEGER, Uploaded BOOL, UploadedBy VARCHAR(50), UploadedOn DATE, UploadLocation TEXT);", DateContext);
			return Create;
		}

		public static string SelectDiaries()
		{
			string Select = string.Format("SELECT RowID, * FROM DiaryTable{0}", DateContext);
			return Select;
		}

		public static string DiaryUpdate(string User, string Date, string FilePath, int DiaryID)
		{
			string Update = string.Format("UPDATE OR ROLLBACK DiaryTable{0} SET Uploaded = 'True', " +
				"UploadedBy = '{1}', UploadedOn = '{2}', UploadLocation = '{3}'" +
				" WHERE RowID = {4}", DateContext, User, Date, FilePath, DiaryID);
			return Update;
		}

		public static string InsertBulkDiary()
		{
			string Insert = string.Format("INSERT INTO DiaryTable{0}(UDYear, UDNumber, UDDate, CertifierID, CertifierEdipi, " +
				"LastName, CycleDate, CycleNumber, Accepted, Rejected, Total, Uploaded, UploadedBy, UploadedOn, UploadLocation) " +
				"VALUES(@UDYear, @UDNumber, @UDDate, @CertifierID, @CertifierEdipi, @LastName, @CycleDate, @CycleNumber, @Accepted, " +
				"@Rejected, @Total, @Uploaded, @UploadedBy, @UploadedOn, @UploadLocation)", DateContext);
			return Insert;
		}

		public static string CreateCertifiedPackageTable()
		{
			string Create = string.Format("CREATE TABLE IF NOT EXISTS CertifiedPackages{0} (DiaryKey INTEGER, UDNumber INTEGER," +
				" MembersEdipi INTEGER, MembersLastName VARCHAR(50), MembersFirstName VARCHAR(50), MembersMI VARCHAR(10))", DateContext);
			return Create;
		}

		public static string InsertCertifiedPackage(int DiaryID, int UDNumber, int MembersEdipi, string MembersLastName, string MembersFirstName, string MembersMI)
		{
			string Insert = string.Format("INSERT INTO CertifiedPackages{0}(DiaryKey, UDNumber, MembersEdipi, MembersLastName," +
				" MembersFirstName, MembersMI) VALUES ('{1}', '{2}', '{3}', '{4}', '{5}', '{6}')", DateContext, DiaryID, UDNumber, MembersEdipi, MembersLastName, MembersFirstName, MembersMI);
			return Insert;
		}

		public static string SelectCertifiedPackages()
		{
			string Select = string.Format("SELECT RowID, * FROM CertifiedPackages{0}", DateContext);
			return Select;
		}
	}
}
