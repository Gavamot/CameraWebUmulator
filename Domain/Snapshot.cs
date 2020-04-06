using System;

namespace Domain
{
    public class Snapshot
    {
        public Snapshot() { }
        public Snapshot(byte[] data)
        {
            Data = data;
        }

        public byte[] Data { get; set; }
        public string Format { get; set; } = "jpeg";
    }
}
