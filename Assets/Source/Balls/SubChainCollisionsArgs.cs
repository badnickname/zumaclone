namespace Balls
{
    public class SubChainCollisionsArgs
    {
        public int Id
        {
            get;
            private set;
        }

        public int Count
        {
            get;
            private set;
        }

        public SubChainCollisionsArgs(int id, int count)
        {
            Id = id;
            Count = count;
        }
    }
}