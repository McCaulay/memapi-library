﻿
namespace MEMAPI
{
    public class Module
    {
        public int Id { get; }
        public string Name { get; }
        public ulong CodeSize { get; }
        public ulong DataSize { get; }

        public Module(int id, string name, ulong codeSize, ulong dataSize)
        {
            Id = id;
            Name = name;
            CodeSize = codeSize;
            DataSize = dataSize;
        }
    }
}
