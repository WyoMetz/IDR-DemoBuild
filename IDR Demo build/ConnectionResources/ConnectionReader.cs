using System;
using System.IO;

public class ConnectionReader
{
	//public DatabaseConnection()
	//{

	//}

	public string BackgroundImage()
	{
		string fileLocation = new Uri("IDR Demo Build/ResourceFiles/ResourceLocations.txt", UriKind.Relative);
		using (var streamReader = new StreamReader(fileLocation))
		{
			while (!streamReader.EndOfStream)
			{
				var lines = streamReader.ReadLine();
				var values = lines.Split(":");
			}
			return values;
		}
	}
}
