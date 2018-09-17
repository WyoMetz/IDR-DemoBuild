using System.Collections.Generic;

namespace DocumentRepository.Core
{
	public class SectionContextList
	{
		public static IList<string> Sections { get; set; }

		public static async void PrepareSectionContext()
		{
			if (Sections == null)
			{
				Sections = await Repository.GetSectionsAsync();
			}
		}

		public static void Add(string Section)
		{
			Sections.Add(Section);
		}
	}
}
