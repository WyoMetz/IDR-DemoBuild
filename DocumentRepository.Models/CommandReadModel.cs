namespace DocumentRepository.Models
{
	public class CommandReadModel
	{
		private static readonly string DateContext = CommandModel.GetDateContext();

		public static string SelectDiaries()
		{
			string Select = string.Format("SELECT RowID, * FROM DiaryTable{0}", DateContext);
			return Select;
		}

		public static string SelectCertifiedPackages()
		{
			string Select = string.Format("SELECT * FROM DiaryTable{0} JOIN CertifiedPackages{0} ON DiaryTable{0}.RowID = CertifiedPackages{0}.DiaryKey", DateContext);
			return Select;
		}

		public static string SelectDocument()
		{
			string Select = string.Format("SELECT * FROM DocumentTable{0} JOIN MarineInfo{0} ON DocumentTable{0}.MarineEDIPI = MarineInfo{0}.EDIPI", DateContext);
			return Select;
		}

		public static string ReadDateTable()
		{
			string Select = "SELECT * FROM DateTable";
			return Select;
		}

		public static string ReadSectionTable()
		{
			string Select = "SELECT * FROM SectionTable";
			return Select;
		}

		public static string ReadDocTable()
		{
			string Select = "SELECT * FROM DocTypeTable";
			return Select;
		}
	}
}
