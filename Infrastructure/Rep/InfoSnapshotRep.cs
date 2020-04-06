using App.Abstractions.Rep;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Resources;
using System.Drawing.Drawing2D;
using System.Net.NetworkInformation;
using System.Linq;
using App.Abstractions.Config;

namespace Infrastructure.Rep
{
    public class InfoSnapshotRep : ISnapshotRep
    {
        readonly IDateTime dateTime;
        readonly IInfoSnapshotConfig config;

        public InfoSnapshotRep(IDateTime dateTime, IInfoSnapshotConfig config)
        {
            this.dateTime = dateTime;
            this.config = config;
        }

        public async Task<Snapshot> GetSnapshot()
        {
            await SleepRandom();

            var image = GetSnapshotResource();

            string date = GetDateTimeString() + Environment.NewLine;
            string ip = GetIpAdresses()
              .Aggregate((total, next) => total += next + Environment.NewLine);
            image = AddInfo(image, date, ip);

            var snapshotData = ImageToByteArray(image);

            return new Snapshot(snapshotData);
        }

        private Bitmap AddInfo(Bitmap screen, string date, string ip)
        {
            int textHeight = 40;
            int rectHeight = (int)(textHeight * 1.4);
            RectangleF rectfDate = new RectangleF(5, 0, screen.Width, rectHeight);
            
            Graphics g = Graphics.FromImage(screen);
           
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            g.DrawString(date, new Font("Tahoma", textHeight), Brushes.Blue, rectfDate);

            int marginY = textHeight + 20;
            RectangleF rectfIp = new RectangleF(5, marginY, screen.Width, rectHeight);
            g.DrawString(ip, new Font("Tahoma", textHeight), Brushes.Red, rectfIp);

            g.Flush();

            return screen;
        }

        private string GetDateTimeString()
        {
            return dateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.ffff");
        }

        private IEnumerable<string> GetIpAdresses()
        {
            return NetworkInterface.GetAllNetworkInterfaces()
                .Select(x => x.GetIPProperties())
                .SelectMany(x => x.UnicastAddresses)
                .Select(x => x.Address.MapToIPv4().ToString())
                .Where(IsExternalIp);
        }

        private bool IsExternalIp(string ip)
        {
            string[] localIpFirstPats = new []
            {
                "0.",
                "127.",
                "192."
            };
            return !localIpFirstPats.Any(x => ip.StartsWith(x));
        }

        private async Task SleepRandom()
        {
            var rnd = new Random(dateTime.Now.Millisecond);
            int sleepMs = rnd.Next(config.MinGetSnapshotDelayMs, config.MaxGetSnapshotDelayMs);
            await Task.Delay(sleepMs);
        }

        Bitmap GetSnapshotResource()
        {
            using (var ms = new MemoryStream(Resource.snapshot))
            {
                return new Bitmap(ms);
            }
        }

        byte[] ImageToByteArray(Bitmap img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, img.RawFormat);
                return stream.ToArray();
            }
        }
    }
}
