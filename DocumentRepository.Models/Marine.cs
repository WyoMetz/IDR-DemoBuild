﻿using System.Collections.Generic;
namespace DocumentRepository.Models
{
	public class Marine
	{
		public int EDIPI { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public string MI { get; set; }
		public IList<Document> Documents { get; set; }
	}
}
