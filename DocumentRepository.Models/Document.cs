using System;

namespace DocumentRepository.Models
{
	public class Document : Marine
	{
		public int DocID { get; set; }
		public string DocType { get; set; }
		public string Section { get; set; }
		public DateTime UploadDate { get; set; }
		public string UploadUser { get; set; }
		public DateTime DateOfDoc { get; set; }
		public string UploadLocation { get; set; }
		public string DateOfDocShort
		{
			get
			{
				return DateOfDoc.ToShortDateString();
			}
		}
	}
}
