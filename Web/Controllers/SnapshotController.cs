using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Abstractions.Rep;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    [ApiController]
    public class SnapshotController : ControllerBase
    {
        readonly ILogger<SnapshotController> log;
        readonly ISnapshotRep rep;
        public SnapshotController(ILogger<SnapshotController> log, ISnapshotRep rep)
        {
            this.rep = rep;
            this.log = log;
        }

        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> GetSnapshot()
        {
            var snapshot = await rep.GetSnapshot();
            return File(snapshot.Data, $"image/{snapshot.Format}");
        }
    }
}
