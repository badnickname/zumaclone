namespace Balls
{
    public sealed class SubChain
    {
        public int Id { get; set; }
        public float Speed { get; set; }
        public int Count { get; set; }

        public SubChain(int id, float speed, int count)
        {
            Id = id;
            Speed = speed;
            Count = count;
        }
    }
}