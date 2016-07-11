﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lykke.BlockchainExplorer.BlockchainDataProvider.NBitcoinIndexer.Converters
{
    public class JsonObjectException : Exception
    {
        public JsonObjectException(Exception inner, JsonReader reader)
            : base(inner.Message, inner)
        {
            Path = reader.Path;
        }
        public JsonObjectException(string message, JsonReader reader)
            : base(message)
        {
            Path = reader.Path;
        }

        public string Path
        {
            get;
            private set;
        }
    }
}
