using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Abstractions.Rep
{
    public interface ISnapshotRep
    {
        Task<Snapshot> GetSnapshot();
    }
}
