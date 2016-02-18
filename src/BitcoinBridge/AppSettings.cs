﻿using System.IO;
using Sevices.Bitcoin;

namespace BitcoinBridge
{
    public class AppSettings
    {
        public SrvBitcoinRpcSettings BitcoinRpcSettings { get; set; }
        public string ConnectionString { get; set; }
    }


    public static class AppSettingsReader
    {
        public static AppSettings ReadSettings()
        {
            var json = File.ReadAllText("settings.json");
            return Newtonsoft.Json.JsonConvert.DeserializeObject<AppSettings>(json);
        }

    }
}