using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using api;
using api2018;
using api2017;
using Newtonsoft.Json;
using vaultgamesesh;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
namespace server
{
    internal class APIServer
    {
        public APIServer()
        {
			try
			{
				Console.WriteLine("[APIServer.cs] has started.");
                Console.WriteLine("[APIServer2.cs] has started.");
                Console.WriteLine("[MatchmakingServer.cs] has started.");
                new Thread(new ThreadStart(this.StartListen)).Start();
                new Thread(new ThreadStart(this.StartListen2)).Start();
                new Thread(new ThreadStart(this.StartListen3)).Start();
            }
			catch (Exception ex)
			{
				Console.WriteLine("An Exception Occurred while Listening :" + ex.ToString());
			}
		}
		private void StartListen()
		{
			try
			{
				var values = new Dictionary<string, string>
				  {
  
				  };
				//2 different servers for 3 different stages of the game, the apis change so much idk anymore
				this.listener.Prefixes.Add("http://localhost:" + start.Program.version + "/");
                HttpClient client = new HttpClient();
                if (start.Program.version == "2021")
				{
					for (; ; )
					{
						this.listener.Start();
						HttpListenerContext context = this.listener.GetContext();
						HttpListenerRequest request = context.Request;
						HttpListenerResponse response = context.Response;
						Console.WriteLine("[APIServer.cs] is listening.");
						List<byte> list = new List<byte>();
						string rawUrl = request.RawUrl;
						string Url = "";
                        int timeout = 0;
                        int timeout2 = 0;

                        byte[] bytes = null;
						string signature = request.Headers.Get("X-RNSIG");
						if (rawUrl.StartsWith("/api/"))
						{
							Url = rawUrl.Remove(0, 5);
						}
						if (!(Url == ""))
						{
							Console.WriteLine("API Requested: " + Url);
						}
						else
						{
							Console.WriteLine("API Requested (rawUrl): " + rawUrl);
						}
						string text;
						string s = "";
						using (StreamReader streamReader = new StreamReader(request.InputStream, request.ContentEncoding))
						{
							text = streamReader.ReadToEnd();
						}
						Console.WriteLine("API Data: " + text);

                        senddata:

                        if (rawUrl == "/eac/challenge")
                        {
                            s = File.ReadAllText("SaveData\\challenge.txt");
                        }
                        //else if (rawUrl.Contains("/rooms?name=Orientation"))
                        //{
                        //    rawUrl = "gamesessions/v2/create";

                        //    text = File.ReadAllText("SaveData\\Orientation.json");
                        //    goto senddata;
                        //}
                        //else if (rawUrl.Contains("/rooms"))
                        else if (rawUrl.Contains("/goto/rooms/dormroom"))
                        {
                            File.WriteAllText("SaveData\\DATAROOM.txt", s);
                        }
                        else if (rawUrl.Contains("/rooms?name=Orientation"))
                        {
                            //rawUrl = "gamesessions/v2/create";

                            //text = File.ReadAllText("SaveData\\roomtest.txt");
                            s = File.ReadAllText("SaveData\\room.json");

                            //goto senddata;
                        }
                        else if (Url == "gamesessions/v2/joinrandom")
                        {
                            s = gamesesh.GameSessions.JoinRandom(text);
                        }
                        else if(Url == "gamesessions/v2/create")
                        {
                            s = gamesesh.GameSessions.Create(text);
                        }
                        else if (rawUrl != "/eac/challenge")
                        {
                            Console.WriteLine("API Requested reborn server: " + rawUrl);
                            if (rawUrl == "/connect/token")
                            {
                                s = File.ReadAllText("SaveData\\token.txt");
                            }
                            else
                            {
                                while (s == "")
                                {
                                    if (timeout2 > 3) { break; }
                                    timeout = 0;

                                    async void callbootserver()
                                    {

                                        var content = new FormUrlEncodedContent(values);

                                        try
                                        {
                                            HttpResponseMessage response;

                                            
                                            response = await client.PostAsync("http://localhost:2056" + rawUrl, content);
                                            
                                            s = await response.Content.ReadAsStringAsync();

                                        }
                                        catch (Exception ex4)
                                        {
                                            Console.WriteLine(ex4);

                                            s = BracketResponse;
                                        }
                                    }
                                    callbootserver();
                                    Task.Delay(200);

                                    while (s == "")
                                    {
                                        if (timeout > 16) { timeout2 += 1; break; }

                                        Console.WriteLine("");
                                        timeout += 1;

                                        Task.Delay(200);
                                    }
                                    if (rawUrl.Contains("/rooms"))
                                    {
                                        File.WriteAllText("SaveData\\DATAROOM.txt", s);
                                    }

                                }
                            }
                        }

                        if (s.Length > 400)
                        {
                            Console.WriteLine("API Responsed: " + s.Length);
                        }
                        else
                        {
                            Console.WriteLine("API Response: " + s);
                        }
                        bytes = Encoding.UTF8.GetBytes(s);
						response.ContentLength64 = (long)bytes.Length;
						Stream outputStream = response.OutputStream;
						outputStream.Write(bytes, 0, bytes.Length);
                        Thread.Sleep(300);
                        outputStream.Close();
                        this.listener.Stop();
                    }
				}
			}
			catch (Exception ex4)
			{
				Console.WriteLine(ex4);
				File.WriteAllText("crashdump-api.txt", Convert.ToString(ex4));
				this.listener.Close();
				new APIServer();
			}
		}
        private void StartListen2()
        {
            try
            {
                var values = new Dictionary<string, string>
                {

                };
                //2 different servers for 3 different stages of the game, the apis change so much idk anymore
                this.listener2.Prefixes.Add("http://localhost:" + start.Program.version + "8/");
                HttpClient client = new HttpClient();
                if (start.Program.version == "2021")
                {
                    for (; ; )
                    {
                        this.listener2.Start();
                        Console.WriteLine("[APIServer2.cs] is listening.");
                        HttpListenerContext context = this.listener2.GetContext();
                        HttpListenerRequest request = context.Request;
                        HttpListenerResponse response = context.Response;


                        List<byte> list = new List<byte>();
                        bool dataerr = false;
                        bool dorm = false;
                        string rawUrl = request.RawUrl;
                        string Url = "";
                        int timeout = 0;
                        int timeout2 = 0;
                        byte[] bytes = null;
                        string signature = request.Headers.Get("X-RNSIG");
                        if (rawUrl.StartsWith("/api/"))
                        {
                            Url = rawUrl.Remove(0, 5);
                        }
                        if (!(Url == ""))
                        {
                            Console.WriteLine("API2 Requested: " + Url);
                        }
                        else
                        {
                            Console.WriteLine("API2 Requested (rawUrl): " + rawUrl);
                        }
                        string text;
                        string s = "";
                        using (StreamReader streamReader = new StreamReader(request.InputStream, request.ContentEncoding))
                        {
                            text = streamReader.ReadToEnd();
                        }
                        Console.WriteLine("API2 Data: " + text);

                        if (rawUrl == "/api/avatar/v1/defaultunlocked")
                        {
                            s = BracketResponse;
                        }
                        else if (rawUrl != "/api/avatar/v1/defaultunlocked")
                        {
                            Console.WriteLine("API2 Requested reborn server: " + rawUrl);
                        senddata:
                            if (rawUrl == "/connect/token")
                            {
                                s = File.ReadAllText("SaveData\\token.txt");
                            }
                            else if (rawUrl == "/api/PlayerReporting/v1/hile")
                            {
                                s = "{\"Message\":\"\",\"Type\":0}";
                            }
                            else if (Url == "gameconfigs/v1/all")
                            {
                                s = File.ReadAllText("SaveData\\gameconfigs.txt");
                            }
                            else if (rawUrl.Contains("/club/"))
                            {
                                s = BracketResponse;
                            }
                            else if (rawUrl.Contains("/api/storefronts/v3"))
                            {
                                s = BracketResponse;
                            }
                            else if (rawUrl.Contains("quickPlay/v1/getandclear"))
                            {
                                s = JsonConvert.SerializeObject((object)new QuickPlayResponseDTO()
                                {
                                    TargetPlayerId = new long?(),
                                    RoomName = (string)null,
                                    ActionCode = (string)null
                                });
                            }
                            /*else if (rawUrl.Contains("/rooms?name=Orientation"))
                            {
                                Task.Delay(200);

                                //rawUrl = "goto/rooms/dormroom";
                                s = File.ReadAllText("SaveData\\Orientation2.json");
                                
                                Roomold dictionary = JsonConvert.DeserializeObject<Roomold>(s);
                                try
                                {
                                    s = JsonConvert.SerializeObject(new Room
                                    {
                                        errorCode = dictionary.errorCode,

                                        Roomv2 = new Roomv2
                                        {

                                            RoomId = dictionary.roomInstance.roomId,

                                            IsDorm = false,

                                            MaxPlayerCalculationMode = 0,
                                            MaxPlayers = dictionary.roomInstance.maxCapacity,
                                            CloningAllowed = true,
                                            DisableMicAutoMute = true,
                                            DisableRoomComments = false,
                                            EncryptVoiceChat = false,
                                            LoadScreenLocked = false,
                                            Name = dictionary.roomInstance.name,
                                            Description = "",
                                            ImageName = "",
                                            WarningMask = 0,
                                            CustomWarning = 0,
                                            CreatorAccountId = 0,
                                            State = 0,
                                            Accessibility = 0,
                                            SupportsLevelVoting = false,
                                            IsRRO = false,
                                            SupportsScreens = true,
                                            SupportsWalkVR = true,
                                            SupportsTeleportVR = true,
                                            SupportsVRLow = true,
                                            SupportsQuest2 = true,
                                            SupportsMobile = true,
                                            SupportsJuniors = true,
                                            MinLevel = 0,
                                            CreatedAt = DateTime.UtcNow,
                                            Stats = new Stats()
                                            {
                                                CheerCount = 1,
                                                FavoriteCount = 1,
                                                VisitCount = 1,
                                                VisitorCount = 1
                                            }
                                        }
                                    });
                                }
                                catch (Exception ex4)
                                {
                                    Console.WriteLine(ex4);

                                    s = File.ReadAllText("SaveData\\dormhart.txt");

                                }
                                
                                //goto senddata;
                            }
                            else if (rawUrl.Contains("/rooms"))
                            {
                                Task.Delay(200);

                                //rawUrl = "gamesessions/v2/create";
                                //s = File.ReadAllText("SaveData\\Orientation.json");
                                s = File.ReadAllText("SaveData\\Orientation.json");
                                
                                Roomold dictionary = JsonConvert.DeserializeObject<Roomold>(s);
                                try
                                {
                                    s = JsonConvert.SerializeObject(new Room
                                    {
                                        errorCode = dictionary.errorCode,

                                        Roomv2 = new Roomv2
                                        {

                                            RoomId = dictionary.roomInstance.roomId,

                                            IsDorm = false,

                                            MaxPlayerCalculationMode = 0,
                                            MaxPlayers = dictionary.roomInstance.maxCapacity,
                                            CloningAllowed = true,
                                            DisableMicAutoMute = true,
                                            DisableRoomComments = false,
                                            EncryptVoiceChat = false,
                                            LoadScreenLocked = false,
                                            Name = dictionary.roomInstance.name,
                                            Description = "",
                                            ImageName = "",
                                            WarningMask = 0,
                                            CustomWarning = 0,
                                            CreatorAccountId = 0,
                                            State = 0,
                                            Accessibility = 0,
                                            SupportsLevelVoting = false,
                                            IsRRO = false,
                                            SupportsScreens = true,
                                            SupportsWalkVR = true,
                                            SupportsTeleportVR = true,
                                            SupportsVRLow = true,
                                            SupportsQuest2 = true,
                                            SupportsMobile = true,
                                            SupportsJuniors = true,
                                            MinLevel = 0,
                                            CreatedAt = DateTime.UtcNow,
                                            Stats = new Stats()
                                            {
                                                CheerCount = 1,
                                                FavoriteCount = 1,
                                                VisitCount = 1,
                                                VisitorCount = 1
                                            }
                                        }
                                    });
                                }
                                catch (Exception ex4)
                                {
                                    Console.WriteLine(ex4);

                                    s = File.ReadAllText("SaveData\\dormhart.txt");

                                }

                                //s = File.ReadAllText("SaveData\\roomtest.txt");
                                //goto senddata;
                            }*/
                            else if (Url == "settings/v2/set")
                            {

                                Settings.SetPlayerSettings(text);

                            }
                            else if (Url == "settings/v2/")
                            {
                                s = File.ReadAllText("SaveData\\settings.txt");
                            }
                            else if (rawUrl.Contains("/goto/rooms/dormroom"))
                            {
                                s = File.ReadAllText("SaveData\\room.json");
                            }
                            /*else if (rawUrl.Contains("/goto/rooms/dormroom"))
                            {
                                Task.Delay(200);

                                //rawUrl = "goto/rooms/dormroom";RecRoom_Data\il2cpp_data\Metadata\global-metadata.dat
                                s = File.ReadAllText("SaveData\\roomtest.txt");
                                /*Roomold dictionary = JsonConvert.DeserializeObject<Roomold>(s);
                                try
                                {
                                    s = JsonConvert.SerializeObject(new Room
                                    {
                                        errorCode = dictionary.errorCode,

                                        Roomv2 = new Roomv2
                                        {

                                            RoomId = dictionary.roomInstance.roomId,

                                            IsDorm = true,

                                            MaxPlayerCalculationMode = 0,
                                            MaxPlayers = dictionary.roomInstance.maxCapacity,
                                            CloningAllowed = true,
                                            DisableMicAutoMute = true,
                                            DisableRoomComments = false,
                                            EncryptVoiceChat = false,
                                            LoadScreenLocked = false,
                                            Name = dictionary.roomInstance.name,
                                            Description = "",
                                            ImageName = "",
                                            WarningMask = 0,
                                            CustomWarning = 0,
                                            CreatorAccountId = 0,
                                            State = 0,
                                            Accessibility = 0,
                                            SupportsLevelVoting = false,
                                            IsRRO = false,
                                            SupportsScreens = true,
                                            SupportsWalkVR = true,
                                            SupportsTeleportVR = true,
                                            SupportsVRLow = true,
                                            SupportsQuest2 = true,
                                            SupportsMobile = true,
                                            SupportsJuniors = true,
                                            MinLevel = 0,
                                            CreatedAt = DateTime.UtcNow,
                                            Stats = new Stats()
                                            {
                                                CheerCount = 1,
                                                FavoriteCount = 1,
                                                VisitCount = 1,
                                                VisitorCount = 1
                                            }
                                        }
                                    });
                                }
                                catch (Exception ex4)
                                {
                                    Console.WriteLine(ex4);

                                    s = File.ReadAllText("SaveData\\dormhart.txt");

                                }
                                //goto senddata;
                            }*/
                            else if (rawUrl.Contains("/api/avatar/v2"))
                            {
                                //rawUrl = "goto/rooms/dormroom";RecRoom_Data\il2cpp_data\Metadata\global-metadata.dat
                                s = File.ReadAllText("SaveData\\avatar.txt");
                                //goto senddata;
                            }
                            else if (Url == ("config/v2"))
                            {
                                s = Config2.GetDebugConfig();
                            }
                            ///api/avatar/v2
                            else
                            {

                                while (s == "")
                                {
                                    if (timeout2 > 8) { goto loaddata; }
                                    timeout = 0;

                                    async void callbootserver()
                                    {

                                        var content = new FormUrlEncodedContent(values);
                                        try
                                        {
                                            HttpResponseMessage response;

                                            if (rawUrl.Contains("/rooms"))
                                            {
                                                response = await client.PostAsync("https://match.rec.net" + rawUrl, content);
                                            }
                                            else
                                            {
                                                response = await client.PostAsync("http://localhost:2056" + rawUrl, content);
                                            }
                                            s = await response.Content.ReadAsStringAsync();


                                        }
                                        catch (Exception ex4)
                                        {
                                            Console.WriteLine(ex4);
                                            dataerr = true;
                                            s = BracketResponse;
                                        }
                                    }
                                    callbootserver();
                                    Task.Delay(200);

                                    while (s == "")
                                    {
                                        if (timeout > 32) { timeout2 += 1; break; }
                                        Console.WriteLine("");


                                        timeout += 1;

                                        Task.Delay(200);
                                    }
                                }
                                if (rawUrl.Contains("/goto/rooms/dormroom"))
                                {
                                    File.WriteAllText("SaveData\\DATAROOM.txt", s);
                                }
                                else if (rawUrl.Contains("/goto/rooms"))
                                {

                                    Roomold dictionary = JsonConvert.DeserializeObject<Roomold>(s);
                                    try
                                    {
                                        if (rawUrl.Contains("/goto/rooms/dormroom"))
                                        {
                                            dorm = true;
                                        }
                                        else
                                        {
                                            dorm = false;
                                        }

                                        s = JsonConvert.SerializeObject(new Room
                                        {
                                            errorCode = dictionary.errorCode,

                                            Roomv2 = new Roomv2
                                            {

                                                RoomId = dictionary.roomInstance.roomId,

                                                IsDorm = dorm,

                                                MaxPlayerCalculationMode = 0,
                                                MaxPlayers = dictionary.roomInstance.maxCapacity,
                                                CloningAllowed = true,
                                                DisableMicAutoMute = true,
                                                DisableRoomComments = false,
                                                EncryptVoiceChat = false,
                                                LoadScreenLocked = false,
                                                Name = dictionary.roomInstance.name,
                                                Description = "",
                                                ImageName = "",
                                                WarningMask = 0,
                                                CustomWarning = 0,
                                                CreatorAccountId = 0,
                                                State = 0,
                                                Accessibility = 0,
                                                SupportsLevelVoting = false,
                                                IsRRO = false,
                                                SupportsScreens = true,
                                                SupportsWalkVR = true,
                                                SupportsTeleportVR = true,
                                                SupportsVRLow = true,
                                                SupportsQuest2 = true,
                                                SupportsMobile = true,
                                                SupportsJuniors = true,
                                                MinLevel = 0,
                                                CreatedAt = DateTime.UtcNow,
                                                Stats = new Stats()
                                                {
                                                    CheerCount = 1,
                                                    FavoriteCount = 1,
                                                    VisitCount = 1,
                                                    VisitorCount = 1
                                                }
                                            }
                                        });
                                    }
                                    catch (Exception ex4)
                                    {
                                        Console.WriteLine(ex4);

                                        s = File.ReadAllText("SaveData\\dormhart.txt");

                                    }

                                }
                                if (dataerr == false)
                                {
                                    goto Skipdata;
                                }
                                Console.WriteLine("using localapi");
                            loaddata:
                                if (rawUrl == "/api/avatar/v4/items")
                                {
                                    s = File.ReadAllText("SaveData\\avataritems.txt");
                                }

                                else
                                {
                                    s = BracketResponse;
                                }
                                dataerr = false;

                            }
                        }
                    Skipdata:
                        if (s.Length > 400)
                        {
                            Console.WriteLine("API2 Responsed: " + s.Length);
                        }
                        else { 
                            Console.WriteLine("API2 Response: " + s);
                        }
                        bytes = Encoding.UTF8.GetBytes(s);
                        response.ContentLength64 = (long)bytes.Length;
                        Stream outputStream = response.OutputStream;
                        outputStream.Write(bytes, 0, bytes.Length);
                        Thread.Sleep(300);
                        outputStream.Close();
                        this.listener2.Stop();  
                    }
                }
            }
            catch (Exception ex4)
            {
                Console.WriteLine(ex4);
                File.WriteAllText("crashdump-api2.txt", Convert.ToString(ex4));
                this.listener2.Close();
                new APIServer();
            }
        }

        private void StartListen3()
        {
            try
            {
                var values = new Dictionary<string, string>
                {

                };
                //2 different servers for 3 different stages of the game, the apis change so much idk anymore
                this.listener3.Prefixes.Add("http://localhost:" + start.Program.version + "6/");
                HttpClient client = new HttpClient();
                if (start.Program.version == "2021")
                {
                    for (; ; )
                    {
                        this.listener3.Start();
                        HttpListenerContext context = this.listener3.GetContext();
                        HttpListenerRequest request = context.Request;
                        HttpListenerResponse response = context.Response;
                        Console.WriteLine("[MatchmakingServer.cs] is listening.");
                        List<byte> list = new List<byte>();
                        string rawUrl = request.RawUrl;
                        string Url = "";
                        int timeout = 0;
                        int timeout2 = 0;
                        string reboud = "";
                        byte[] bytes = null;
                        string signature = request.Headers.Get("X-RNSIG");
                        if (rawUrl.StartsWith("/api/"))
                        {
                            Url = rawUrl.Remove(0, 5);
                        }
                        if (!(Url == ""))
                        {
                            Console.WriteLine("API Requested: " + Url);
                        }
                        else
                        {
                            Console.WriteLine("API Requested (rawUrl): " + rawUrl);
                        }
                        string text;
                        string s = "";

                        using (StreamReader streamReader = new StreamReader(request.InputStream, request.ContentEncoding))
                        {
                            text = streamReader.ReadToEnd();
                        }
                        Console.WriteLine("API Data: " + text);

                        if (rawUrl != "/player/heartbeat")
                        {
                            s = File.ReadAllText("SaveData\\challenge.txt");
                        }
                        else if (rawUrl.Contains("/goto/rooms/dormroom"))
                        {
                            File.WriteAllText("SaveData\\DATAROOM.txt", s);
                        }
                        else if (rawUrl == "/player/heartbeat")
                        {
                            Console.WriteLine("API Requested reborn server: " + rawUrl);
                            {
                                while (s == "")
                                {
                                    if (timeout2 > 6) {
                                        
                                        break; 
                                    }
                                    timeout = 0;

                                    async void callbootserver()
                                    {

                                        var content = new FormUrlEncodedContent(values);

                                        try
                                        {
                                            var response = await client.PostAsync("http://localhost:2056" + rawUrl, content);
                                            s = await response.Content.ReadAsStringAsync();

                                        }
                                        catch (Exception ex4)
                                        {
                                            Console.WriteLine(ex4);



                                            s = File.ReadAllText("SaveData\\dormhart.txt");
                                        }
                                    }
                                    callbootserver();
                                    Task.Delay(200);

                                    while (s == "")
                                    {
                                        if (timeout > 64) { timeout2 += 1; break; }

                                        Console.WriteLine("");

                                        timeout += 1;

                                        Task.Delay(200);
                                    }
                                }

                                PlayerPresence dictionary = JsonConvert.DeserializeObject<PlayerPresence>(s);
                                try
                                {
                                    s = JsonConvert.SerializeObject(new PlayerPresencev2
                                    {
                                        deviceClass = dictionary.deviceClass,
                                        playerId = dictionary.playerId,
                                        statusVisibility = 0,
                                        vrMovementMode = dictionary.vrMovementMode,
                                        roomInstance = new RoomInstancev2
                                        {
                                            roomInstanceId = dictionary.roomInstance.roomInstanceId,

                                            roomId = dictionary.roomInstance.roomId,

                                            subRoomId = dictionary.roomInstance.subRoomId,

                                            location = dictionary.roomInstance.location,

                                            roomInstanceType = 1,

                                            dataBlob = dictionary.roomInstance.dataBlob,

                                            photonRegionId = dictionary.roomInstance.photonRegionId,

                                            photonRoomId = dictionary.roomInstance.photonRoomId,

                                            name = dictionary.roomInstance.name,

                                            maxCapacity = dictionary.roomInstance.maxCapacity,

                                            isFull = dictionary.roomInstance.isFull,

                                            isPrivate = dictionary.roomInstance.isPrivate,

                                            isInProgress = dictionary.roomInstance.isInProgress,
                                        },
                                        isOnline = true,
                                        appVersion = "20221008"

                                    });
                                }
                                catch (Exception ex4)
                                {
                                    Console.WriteLine(ex4);

                                    s = File.ReadAllText("SaveData\\dormhart.txt");

                                }

                            }
                        }

                        if (s.Length > 400)
                        {
                            Console.WriteLine("Matchmaking Responsed: " + s.Length);
                        }
                        else
                        {
                            Console.WriteLine("Matchmaking Response: " + s);
                        }
                        bytes = Encoding.UTF8.GetBytes(s);
                        response.ContentLength64 = (long)bytes.Length;
                        Stream outputStream = response.OutputStream;
                        outputStream.Write(bytes, 0, bytes.Length);
                        Thread.Sleep(300);
                        outputStream.Close();
                        this.listener3.Stop();
                    }
                }
            }
            catch (Exception ex4)
            {
                Console.WriteLine(ex4);
                File.WriteAllText("crashdump-Matchmaking.txt", Convert.ToString(ex4));
                this.listener3.Close();
                new APIServer();
            }
        }



        public static ulong CachedPlayerID = ulong.Parse(File.ReadAllText("SaveData\\Profile\\userid.txt"));
		public static ulong CachedPlatformID = 10000;
		public static int CachedVersionMonth = 01;

		public static string BlankResponse = "";
		public static string BracketResponse = "[]";

		public static string PlayerEventsResponse = "{\"Created\":[],\"Responses\":[]}";
		public static string VersionCheckResponse2 = "{\"VersionStatus\":0}";
		public static string VersionCheckResponse = "{\"ValidVersion\":true}";
		public static string ModerationBlockDetails = "{\"ReportCategory\":0,\"Duration\":0,\"GameSessionId\":0,\"Message\":\"\"}";
		public static string ImagesV2Named = "[{\"FriendlyImageName\":\"DormRoomBucket\",\"ImageName\":\"DormRoomBucket\",\"StartTime\":\"2021-12-27T21:27:38.1880175-08:00\",\"EndTime\":\"2025-12-27T21:27:38.1880399-08:00\"}";
		public static string ChallengesV1GetCurrent = "{\"Success\":true,\"Message\":\"OpenRec\"}";
		public static string ChecklistV1Current = "[{\"Order\":0,\"Objective\":3000,\"Count\":3,\"CreditAmount\":100},{\"Order\":1,\"Objective\":3001,\"Count\":3,\"CreditAmount\":100},{\"Order\":2,\"Objective\":3002,\"Count\":3,\"CreditAmount\":100}]";

		public static string Banned = "{\"ReportCategory\":1,\"Duration\":10000000000000000,\"GameSessionId\":100,\"Message\":\"You have been banned. You are probably a little kid and are now whining at your VR headset. If you aren't a little kid, DM me to appeal.\"}";

		private HttpListener listener = new HttpListener();
        private HttpListener listener2 = new HttpListener();
        private HttpListener listener3 = new HttpListener();

        public class QuickPlayResponseDTO
        {
            public long? TargetPlayerId { get; set; }

            public string RoomName { get; set; }

            public string ActionCode { get; set; }
        }


        public class Room
        {
            public int errorCode { get; set; }

            public Roomv2 Roomv2 { get; set; }

        }

        public class Roomold
        {
            public int errorCode { get; set; }

            public RoomInstance roomInstance { get; set; }

        }
      

        public class Roomv2
        {

            public int RoomId { get; set; }

            public bool IsDorm { get; set; }

            public int MaxPlayerCalculationMode { get; set; }

            public int MaxPlayers { get; set; }

            public bool CloningAllowed { get; set; }

            public bool DisableMicAutoMute { get; set; }

            public bool DisableRoomComments { get; set; }

            public bool EncryptVoiceChat { get; set; }

            public bool LoadScreenLocked { get; set; }

            public string Name { get; set; }

            public string Description { get; set; }

            public string ImageName { get; set; }

            public int WarningMask { get; set; }

            public object CustomWarning { get; set; }

            public int CreatorAccountId { get; set; }

            public int State { get; set; }

            public int Accessibility { get; set; }

            public bool SupportsLevelVoting { get; set; }

            public bool IsRRO { get; set; }

            public bool SupportsScreens { get; set; }

            public bool SupportsWalkVR { get; set; }

            public bool SupportsTeleportVR { get; set; }

            public bool SupportsVRLow { get; set; }

            public bool SupportsQuest2 { get; set; }

            public bool SupportsMobile { get; set; }

            public bool SupportsJuniors { get; set; }

            public int MinLevel { get; set; }

            public DateTime CreatedAt { get; set; }

            public Stats Stats { get; set; }
        }

        public class Stats
        {
            public int CheerCount { get; set; }

            public int FavoriteCount { get; set; }

            public int VisitorCount { get; set; }

            public int VisitCount { get; set; }
        }

        public class PlayerPresence
        {
            public int playerId { get; set; }

            public PlayerStatusVisibility statusVisibility { get; set; }

            public DeviceClass deviceClass { get; set; }

            public VRMovementMode vrMovementMode { get; set; }

            public RoomInstance roomInstance { get; set; }

        }

        public class PlayerPresencev2
        {
            public int playerId { get; set; }

            public PlayerStatusVisibility statusVisibility { get; set; }

            public DeviceClass deviceClass { get; set; }

            public VRMovementMode vrMovementMode { get; set; }

            public RoomInstancev2 roomInstance { get; set; }

            public bool isOnline { get; set; }

            public string appVersion { get; set; }
        }

        public enum DeviceClass
        {
            Unknown,
            VR,
            Screen,
            Mobile,
            VRLow,
        }
        public enum VRMovementMode
        {
            TELEPORT,
            WALK,
        }

        public enum PlayerStatusVisibility
        {
            Public,
            FriendsOnly,
            FavoriteFriendsOnly,
            Offline,
        }

        public class RoomInstance
        {
            public int roomInstanceId { get; set; }

            public int roomId { get; set; }

            public int subRoomId { get; set; }

            public string location { get; set; }

            public string dataBlob { get; set; }

            public string photonRegionId { get; set; }

            public string photonRoomId { get; set; }

            public string name { get; set; }

            public int maxCapacity { get; set; }

            public bool isFull { get; set; }

            public bool isPrivate { get; set; }

            public bool isInProgress { get; set; }
        }
        public class RoomInstancev2
        {
            public int roomInstanceId { get; set; }

            public int roomId { get; set; }

            public int subRoomId { get; set; }

            public string location { get; set; }

            public int roomInstanceType { get; set; }

            public string dataBlob { get; set; }

            public string photonRegionId { get; set; }

            public string photonRoomId { get; set; }

            public string name { get; set; }

            public int maxCapacity { get; set; }

            public bool isFull { get; set; }

            public bool isPrivate { get; set; }

            public bool isInProgress { get; set; }
        }
    }

}
