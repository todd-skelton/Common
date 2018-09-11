using System.Collections.Generic;

namespace Kloc.Common.Blockchaining
{
    public class Blockchain<T>
    {
        public List<Block<T>> _chain { get; private set; }

        public Blockchain()
        {
            _chain = new List<Block<T>>
            {
                new Block<T>(null, default)
            };
        }

        public IReadOnlyCollection<Block<T>> Chain => _chain.AsReadOnly();

        public void Add(T item)
        {
            var latestBlock = _chain[_chain.Count - 1];
            var block = new Block<T>(latestBlock, item);
            _chain.Add(block);
        }

        public bool IsValid()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                var currentBlock = _chain[i];
                var previousBlock = _chain[i - 1];

                if (currentBlock.Hash != currentBlock.CalculateHash())
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

        public Block<T> this[int i] => _chain[i];
    }
}
