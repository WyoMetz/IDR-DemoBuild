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

		public static string SelectCertifiedPackages()
		{
			string Select = string.Format("SELECT * FROM DiaryTable{0} JOIN CertifiedPackages{0} ON DiaryTable{0}.RowID = CertifiedPackages{0}.DiaryKey", DateContext);
			return Select;
		}

	}
}
