using System;

namespace DocumentRepository.Models
{
	public class CommandUpdateModel
	{
		private static readonly string DateContext = CommandModel.GetDateContext();

		private static readonly string SectionContext = CommandModel.GetSectionContext();

		private static readonly string UploadDate = DateTime.Now.ToLongDateString();

		private static readonly string UploadUser = CommandModel.GetUserContext();

		public static string DiaryUpdate(string FilePath, int DiaryID)
		{
			string Update = string.Format("UPDATE OR ROLLBACK DiaryTable{0} SET Uploaded = 'True', " +
				"Section = '{5}', UploadedBy = '{1}', UploadedOn = '{2}', UploadLocation = '{3}'" +
				" WHERE RowID = {4}", DateContext, UploadUser, UploadDate, FilePath, DiaryID, SectionContext);
			return Update;
		}
	}
}
