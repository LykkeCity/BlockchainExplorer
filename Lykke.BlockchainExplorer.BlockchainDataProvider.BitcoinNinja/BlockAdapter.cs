using Lykke.BlockchainExplorer.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lykke.BlockchainExplorer.Core.Domain;
using Lykke.BlockchainExplorer.BlockchainDataProvider.BitcoinNinja.Client;
using Lykke.BlockchainExplorer.Settings;
using NBitcoin.OpenAsset;

namespace Lykke.BlockchainExplorer.BlockchainDataProvider.BitcoinNinja
{
    public class BlockAdapter : IBlockProvider
    {
        private BitcoinNinjaClient _client;
        private readonly ITransactionProvider _transactionProvider;

        public BlockAdapter(ITransactionProvider transactionProvider)
        {
            _transactionProvider = transactionProvider;
            var settings = new BitcoinNinjaSettings()
            {
                Network = AppSettings.Network,
                UrlMain = AppSettings.SqlNinjaUrlMain,
                UrlTest = AppSettings.SqlNinjaUrlTest
            };

            _client = new BitcoinNinjaClient(settings);
        }

        public async Task<Block> GetBlock(string id)
        {
            var b = await _client.GetInformationBlockAsync(id);

            var transactionList = new List<Core.Domain.Transaction>();
            var transactionIds = b.Transactions.Select(p => p.TxId).ToList();

            foreach (var transactionId in transactionIds)
            {
                transactionList.Add(await _transactionProvider.GetTransaction(transactionId));
            }

            var block = new Block()
            {
                Hash = b.Hash,
                Height = b.Height,
                Time = b.Time,
                Confirmations = b.Confirmations,
                Difficulty = b.Difficulty,
                MerkleRoot = b.MerkleRoot,
                Nonce = b.Nonce,
                TotalTransactions = b.TotalTransactions, 
                PreviousBlock = b.PreviousBlock,
                Transactions = transactionList
            };
             
            return block;
        }
         
        public async Task<Block> GetLastBlock()
        {
            var b = await _client.GetLastBlockAsync();

            var block = new Block()
            {
                Hash = b.Hash,
                Height = b.Height
            };

            return block;
        }
    }
}