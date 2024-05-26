using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;

namespace start
{
	class Setup
	{
		public static bool firsttime = false;
		public static void setup()
		{
			//sets up all the important files so openrec doesnt crash like lame vaultserver xD
			Console.WriteLine("Setting up... ");

			if ((File.Exists("SaveData\\App\\firsttime.txt") && File.Exists("SaveData\\avatar.txt")))
			{

				firsttime = false;
			}
			else {
                Console.WriteLine("did you install this to the same place as rebornrec server");
				Console.ReadKey();
                System.Environment.Exit(1);
            }
			Console.WriteLine("Done!");
			Console.Clear();
		}
	}
}
