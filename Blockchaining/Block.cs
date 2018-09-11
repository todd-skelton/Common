using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace Kloc.Common.Blockchaining
{
    public class Block<T>
    {
        private Block()
        {
        }

        public Block(Block<T> previousBlock, T data)
        {
            Id = previousBlock == null ? 0 : previousBlock.Id + 1;
            PreviousHash = previousBlock?.Hash;
            Timestamp = DateTimeOffset.Now;
            Data = data;
            Hash = CalculateHash();
        }

        public long Id { get; private set; }
        public DateTimeOffset Timestamp { get; private set; }
        public string PreviousHash { get; private set; }
        public string Hash { get; private set; }
        public T Data { get; private set; }

        public string CalculateHash()
        {
            SHA256 sha256 = SHA256.Create();

            byte[] outputBytes = sha256.ComputeHash(GetBytes());

            return Convert.ToBase64String(outputBytes);
        }

        private byte[] GetBytes()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, Id);
                formatter.Serialize(ms, Timestamp);
                if (PreviousHash != null) formatter.Serialize(ms, PreviousHash);
                if (Data != null) formatter.Serialize(ms, Data);
                return ms.ToArray();
            }
        }
    }
}
