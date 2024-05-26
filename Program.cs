using System;
using server;
using System.IO;
using ws;
using api;
using System.Net;
using System.Diagnostics;
using vaultgamesesh;
using System.Collections.Generic;
using Newtonsoft.Json;
using Servers;

namespace start
{
    class Program
    {
        static void Main()
        {
            //startup for openrec
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Setup.firsttime = true;
            Setup.setup();
            goto Tutorial;

        Tutorial:
            if (Setup.firsttime == true)
            {
               
                Console.Title = "bootloader Intro";
                Console.WriteLine("Welcome!");
                Console.WriteLine("Is this your first time using bootloader server?");
                Console.WriteLine("Yes or No (Y, N)");
                string readline22 = Console.ReadLine();
                if (readline22 == "y" || readline22 == "Y")
                {
                    Console.Clear();
                    Console.Title = "bootloader Tutorial";
                    Console.WriteLine("bootloader server software that bootstrapt old RecRoom versions.");

                    Console.WriteLine("1. Start the recbornserver by pressing 5 on the main menu and selecting your version as follows");
                    Console.WriteLine("2. Run Recroom_Release.exe from the folder of the build you downloaded." + Environment.NewLine);
                    Console.WriteLine("And that's it! Press any key to start the bootloader server:");
                    Console.ReadKey();
                    Console.Clear();
                    goto Start;
                }
                else
                {
                    Console.Clear();
                    goto Start;
                }
            }
            else
            {
                goto Start;
            }

        Start:


            Console.WriteLine("" + Environment.NewLine);
            Console.WriteLine("Made to bootstrap more moden build of rec room");
            Console.WriteLine("from 2021 to 2022");
            Console.WriteLine("" + Environment.NewLine);
            Console.WriteLine("this is early workin progrest");
            Console.WriteLine("" + Environment.NewLine);

            Console.Title = "bootloader server";
            version = "2021";
            Console.WriteLine("starting bootloader server");
            new NameServer();
            //new APIServer_rebuild();
            new ImageServer();
            new APIServer();
            new Late2018WebSock();                    
         
        }
        public static string msg = "//This is the server sending and recieving data from recroom." + Environment.NewLine + "//Ignore this if you don't know what this means." + Environment.NewLine + "//Please start up the build now." + Environment.NewLine + "//and don't forger to start rebornrec server";
        public static string version = "";
        public static string appversion = "0.0.1";
        public static bool bannedflag = false;
    }

}
