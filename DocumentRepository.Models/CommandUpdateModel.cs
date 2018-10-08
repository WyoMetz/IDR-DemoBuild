using System;

namespace DocumentRepository.Models
{
	public class CommandUpdateModel
	{
		private static string DateContext()
		{
			return CommandModel.GetDateContext();
		}

		private static string SectionContext()
		{
			return CommandModel.GetSectionContext();
		}

		private static string UploadDate()
		{
			return DateTime.Now.ToString();
		}

		private static string UploadUser()
		{
			return CommandModel.GetUserContext();
		}

		public static string DiaryUpdate(string FilePath, int DiaryID)
		{
			string Update = string.Format("UPDATE OR ROLLBACK DiaryTable{0} SET Uploaded = 'True', " +
				"Section = '{5}', UploadedBy = '{1}', UploadedOn = '{2}', UploadLocation = '{3}'" +
				" WHERE RowID = {4}", DateContext(), UploadUser(), UploadDate(), FilePath, DiaryID, SectionContext());
			return Update;
		}
	}
}
