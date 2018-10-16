using DocumentRepository.DAL;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RepositoryDownloader
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("************************************************** \nIPAC DOCUMENT REPOSITORY \nApplication Installer \n\nRepository Created by the Marines of \nSystems Integration \n**************************************************\n\n\n Press any key to begin......\n\n\n");
			Console.ReadLine();
			DownloadRepository();
			DownloadVisualizer();
			Console.WriteLine("Done \n Press any key to exit...");
			Console.ReadLine();
		}

		public static async void RunShortcut()
		{
			Task CreateShortCut = Task.Run(() => ShortcutBuilder.CreateShortCut());
			await CreateShortCut;
		}

		public static async void DownloadRepository()
		{
			Task Download = Task.Run(() => FileOperation.DownloadRepository());
			await Download;
			RunShortcut();
		}

		public static Task DownloadVisualizer()
		{
			string[] SystemsText =
				{
							@" __           _                        _____       _                       _   _             ",
							@"/ _\_   _ ___| |_ ___ _ __ ___  ___    \_   \_ __ | |_ ___  __ _ _ __ __ _| |_(_) ___  _ __  ",
							@"\ \| | | / __| __/ _ \ '_ ` _ \/ __|    / /\/ '_ \| __/ _ \/ _` | '__/ _` | __| |/ _ \| '_ \ ",
							@"_\ \ |_| \__ \ ||  __/ | | | | \__ \ /\/ /_ | | | | ||  __/ (_| | | | (_| | |_| | (_) | | | |",
							@"\__/\__, |___/\__\___|_| |_| |_|___/ \____/ |_| |_|\__\___|\__, |_|  \__,_|\__|_|\___/|_| |_|",
							@"    |___/                                                  |___/                             ",
						};
			foreach (string text in SystemsText)
			{
				Console.WriteLine(text);
				Thread.Sleep(150);
			}
			Console.WriteLine("\n\n\n Checking Eviroment Variables.....\n");
			string[] Dots = { ".", ".", ".", ".", ".", ".", ".", ".", ".", "." };
			foreach (string text in Dots)
			{
				Console.Write(text);
				Thread.Sleep(500);
			}
			Console.WriteLine("\nAll Checks Passed");
			Thread.Sleep(500);
			Console.WriteLine("\n User is " + AppSettings.User);
			Console.WriteLine("\nChecking System Version\n");
			foreach (string text in Dots)
			{
				Console.Write(text);
				Thread.Sleep(500);
			}
			Console.WriteLine("\nChecks Done!");
			Console.WriteLine("\nYour application shortcut should be on your Desktop.");
			Console.WriteLine("\n\n If you Experience any Issues with this Program please contact Qualtiy Control and Systems Integration");
			Console.WriteLine("\n\n Program Copywrite Systems Integration October 2018\n");
			//Console.WriteLine(@"(╯°□°）╯︵ ┻━┻ ");
			return Task.CompletedTask;
		}

		public static async void DoVisuals()
		{
			Task VisualStuff = Task.Run(() => DownloadVisualizer());
			await VisualStuff;
		}
	}
}
//Standard Text
//string[] SystemsText =
//	{
//				@" __           _                        _____       _                       _   _             ",
//				@"/ _\_   _ ___| |_ ___ _ __ ___  ___    \_   \_ __ | |_ ___  __ _ _ __ __ _| |_(_) ___  _ __  ",
//				@"\ \| | | / __| __/ _ \ '_ ` _ \/ __|    / /\/ '_ \| __/ _ \/ _` | '__/ _` | __| |/ _ \| '_ \ ",
//				@"_\ \ |_| \__ \ ||  __/ | | | | \__ \ /\/ /_ | | | | ||  __/ (_| | | | (_| | |_| | (_) | | | |",
//				@"\__/\__, |___/\__\___|_| |_| |_|___/ \____/ |_| |_|\__\___|\__, |_|  \__,_|\__|_|\___/|_| |_|",
//				@"    |___/                                                  |___/                             ",
//			};

//Starwars Theme
//string[] SystemsText =
//{
//				@"     _______.____    ____  _______.___________. _______ .___  ___.      _______.    __  .__   __. .___________. _______   _______ .______          ___   .___________. __    ______   .__   __. ",
//				@"    /       |\   \  /   / /       |           ||   ____||   \/   |     /       |   |  | |  \ |  | |           ||   ____| /  _____||   _  \        /   \  |           ||  |  /  __  \  |  \ |  | ",
//				@"   |   (----` \   \/   / |   (----`---|  |----`|  |__   |  \  /  |    |   (----`   |  | |   \|  | `---|  |----`|  |__   |  |  __  |  |_)  |      /  ^  \ `---|  |----`|  | |  |  |  | |   \|  | ",
//				@"    \   \      \_    _/   \   \       |  |     |   __|  |  |\/|  |     \   \       |  | |  . `  |     |  |     |   __|  |  | |_ | |      /      /  /_\  \    |  |     |  | |  |  |  | |  . `  | ",
//				@".----)   |       |  | .----)   |      |  |     |  |____ |  |  |  | .----)   |      |  | |  |\   |     |  |     |  |____ |  |__| | |  |\  \----./  _____  \   |  |     |  | |  `--'  | |  |\   | ",
//				@"|_______/        |__| |_______/       |__|     |_______||__|  |__| |_______/       |__| |__| \__|     |__|     |_______| \______| | _| `._____/__/     \__\  |__|     |__|  \______/  |__| \__| ",
//			};
//Trains But doesnt Work :(
//string[] SystemsText =
//{
//				@"            ___     _  _            _                                        ___             _               __ _                    _        _                    ",
//				@"    o O O  / __|   | || |   ___    | |_     ___    _ __     ___      o O O  |_ _|   _ _     | |_     ___    / _` |    _ _   __ _    | |_     (_)     ___    _ _     ",
//				@"   o       \__ \    \_, |  (_-<    |  _|   / -_)  | '  \   (_-<     o        | |   | ' \    |  _|   / -_)   \__, |   | '_| / _` |   |  _|    | |    / _ \  | ' \   ",
//				@"  TS__[O]  |___/   _|__/   /__/_   _\__|   \___|  |_|_|_|  /__/_   TS__[O]  |___|  |_||_|   _\__|   \___|   |___/   _|_|_  \__,_|   _\__|   _|_|_   \___/  |_||_| ",
//				@" {======|_|"""""|_| """"|_|"""""|_|"""""|_|"""""|_|"""""|_|"""""| {======|_|"""""|_|"""""|_|"""""|_|"""""|_|"""""|_|"""""|_|"""""|_|"""""|_|"""""|_|"""""|_|"""""|",
//				@"./o--000'"`-0-0-'"`-0-0-'"`-0-0-'"`-0-0-'"`-0-0-'"`-0-0-'"`-0-0-'./o--000'"`-0-0-'"`-0-0-'"`-0-0-'"`-0-0-'"`-0-0-'"`-0-0-'"`-0-0-'"`-0-0-'"`-0-0-'"`-0-0-'"`-0-0-' ",
//			};