using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentRepository.Models
{
	public class User
	{
		public string UserName { get; set; }
		public string Section { get; set; }
		public string Background { get; set; }
		private DateTime dateJoined { get; set; }
		public string PrimaryColor { get; set; }
		public string SecondaryColor { get; set; }
		public string UsesLightTheme { get; set; }
		public string DateJoined
		{
			get
			{
				return dateJoined.ToShortDateString();
			}
			set
			{
				dateJoined = DateTime.Parse(value);
			}
		}
	}
}
