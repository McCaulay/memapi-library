
namespace MEMAPI
{
    public class Thread
    {
        public uint Id { get; }

        public Thread(uint id)
        {
            Id = id;
        }

        public string[] toArray()
        {
            return new string[] { Id.ToString() };
        }
    }
}
