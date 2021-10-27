using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace PollingApp.Entities
{
    public class BlockChain<T>
    {
        public IList<Block<T>> Chain { get; private set; }
        public int Difficulty { get; private set; } = 3;

        public BlockChain()
        {
            InitializeChain();
        }
        public BlockChain(IList<Block<T>> list)
        {
            Chain = list;
        }
        private void InitializeChain()
        {
            Chain = new List<Block<T>>();
            AddGenesisBlock();
        }
        private Block<T> CreateGenesisBlock()
        {
            Block<T> block = new Block<T>(DateTime.Now, null,default);
            Mine(Difficulty,ref block);
            return block;
        }
        private void AddGenesisBlock()
        {
            Chain.Add(CreateGenesisBlock());
        }
        public Block<T> GetLatestBlock()
        {
            return Chain[Chain.Count - 1];
        }
        public void ChainSet(IList<Block<T>> blocks) => Chain = blocks;
        public IList<Block<T>> GetBlocks() => Chain;
        public void AddBlock(Block<T> block)
        {
            Block<T> latestblock = GetLatestBlock();
            block.Index = latestblock.Index + 1;
            block.PreviousHash = latestblock.Hash;
            block.Hash = CalculateHash(block);
            Mine(this.Difficulty,ref block);
            Chain.Add(block);
        }
        public void CreateTransaction(T transaction)
        {
            Block<T> block = new Block<T>(DateTime.Now, GetLatestBlock().Hash, transaction);
            AddBlock(block);
        }
        public static bool IsValid(IList<Block<T>> list)
        {
            for (int i = 1; i < list.Count; i++)
            {
                Block<T> currentBlock = list[i];
                Block<T> previousBlock = list[i - 1];
                if (currentBlock.Hash != CalculateHash(currentBlock))
                {
                    return false;
                }
                if (currentBlock.PreviousHash != previousBlock.Hash)
                {
                    return false;
                }

            }
            return true;
        }
        public static bool BlockChainSetting(IList<Block<T>> chain)
        {
            if (chain[0].PreviousHash == null && chain[1].PreviousHash == null)
            {
                chain.RemoveAt(0);
            }
            return true;
        }
        private static string CalculateHash(Block<T> block)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] inbytes = Encoding.ASCII.GetBytes($"{block.TimeStamp}-{block.PreviousHash ?? ""}-{JsonConvert.SerializeObject(block.Transactions)}-{block.Nonce}");
            byte[] outbytes = sha256.ComputeHash(inbytes);
            return Convert.ToBase64String(outbytes);
        }
        private static void Mine(int difficulty,ref Block<T> block)
        {
            var leadingZeros = new string('0', difficulty);
            while (block.Hash == null || block.Hash.Substring(0, difficulty) != leadingZeros)
            {
                block.Nonce++;
                block.Hash =CalculateHash(block);
            }

        }
    }
}
