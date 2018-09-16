using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
	}
}
