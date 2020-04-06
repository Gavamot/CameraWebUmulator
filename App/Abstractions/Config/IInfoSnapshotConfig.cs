using System;
using System.Collections.Generic;
using System.Text;

namespace App.Abstractions.Config
{
    public interface IInfoSnapshotConfig
    {
        public int MinGetSnapshotDelayMs { get; set; }
        public int MaxGetSnaphotDelayMs { get; set;}
    }
}
