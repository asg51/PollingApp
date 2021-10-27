using System;

namespace PollingApp.Entities
{
    public class Block<T>
    {
        public int Index { get; set; }
        public DateTime TimeStamp { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }
        public T Transactions { get; set; }

        public int Nonce { get; set; } = 0;
        public Block(DateTime timeStamp, string previousHash, T transactions)
        {
            Index = 0;
            TimeStamp = timeStamp;
            PreviousHash = previousHash;
            Transactions = transactions;
        }
    }
}

