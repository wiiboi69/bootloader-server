using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using static server.APIServer;
using System.Threading.Tasks;
using System.Collections.Generic;
using api;
using static System.Net.Mime.MediaTypeNames;
using System.Numerics;
using server;

namespace Servers
{
    internal class APIServer_rebuild
    {
        private HttpListener listener = new HttpListener();

        public APIServer_rebuild()
        {
            try
            {

                Console.WriteLine("[APIServer_rebuild] has started.");
                new Thread(new ThreadStart(this.StartListen)).Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An Exception Occurred while Listening :" + ex.ToString());
            }
        }

        private void StartListen()
        {
            var values = new Dictionary<string, string>
            {

            };
            this.listener.Prefixes.Add("http://localhost:20218/");
            HttpClient client = new HttpClient();
            while (true)
            {
                this.listener.Start();
                Console.WriteLine("[APIServer_rebuild] is listening for a request.");
                HttpListenerContext context = this.listener.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;
                byte[] bytes = null;
                string s = "";
                int timeout = 0;
                int timeout2 = 0;
                string rawUrl = request.RawUrl;
                string Url = "";
                string text;
                string signature = request.Headers.Get("X-RNSIG");
                using (StreamReader streamReader = new StreamReader(request.InputStream, request.ContentEncoding))
                {
                    text = streamReader.ReadToEnd();
                }

                if (rawUrl.StartsWith("/api/"))
                {
                    Url = rawUrl.Remove(0, 5);
                }
                ///playerReputation/v2/
                ///relationships/v2/get
                ///avatar/v4/items
                ////pageview/consume
                if (rawUrl.StartsWith("/api/pageview/consume") || rawUrl.StartsWith("/club/") || rawUrl.StartsWith("/subscription/") || rawUrl.Contains("/api/players/v2/progression/bulk"))
                {
                    s = BracketResponse;
                    goto senddata;


                }
                
                else if (Url == "PlayerReporting/v1/moderationBlockDetails")
                {
                    s = APIServer.ModerationBlockDetails;
                    goto senddata;

                }
                else if (Url.StartsWith("playerReputation/v2/bulk"))
                {
                    s =  "{\"AccountId\":" + CachedPlayerID + ",\"Noteriety\":0,\"CheerGeneral\":1,\"CheerHelpful\":1,\"CheerGreatHost\":1,\"CheerSportsman\":1,\"CheerCreative\":1,\"CheerCredit\":77,\"SubscriberCount\":2,\"SubscribedCount\":0,\"SelectedCheer\":40}";
                    goto senddata;

                }
                else if (rawUrl == "/api/relationships/v2/get"
                    || rawUrl == "/api/gamesight/event"
                    || rawUrl == "/api/config/v1/amplitude"
                    || rawUrl == "/api/PlayerReporting/v1/moderationBlockDetails"
                    || rawUrl == "/api/config/v1/amplitude")
                {
                    Console.WriteLine("API Requested reborn server: " + rawUrl);
                    
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
                            

                        }
                    }
                    goto senddata;
                }
      

                if (rawUrl.StartsWith("/api/"))
                {

                    if (rawUrl == "/api/PlayerReporting/v1/hile")
                    {
                        s = "{\"Message\":\"\",\"Type\":0}";
                    }
                    else if (Url.Contains("versioncheck/v4"))
                    {
                        s = "{\"VersionStatus\":0}";
                    }
                    else if (Url.Contains("storefronts/v3"))
                    {
                        s = BracketResponse;
                    }
                    else if (Url == "settings/v2/set")
                    {
                        Settings.SetPlayerSettings(text);
                    }
                    else if (Url == "avatar/v4/items")
                    {
                        s = File.ReadAllText("SaveData\\avataritems.txt");


                    }
                    //
                    else if (Url == "settings/v2/")
                    {
                        s = File.ReadAllText("SaveData\\settings.txt");
                        

                    }
                    else if (Url == "avatar/v1/defaultunlocked")
                    {
                        s = BracketResponse;
                    }
                    else if (rawUrl.Contains("/api/avatar/v2"))
                    {
                        s = File.ReadAllText("SaveData\\avatar.txt");
                    }
                    else if (Url == ("config/v2"))
                    {
                        s = Config2.GetDebugConfig();
                    }
                    else if (Url == "gameconfigs/v1/all")
                    {
                        s = File.ReadAllText("SaveData\\gameconfigs.txt");
                    }
                    else
                    {
                        goto Getdata;
                    }
                    goto senddata;
                }
                else if (rawUrl == "/connect/token")
                {
                    s = File.ReadAllText("SaveData\\token.txt");
                    goto senddata;

                }
                else if (rawUrl.Contains("quickPlay/v1/getandclear"))
                {
                    s = JsonConvert.SerializeObject((object)new QuickPlayResponseDTO()
                    {
                        TargetPlayerId = new long?(),
                        RoomName = (string)null,
                        ActionCode = (string)null
                    });
                    goto senddata;

                }
                
            Getdata:
                Console.WriteLine("RAWURL: " + request.RawUrl + " | HTTPMETHOD: " + request.HttpMethod + " | POSTDATA: " + text);

                string str1 = "";
                NameValueCollection headers = request.Headers;
                for (int index = 0; index < request.Headers.Count; ++index)
                {
                    string key = headers.GetKey(index);
                    Console.WriteLine(key + " : " + headers.GetValues(key)[0]);
                    if (key == "Authorization")
                    {
                        string str2 = headers.GetValues("Authorization")[0];
                    }
                    if (key == "X-RNSIG")
                        str1 = headers.GetValues(key)[0];
                }
                RestClient restClient = new RestClient("http://localhost:2056");

                RestRequest restRequest1 = new RestRequest(rawUrl, APIServer_rebuild.ParseMethod(request.HttpMethod));
                //restRequest1.AddHeader("Authorization", "Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6ImFIWEFmR1dEWjZXMTNIYWd5U3FPZlZtVFUyRSIsInR5cCI6IkpXVCIsIng1dCI6ImFIWEFmR1dEWjZXMTNIYWd5U3FPZlZtVFUyRSJ9.eyJuYmYiOjE2MzQ5Mjc5NTEsImV4cCI6MTYzNDk3MTE1MSwiaXNzIjoiaHR0cHM6Ly9hdXRoLnJlYy5uZXQiLCJhdWQiOlsiaHR0cHM6Ly9hdXRoLnJlYy5uZXQvcmVzb3VyY2VzIiwicm4uYWNjb3VudHMiLCJybi5hcGkiLCJybi5hdXRoIiwicm4uY2hhdCIsInJuLmNsdWJzIiwicm4uY29tbWVyY2UiLCJybi5sZWFkZXJib2FyZCIsInJuLmxpbmsiLCJybi5tYXRjaCIsInJuLm5vdGlmeSIsInJuLnJvb21jb21tZW50cyIsInJuLnJvb21zIiwicm4uc3RvcmFnZSJdLCJjbGllbnRfaWQiOiJyZWNyb29tIiwicm9sZSI6ImdhbWVDbGllbnQiLCJybi5wbGF0IjoiMSIsInJuLnBsYXRpZCI6IjQ2Nzk4NDI5Nzg3ODk3MTMiLCJybi5kZXZpY2VjbGFzcyI6IjUiLCJybi52ZXIiOiIyMDIxMTAwOCIsInJuLmFzaWQiOiIxNjM0OTI3OTQ5NTQyIiwicm4uc2siOiIxMDgzNjgyNzkzIiwic3ViIjoiMzA2OTIxMTMiLCJhdXRoX3RpbWUiOjE2MzQ5Mjc5NTEsImlkcCI6ImxvY2FsIiwicm4ucGlkIjoiMzA2OTIxMTMiLCJzY29wZSI6WyJybi5hY2NvdW50cyIsInJuLmFjY291bnRzLmdjIiwicm4uYXBpIiwicm4uYXV0aCIsInJuLmF1dGguZ2MiLCJybi5jaGF0Iiwicm4uY2x1YnMiLCJybi5jb21tZXJjZSIsInJuLmxlYWRlcmJvYXJkIiwicm4ubGluayIsInJuLm1hdGNoLnJlYWQiLCJybi5tYXRjaC53cml0ZSIsInJuLm5vdGlmeSIsInJuLnJvb21jb21tZW50cyIsInJuLnJvb21zIiwicm4uc3RvcmFnZSIsIm9mZmxpbmVfYWNjZXNzIl0sImFtciI6WyJjYWNoZWRfbG9naW4iXX0.oCEqD-YeaU-ewmTtpTPnJM5FN8tOrOcAsQhLvFyluf6j3FCTIeSQyCDDcT12CrRdOmaamZwQy4jSccVRLiipdki4OEpQjOx8dmwyrhtlofIQbxl-2Twr0-1p5vLePBB2ePW1qyUOAwZv2J92vXtdq0YWSZAgWs_WjnEIwMLmWlyjY8Cw0M-2Y7RXfKZSYvYaUHOthDCvP1G-7URAznjwenccuSJ7Wx_roHi0hEBZLq5r85zVPD9Gs6gC8nDJcg13CyKQujv0FxziQBGbDGRwZxu-W5XhJOy1u3yMchayUbKC_3OydXlZ-wW7n3ba_QQs3uRqgLtoFcyzd8nHMsB0xQ");
                //restRequest1.AddHeader("X-RNSIG", str1);
               // restRequest1.AddParameter("text/xml", (object)s, ParameterType.RequestBody);
                RestRequest restRequest2 = restRequest1;
                IRestResponse restResponse = restClient.Execute((IRestRequest)restRequest2);
                switch (restResponse.ResponseStatus)
                {
                    case ResponseStatus.Completed:
                        if (restResponse.StatusCode == HttpStatusCode.OK)
                        {
                            try
                            {
                                Console.WriteLine(restResponse.Content);

                                s = restResponse.Content;
                            }
                            catch
                            {
                                Console.WriteLine("ERROR: " + (object)restResponse.StatusCode + " " + restResponse.StatusDescription);
                                
                                break;
                            }
                        }
                        break;
                    case ResponseStatus.Error:
                        Console.WriteLine("ERROR: " + restResponse.ErrorMessage);
                        break;
                    case ResponseStatus.TimedOut:
                        Console.WriteLine("ERROR: Timed out trying to contact RecNet servers");
                        break;
                    default:
                        Console.WriteLine("ERROR: Unknown error occurred");
                        break;
                }

                if (rawUrl == "/player/heartbeat")
                {
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

                senddata:
                Thread.Sleep(400);
                Console.WriteLine(rawUrl);
                if (s.Length > 400)
                {
                    Console.WriteLine("API Response length: " + s.Length);
                }
                else
                {
                    Console.WriteLine("API Response: " + s);
                }
                bytes = Encoding.UTF8.GetBytes(s);
                Stream outputStream = response.OutputStream;
                outputStream.Write(bytes, 0, bytes.Length);
                Thread.Sleep(300);
                outputStream.Close();
                this.listener.Stop();
            }
        }

        public static Method ParseMethod(string method)
        {
            if (method == "GET")
                return Method.GET;
            if (method == "POST")
                return Method.POST;
            if (method == "PUT")
                return Method.PUT;
            if (method == "PATCH")
                return Method.PATCH;
            if (method == "DELETE")
                return Method.DELETE;
            if (method == "COPY")
                return Method.COPY;
            if (method == "HEAD")
                return Method.HEAD;
            return method == "OPTIONS" ? Method.OPTIONS : Method.GET;
        }
    }
}
