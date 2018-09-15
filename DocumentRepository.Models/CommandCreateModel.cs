namespace DocumentRepository.Models
{
	public class CommandCreateModel
	{
		private static readonly string DateContext = CommandModel.GetDateContext();

		public static string CreateDiaryTable()
		{
			string Create = string.Format("CREATE TABLE IF NOT EXISTS DiaryTable{0} (UDYear INTEGER, UDNumber INTEGER, UDDate DATE," +
				" CertifierID VARCHAR(10), CertifierEDIPI INTEGER, LastName VARCHAR(50), CycleDate DATE, CycleNumber INTEGER," +
				" Accepted INTEGER, Rejected INTEGER, Total INTEGER, Uploaded BOOL, UploadedBy VARCHAR(50), UploadedOn DATE, UploadLocation TEXT)", DateContext);
			return Create;
		}

		public static string CreateCertifiedPackageTable()
		{
			string Create = string.Format("CREATE TABLE IF NOT EXISTS CertifiedPackages{0} (DiaryKey INTEGER, UDNumber INTEGER," +
				" MembersEdipi INTEGER, MembersLastName VARCHAR(50), MembersFirstName VARCHAR(50), MembersMI VARCHAR(10))", DateContext);
			return Create;
		}

		public static string CreateMarineTable()
		{
			string Create = string.Format("CREATE TABLE IF NOT EXISTS MarineInfo{0} (EDIPI PRIMARY KEY UNIQUE, LastName VARCHAR(50), FirstName VARCHAR(50), MI VARCHAR(10))", DateContext);
			return Create;
		}

		public static string CreateDocumentTable()
		{
			string Create = string.Format("CREATE TABLE IF NOT EXISTS DocumentTable{0} (MarineEDIPI INTEGER, DocType VARCHAR(50), UploadDate DATE, UploadUser VARCHAR(80), DateOfDoc DATE, UploadLocation TEXT)", DateContext);
			return Create;
		}
	}
}
