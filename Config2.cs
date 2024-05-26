using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using api;

namespace api
{
	// Token: 0x02000013 RID: 19
	internal class Config2
	{
        // Token: 0x17000062 RID: 98
        // (get) Token: 0x06000121 RID: 289 RVA: 0x00009153 File Offset: 0x00007353
        // (set) Token: 0x06000122 RID: 290 RVA: 0x0000915B File Offset: 0x0000735B
        public string MessageOfTheDay { get; set; }

        // Token: 0x17000063 RID: 99
        // (get) Token: 0x06000123 RID: 291 RVA: 0x00009164 File Offset: 0x00007364
        // (set) Token: 0x06000124 RID: 292 RVA: 0x0000916C File Offset: 0x0000736C
        public string CdnBaseUri { get; set; }

        // Token: 0x17000064 RID: 100
        // (get) Token: 0x06000125 RID: 293 RVA: 0x00009175 File Offset: 0x00007375
        // (set) Token: 0x06000126 RID: 294 RVA: 0x0000917D File Offset: 0x0000737D
        public string ShareBaseUrl { get; set; }

        // Token: 0x17000013 RID: 19
        // (get) Token: 0x06000054 RID: 84 RVA: 0x00002237 File Offset: 0x00000437
        // (set) Token: 0x06000055 RID: 85 RVA: 0x0000223F File Offset: 0x0000043F
        public List<LevelProgressionEntry> LevelProgressionMaps { get; set; }

        // Token: 0x17000066 RID: 102
        // (get) Token: 0x06000129 RID: 297 RVA: 0x00009197 File Offset: 0x00007397
        // (set) Token: 0x0600012A RID: 298 RVA: 0x0000919F File Offset: 0x0000739F
        public MatchPrams MatchmakingParams { get; set; }

        // Token: 0x17000067 RID: 103
        // (get) Token: 0x0600012B RID: 299 RVA: 0x000091A8 File Offset: 0x000073A8
        // (set) Token: 0x0600012C RID: 300 RVA: 0x000091B0 File Offset: 0x000073B0
        public ServerMaintainence ServerMaintainence2 { get; set; }

        // Token: 0x17000068 RID: 104
        // (get) Token: 0x0600012D RID: 301 RVA: 0x000091B9 File Offset: 0x000073B9
        // (set) Token: 0x0600012E RID: 302 RVA: 0x000091C1 File Offset: 0x000073C1
        public Objective[][] DailyObjectives { get; set; }

        // Token: 0x17000069 RID: 105
        // (get) Token: 0x0600012F RID: 303 RVA: 0x000091CA File Offset: 0x000073CA
        // (set) Token: 0x06000130 RID: 304 RVA: 0x000091D2 File Offset: 0x000073D2
        public List<ConfigTableEntry> ConfigTable { get; set; }

        // Token: 0x1700006A RID: 106
        // (get) Token: 0x06000131 RID: 305 RVA: 0x000091DB File Offset: 0x000073DB
        // (set) Token: 0x06000132 RID: 306 RVA: 0x000091E3 File Offset: 0x000073E3
        public Config2.PhotonConfig2 PhotonConfig { get; set; }

        // Token: 0x1700006B RID: 107
        // (get) Token: 0x06000133 RID: 307 RVA: 0x000091EC File Offset: 0x000073EC
        // (set) Token: 0x06000134 RID: 308 RVA: 0x000091F4 File Offset: 0x000073F4
        public AutoMicMutingConfig AutoMicMutingConfig2 { get; set; }

        // Token: 0x0600005E RID: 94 RVA: 0x00005450 File Offset: 0x00003650
        public static string GetDebugConfig()
        {

            return JsonConvert.SerializeObject(new Config2
            {
                MessageOfTheDay = new WebClient().DownloadString("https://raw.githubusercontent.com/recroom2016/OpenRec/master/Update/motd.txt"),
                CdnBaseUri = "http://localhost:20182/",

                LevelProgressionMaps = new List<LevelProgressionEntry>
                {
                    new LevelProgressionEntry
                    {
                        Level = 0,
                        RequiredXp = 1
                    },
                    new LevelProgressionEntry
                    {
                        Level = 1,
                        RequiredXp = 2
                    },
                    new LevelProgressionEntry
                    {
                        Level = 2,
                        RequiredXp = 3
                    },
                    new LevelProgressionEntry
                    {
                        Level = 3,
                        RequiredXp = 4
                    },
                    new LevelProgressionEntry
                    {
                        Level = 4,
                        RequiredXp = 5
                    },
                    new LevelProgressionEntry
                    {
                        Level = 5,
                        RequiredXp = 6
                    },
                    new LevelProgressionEntry
                    {
                        Level = 6,
                        RequiredXp = 7
                    },
                    new LevelProgressionEntry
                    {
                        Level = 7,
                        RequiredXp = 8
                    },
                    new LevelProgressionEntry
                    {
                        Level = 8,
                        RequiredXp = 9
                    },
                    new LevelProgressionEntry
                    {
                        Level = 9,
                        RequiredXp = 10
                    },
                    new LevelProgressionEntry
                    {
                        Level = 10,
                        RequiredXp = 11
                    },
                    new LevelProgressionEntry
                    {
                        Level = 11,
                        RequiredXp = 12
                    },
                    new LevelProgressionEntry
                    {
                        Level = 12,
                        RequiredXp = 13
                    },
                    new LevelProgressionEntry
                    {
                        Level = 13,
                        RequiredXp = 14
                    },
                    new LevelProgressionEntry
                    {
                        Level = 14,
                        RequiredXp = 15
                    },
                    new LevelProgressionEntry
                    {
                        Level = 15,
                        RequiredXp = 16
                    },
                    new LevelProgressionEntry
                    {
                        Level = 16,
                        RequiredXp = 17
                    },
                    new LevelProgressionEntry
                    {
                        Level = 17,
                        RequiredXp = 18
                    },
                    new LevelProgressionEntry
                    {
                        Level = 18,
                        RequiredXp = 19
                    },
                    new LevelProgressionEntry
                    {
                        Level = 19,
                        RequiredXp = 20
                    },
                    new LevelProgressionEntry
                    {
                        Level = 20,
                        RequiredXp = 21
                    }
                },
                MatchmakingParams = new MatchPrams
                {
                    PreferEmptyRoomsFrequency = 0f,
                    PreferFullRoomsFrequency = 1f
                },
                ServerMaintainence2 = new ServerMaintainence
                {
                    StartsInMinutes = 0
                },
                DailyObjectives = new Objective[][]
                {
                    new Objective[]
                    {
                        new Objective
                        {
                            type = 20,
                            score = 1
                        },
                        new Objective
                        {
                            type = 21,
                            score = 1
                        },
                        new Objective
                        {
                            type = 22,
                            score = 1
                        }
                    },
                    new Objective[]
                    {
                        new Objective
                        {
                            type = 32,
                            score = 1
                        },
                        new Objective
                        {
                            type = 21,
                            score = 1
                        },
                        new Objective
                        {
                            type = 22,
                            score = 1
                        }
                    },
                    new Objective[]
                    {
                        new Objective
                        {
                            type = 20,
                            score = 1
                        },
                        new Objective
                        {
                            type = 21,
                            score = 1
                        },
                        new Objective
                        {
                            type = 22,
                            score = 1
                        }
                    },
                    new Objective[]
                    {
                        new Objective
                        {
                            type = 20,
                            score = 1
                        },
                        new Objective
                        {
                            type = 21,
                            score = 1
                        },
                        new Objective
                        {
                            type = 22,
                            score = 1
                        }
                    },
                    new Objective[]
                    {
                        new Objective
                        {
                            type = 20,
                            score = 1
                        },
                        new Objective
                        {
                            type = 21,
                            score = 1
                        },
                        new Objective
                        {
                            type = 22,
                            score = 1
                        }
                    },
                    new Objective[]
                    {
                        new Objective
                        {
                            type = 20,
                            score = 1
                        },
                        new Objective
                        {
                            type = 21,
                            score = 1
                        },
                        new Objective
                        {
                            type = 22,
                            score = 1
                        }
                    },
                    new Objective[]
                    {
                        new Objective
                        {
                            type = 20,
                            score = 1
                        },
                        new Objective
                        {
                            type = 21,
                            score = 1
                        },
                        new Objective
                        {
                            type = 22,
                            score = 1
                        }
                    }
                },
                ConfigTable = new List<ConfigTableEntry>
                {
                    new ConfigTableEntry
                    {
                        Key = "Gift.DropChance",
                        Value = 0.5f.ToString()
                    },
                    new ConfigTableEntry
                    {
                        Key = "Gift.XP",
                        Value = 0.5f.ToString()
                    }
                },
                PhotonConfig = new Config2.PhotonConfig2
                {
                    CloudRegion = "us",
                    CrcCheckEnabled = false,
                    EnableServerTracingAfterDisconnect = false
                },
                AutoMicMutingConfig2 = new AutoMicMutingConfig
                {
                    MicSpamVolumeThreshold = 10.0,
                    MicSpamSamplePercentageForForceMute = 500.0,
                    MicSpamSamplePercentageForForceMuteToEnd = 0.0,
                    MicSpamSamplePercentageForWarning = 100.0,
                    MicSpamSamplePercentageForWarningToEnd = 0.0,
                    MicSpamWarningStateVolumeMultiplier = 10.0,
                    MicVolumeSampleInterval = 1.0,
                    MicVolumeSampleRollingWindowLength = 15.0
                }
            });
        }
        public class MatchPrams
        {
            // Token: 0x1700006E RID: 110
            // (get) Token: 0x0600013B RID: 315 RVA: 0x0000922F File Offset: 0x0000742F
            // (set) Token: 0x0600013C RID: 316 RVA: 0x00009237 File Offset: 0x00007437
            public float PreferFullRoomsFrequency { get; set; }

            // Token: 0x1700006F RID: 111
            // (get) Token: 0x0600013D RID: 317 RVA: 0x00009240 File Offset: 0x00007440
            // (set) Token: 0x0600013E RID: 318 RVA: 0x00009248 File Offset: 0x00007448
            public float PreferEmptyRoomsFrequency { get; set; }
        }
        public class ServerMaintainence
        {
            // Token: 0x17000077 RID: 119
            // (get) Token: 0x06000151 RID: 337 RVA: 0x000092E8 File Offset: 0x000074E8
            // (set) Token: 0x06000152 RID: 338 RVA: 0x000092F0 File Offset: 0x000074F0
            public int StartsInMinutes { get; set; }
        }
        public class AutoMicMutingConfig
        {
            // Token: 0x17000078 RID: 120
            // (get) Token: 0x06000154 RID: 340 RVA: 0x00009301 File Offset: 0x00007501
            // (set) Token: 0x06000155 RID: 341 RVA: 0x00009309 File Offset: 0x00007509
            public double MicSpamVolumeThreshold { get; set; }

            // Token: 0x17000079 RID: 121
            // (get) Token: 0x06000156 RID: 342 RVA: 0x00009312 File Offset: 0x00007512
            // (set) Token: 0x06000157 RID: 343 RVA: 0x0000931A File Offset: 0x0000751A
            public double MicVolumeSampleInterval { get; set; }

            // Token: 0x1700007A RID: 122
            // (get) Token: 0x06000158 RID: 344 RVA: 0x00009323 File Offset: 0x00007523
            // (set) Token: 0x06000159 RID: 345 RVA: 0x0000932B File Offset: 0x0000752B
            public double MicVolumeSampleRollingWindowLength { get; set; }

            // Token: 0x1700007B RID: 123
            // (get) Token: 0x0600015A RID: 346 RVA: 0x00009334 File Offset: 0x00007534
            // (set) Token: 0x0600015B RID: 347 RVA: 0x0000933C File Offset: 0x0000753C
            public double MicSpamSamplePercentageForWarning { get; set; }

            // Token: 0x1700007C RID: 124
            // (get) Token: 0x0600015C RID: 348 RVA: 0x00009345 File Offset: 0x00007545
            // (set) Token: 0x0600015D RID: 349 RVA: 0x0000934D File Offset: 0x0000754D
            public double MicSpamSamplePercentageForWarningToEnd { get; set; }

            // Token: 0x1700007D RID: 125
            // (get) Token: 0x0600015E RID: 350 RVA: 0x00009356 File Offset: 0x00007556
            // (set) Token: 0x0600015F RID: 351 RVA: 0x0000935E File Offset: 0x0000755E
            public double MicSpamSamplePercentageForForceMute { get; set; }

            // Token: 0x1700007E RID: 126
            // (get) Token: 0x06000160 RID: 352 RVA: 0x00009367 File Offset: 0x00007567
            // (set) Token: 0x06000161 RID: 353 RVA: 0x0000936F File Offset: 0x0000756F
            public double MicSpamSamplePercentageForForceMuteToEnd { get; set; }

            // Token: 0x1700007F RID: 127
            // (get) Token: 0x06000162 RID: 354 RVA: 0x00009378 File Offset: 0x00007578
            // (set) Token: 0x06000163 RID: 355 RVA: 0x00009380 File Offset: 0x00007580
            public double MicSpamWarningStateVolumeMultiplier { get; set; }
        }
        public class PhotonConfig2
        {
            // Token: 0x17000074 RID: 116
            // (get) Token: 0x0600014A RID: 330 RVA: 0x000092AD File Offset: 0x000074AD
            // (set) Token: 0x0600014B RID: 331 RVA: 0x000092B5 File Offset: 0x000074B5
            public string CloudRegion { get; set; }

            // Token: 0x17000075 RID: 117
            // (get) Token: 0x0600014C RID: 332 RVA: 0x000092BE File Offset: 0x000074BE
            // (set) Token: 0x0600014D RID: 333 RVA: 0x000092C6 File Offset: 0x000074C6
            public bool CrcCheckEnabled { get; set; }

            // Token: 0x17000076 RID: 118
            // (get) Token: 0x0600014E RID: 334 RVA: 0x000092CF File Offset: 0x000074CF
            // (set) Token: 0x0600014F RID: 335 RVA: 0x000092D7 File Offset: 0x000074D7
            public bool EnableServerTracingAfterDisconnect { get; set; }
        }
    }
}
