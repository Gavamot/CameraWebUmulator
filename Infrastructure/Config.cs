using App.Abstractions.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class Config : IConfig
    {
        public int MinGetSnapshotDelayMs { get; set; }
        public int MaxGetSnapshotDelayMs { get; set; }
    }
}
