
namespace MEMAPI
{
    public class MemoryRange
    {
        public ulong Start { get; set; }
        public ulong End { get; set; }
        public ulong Size { get; set; }

        public MemoryRange()
        {

        }

        public MemoryRange(ulong start, ulong end)
        {
            Start = start;
            End = end;
            Size = End - Start;
        }
    }
}
