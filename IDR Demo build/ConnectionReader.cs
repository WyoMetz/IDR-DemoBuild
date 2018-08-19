using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows.Data;

public class ConnectionReader
{
	private static List<string> BackgroundImage()
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
	private static string ImageString = BackgroundImage().First();
	/// <summary>
	/// The URI of the background Image defined by ResourceLocations.txt
	/// </summary>
	public Uri ImageUri = new Uri(ImageString, UriKind.RelativeOrAbsolute);
}
