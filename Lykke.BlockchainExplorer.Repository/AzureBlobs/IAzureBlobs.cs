﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lykke.BlockchainExplorer.Repository.AzureBlobs
{
    internal interface IAzureBlob
    {
        Task SaveBlobAsync(string container, string key, Stream bloblStream);

        Task SaveBlobAsync(string container, string key, byte[] blob);

        Task<Stream> GetAsync(string blobContainer, string key);

        Task<List<string>> FindNamesByPrefixAsync(string container, string prefix);

        Task<List<string>> GetListOfBlobsAsync(string container);

        Task DelBlobAsync(string blobContainer, string key);
    }
}
