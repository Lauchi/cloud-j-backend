using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;
using AudioEngine.Domain;
using MediaToolkit;
using MediaToolkit.Model;
using YoutubeExtractor;

namespace cloud_j_backend.Controllers.YTDownload
{
    [Route("downloadYT")]
    public class YTDownloadController : ApiController
    {
        public const string Mp3Path = @"C:\csharp\cloud-j-backend\AudioEngine\TestMp3s";
        public IMixer Mixer { get; }

        public YTDownloadController(IMixer mixer)
        {
            Mixer = mixer;
        }

        [HttpGet]
        public IHttpActionResult Init()
        {
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult DownloadYT(int channelId, [FromBody] MyUrl url)
        {
            IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(url.Url);
            VideoInfo video = videoInfos.First(info => info.VideoType == VideoType.Mp4);
            if (video.RequiresDecryption)
            {
                DownloadUrlResolver.DecryptDownloadUrl(video);
            }
            string videoPath = Path.Combine(Mp3Path, video.Title + video.VideoExtension);
            var videoDownloader = new VideoDownloader(video, videoPath);
            videoDownloader.DownloadProgressChanged += (sender, args) => Console.WriteLine(args.ProgressPercentage);
            videoDownloader.Execute();

            string mp3Path = Path.Combine(Mp3Path, video.Title + ".mp3");
            var inputFile = new MediaFile {Filename = videoPath};
            var outputFile = new MediaFile {Filename = mp3Path};
            using (var engine = new Engine())
            {
                engine.Convert(inputFile, outputFile);
            }

            return Ok(url);
        }

    }
}