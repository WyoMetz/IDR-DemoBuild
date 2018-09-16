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

		private static string SectionContext { get; set; }

		public static void SetSectionContext(string Section)
		{
			SectionContext = Section;
		}

		public static string GetSectionContext()
		{
			return SectionContext;
		}

		private static string UserContext { get; set; }

		public static void SetUserContext(string User)
		{
			UserContext = User;
		}

		public static string GetUserContext()
		{
			return UserContext;
		}
	}
}
