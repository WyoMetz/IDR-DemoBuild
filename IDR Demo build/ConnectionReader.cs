using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows.Data;

public class ConnectionReader
{
	static string UserName = Environment.UserName.ToString();											//Gets the Computers logged in Username

	static void IdrAppData()
	{
		string AppData = @"C:\Users\" + UserName + @"\AppData\Local\IDR";
		if (!Directory.Exists(AppData))
		{
			Directory.CreateDirectory(AppData);
		}
	}

	static string filePath = @"C:\Users\" + UserName + @"\AppData\Local\IDR\ResourceLocations.txt";    //finds the Resource file TODO: Generate this file and location if not exists.

	static string fileLocation = Path.GetFullPath(filePath);                                       //Corrects the path to file


	private static List<string> BackgroundImage()
	{

		using (var streamReader = new StreamReader(fileLocation))								//Reads the lines within the text file
		{
			List<string> outputValues = new List<string>();
			while (!streamReader.EndOfStream)
			{
				var lines = streamReader.ReadLine();
				var values = lines.Split(';');													// The resources are defined as such e.g. BackgroundLocation; (String to background)

				if(values[0] == "BackgroundLocation")
				{
					string lineOutput = values[1];												 //The String Location is all we want

					outputValues.Add(lineOutput);
				}
			}
			return outputValues;																//returns the String as a List<string> to the calling function
		}
	}
	private static string ImageString = BackgroundImage().First();								// we then define the string statically
	/// <summary>
	/// The URI of the background Image defined by ResourceLocations.txt
	/// </summary>
	public Uri ImageUri = new Uri(ImageString, UriKind.RelativeOrAbsolute);						// pass that to the URI generator to give us a proper URI for use by the Image Class.

	private static List<string> FileRepository()
	{
		using (var streamReader = new StreamReader(fileLocation))                               //Reads the lines within the text file
		{
			List<string> outputValues = new List<string>();
			while (!streamReader.EndOfStream)
			{
				var lines = streamReader.ReadLine();
				var values = lines.Split(';');                                                  // The resources are defined as such e.g. BackgroundLocation; (String to background)

				if (values[0] == "FileRepositoryLocation")
				{
					string lineOutput = values[1];                                               //The String Location is all we want

					outputValues.Add(lineOutput);
				}
			}
			return outputValues;                                                                //returns the String as a List<string> to the calling function
		}
	}
	private static string RepositoryLocation = FileRepository().First();
	/// <summary>
	/// The Location of the File Repository
	/// </summary>
	public string RepositoryPath = Path.GetFullPath(RepositoryLocation);
}
