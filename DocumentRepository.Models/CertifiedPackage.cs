﻿namespace DocumentRepository.Models
{
	public class CertifiedPackage : UnitDiary
	{
		public int PackageID { get; set; }
		public int MembersEdipi { get; set; }
		public string MembersLastName { get; set; }
		public string MembersFirstName { get; set; }
		public string MembersMI { get; set; }
	}
}
