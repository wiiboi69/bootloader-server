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

namespace Servers
{
    internal class MatchServer
    {
        private HttpListener listener = new HttpListener();

        public MatchServer()
        {
            try
            {

                Console.WriteLine("[MatchServer] has started.");
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
            this.listener.Prefixes.Add("http://localhost:56704/");
            HttpClient client = new HttpClient();
            while (true)
            {
                this.listener.Start();
                Console.WriteLine("[MatchServer] is listening for a request.");
                HttpListenerContext context = this.listener.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;
                string s = "";
                int timeout = 0;
                int timeout2 = 0;
                string rawUrl = request.RawUrl;
                string end;
                using (StreamReader streamReader = new StreamReader(request.InputStream, request.ContentEncoding))
                {
                    end = streamReader.ReadToEnd();
                }


                Console.WriteLine("RAWURL: " + request.RawUrl + " | HTTPMETHOD: " + request.HttpMethod + " | POSTDATA: " + end);


                if (rawUrl == "/player/heartbeat")
                {
                    Console.WriteLine("API Requested reborn server: " + rawUrl);
                    {
                        while (s == "")
                        {
                            if (timeout2 > 6)
                            {

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
                    goto senddata;
                }
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
                RestClient restClient = new RestClient("http://match.rec.net");
                RestRequest restRequest1 = new RestRequest(rawUrl, MatchServer.ParseMethod(request.HttpMethod));
                restRequest1.AddHeader("Authorization", "Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6ImFIWEFmR1dEWjZXMTNIYWd5U3FPZlZtVFUyRSIsInR5cCI6IkpXVCIsIng1dCI6ImFIWEFmR1dEWjZXMTNIYWd5U3FPZlZtVFUyRSJ9.eyJuYmYiOjE2MzQ5Mjc5NTEsImV4cCI6MTYzNDk3MTE1MSwiaXNzIjoiaHR0cHM6Ly9hdXRoLnJlYy5uZXQiLCJhdWQiOlsiaHR0cHM6Ly9hdXRoLnJlYy5uZXQvcmVzb3VyY2VzIiwicm4uYWNjb3VudHMiLCJybi5hcGkiLCJybi5hdXRoIiwicm4uY2hhdCIsInJuLmNsdWJzIiwicm4uY29tbWVyY2UiLCJybi5sZWFkZXJib2FyZCIsInJuLmxpbmsiLCJybi5tYXRjaCIsInJuLm5vdGlmeSIsInJuLnJvb21jb21tZW50cyIsInJuLnJvb21zIiwicm4uc3RvcmFnZSJdLCJjbGllbnRfaWQiOiJyZWNyb29tIiwicm9sZSI6ImdhbWVDbGllbnQiLCJybi5wbGF0IjoiMSIsInJuLnBsYXRpZCI6IjQ2Nzk4NDI5Nzg3ODk3MTMiLCJybi5kZXZpY2VjbGFzcyI6IjUiLCJybi52ZXIiOiIyMDIxMTAwOCIsInJuLmFzaWQiOiIxNjM0OTI3OTQ5NTQyIiwicm4uc2siOiIxMDgzNjgyNzkzIiwic3ViIjoiMzA2OTIxMTMiLCJhdXRoX3RpbWUiOjE2MzQ5Mjc5NTEsImlkcCI6ImxvY2FsIiwicm4ucGlkIjoiMzA2OTIxMTMiLCJzY29wZSI6WyJybi5hY2NvdW50cyIsInJuLmFjY291bnRzLmdjIiwicm4uYXBpIiwicm4uYXV0aCIsInJuLmF1dGguZ2MiLCJybi5jaGF0Iiwicm4uY2x1YnMiLCJybi5jb21tZXJjZSIsInJuLmxlYWRlcmJvYXJkIiwicm4ubGluayIsInJuLm1hdGNoLnJlYWQiLCJybi5tYXRjaC53cml0ZSIsInJuLm5vdGlmeSIsInJuLnJvb21jb21tZW50cyIsInJuLnJvb21zIiwicm4uc3RvcmFnZSIsIm9mZmxpbmVfYWNjZXNzIl0sImFtciI6WyJjYWNoZWRfbG9naW4iXX0.oCEqD-YeaU-ewmTtpTPnJM5FN8tOrOcAsQhLvFyluf6j3FCTIeSQyCDDcT12CrRdOmaamZwQy4jSccVRLiipdki4OEpQjOx8dmwyrhtlofIQbxl-2Twr0-1p5vLePBB2ePW1qyUOAwZv2J92vXtdq0YWSZAgWs_WjnEIwMLmWlyjY8Cw0M-2Y7RXfKZSYvYaUHOthDCvP1G-7URAznjwenccuSJ7Wx_roHi0hEBZLq5r85zVPD9Gs6gC8nDJcg13CyKQujv0FxziQBGbDGRwZxu-W5XhJOy1u3yMchayUbKC_3OydXlZ-wW7n3ba_QQs3uRqgLtoFcyzd8nHMsB0xQ");
                restRequest1.AddHeader("X-RNSIG", str1);
                restRequest1.AddParameter("text/xml", (object)s, ParameterType.RequestBody);
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
                                File.WriteAllText("SaveData\\DATAROOM.txt", restResponse.Content);
                            }
                            catch
                            {
                                Console.WriteLine("ERROR: Malformed login response");
                                break;
                            }
                        }
                        Console.WriteLine("ERROR: " + (object)restResponse.StatusCode + " " + restResponse.StatusDescription);
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
                senddata:
                Thread.Sleep(400);
                Console.WriteLine(rawUrl);
                Console.WriteLine(s);
                byte[] bytes = Encoding.UTF8.GetBytes(s);
                response.ContentLength64 = (long)bytes.Length;
                Stream outputStream = response.OutputStream;
                outputStream.Write(bytes, 0, bytes.Length);
                Thread.Sleep(200);
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
