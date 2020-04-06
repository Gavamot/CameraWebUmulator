using Infrastructure.Rep;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Infrastructure.Test
{
    public class StaticInfoSnapshotRepTest
    {
        InfoSnapshotRep rep;

        [SetUp]
        public void Init()
        {
            rep = new InfoSnapshotRep(new DateTimeService(), new Config());
        }

        [Test]
        public async Task GetSnapshot_Works()
        {
            var snapshot = await rep.GetSnapshot();
            Assert.IsNotNull(snapshot);
            Assert.IsNotEmpty(snapshot.Data);
        }
    }
}