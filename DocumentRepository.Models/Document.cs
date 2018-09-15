using System;

namespace DocumentRepository.Models
{
	class Document : Marine
	{
		public int DocID { get; set; }
		public string DocType { get; set; }
		public DateTime UploadDate { get; set; }
		public string UploadUser { get; set; }
		public DateTime DateOfDoc { get; set; }
		public string UploadLocation { get; set; }
	}
}
