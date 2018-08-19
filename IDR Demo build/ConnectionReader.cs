using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows.Data;

public class ConnectionReader
{
	public static List<string> BackgroundImage()
	{
		string UserName = Environment.UserName.ToString();
		string filePath = @"C:\Users\"+UserName+ @"\AppData\Local\IDR\ResourceLocations.txt";
		string fileLocation = Path.GetFullPath(filePath);
		using (var streamReader = new StreamReader(fileLocation))
		{
			List<string> outputValues = new List<string>();
			while (!streamReader.EndOfStream)
			{
				var lines = streamReader.ReadLine();
				var values = lines.Split(';');

				if(values[0] == "BackgroundLocation")
				{
					string lineOutput = values[1];

					outputValues.Add(lineOutput);
				}
			}
			return outputValues;
		}
	}
	public static string ImageString = BackgroundImage().First();

	public Uri ImageUri = new Uri(ImageString, UriKind.RelativeOrAbsolute);
}
