﻿using NBitcoin.Indexer;
using Newtonsoft.Json;
using System;

namespace Lykke.BlockchainExplorer.BlockchainDataProvider.NBitcoinIndexer.Converters
{
    public class BalanceLocatorJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(BalanceLocator).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                return reader.TokenType == JsonToken.Null ? null : BalanceLocator.Parse(reader.Value.ToString());
            }
            catch (FormatException)
            {
                throw new JsonObjectException("Invalid BalanceLocator", reader);
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value != null)
                writer.WriteValue(value.ToString());
        }
    }
}