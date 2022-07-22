using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFMpegCore;
using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;

namespace HighlightToolGUI
{
    public class FFMPEG2
    {
        private String VIDEOPATH;
        private String SAVEPATH;
        private String EXEPATH;
        private String BINPATH;
        private String TMPPATH;
        private String DONEVIDEO;
        private String videoName;
        public FFMPEG2(string videoPath, string finalVideoName)
        {
            ConfigLoader configLoader = new ConfigLoader();
            this.VIDEOPATH = videoPath;
            this.DONEVIDEO = configLoader.DONEVIDEO;
            this.videoName = finalVideoName;
            this.SAVEPATH = configLoader.SAVEPATH;
            this.EXEPATH = configLoader.EXEPATH;
            this.BINPATH = configLoader.BINPATH;
            this.TMPPATH = configLoader.TMPPATH;

            GlobalFFOptions.Configure(new FFOptions { BinaryFolder = BINPATH, TemporaryFilesFolder = TMPPATH });
        }

        public void CutVideo()
        {

            var inputFile = new MediaFile { Filename = VIDEOPATH };
            var outputFile = new MediaFile { };

            using (var engine = new Engine(EXEPATH))
            {
                engine.GetMetadata(inputFile);
                int length = (int)GetVideoLength(VIDEOPATH).TotalSeconds;
                int sections = length / 19;
                int total = 0;
                var options = new ConversionOptions();

                for (int i = 0; i < 20; i++)
                {
                    outputFile = new MediaFile { Filename = SAVEPATH + "video" + i + ".mp4" };
                    options.CutMedia(TimeSpan.FromSeconds(total), TimeSpan.FromSeconds(30));
                    engine.Convert(inputFile, outputFile, options);
                    total = total + sections;
                }
            }
        }

        private TimeSpan GetVideoLength(string path)
        {
            var mediaInfo = FFProbe.Analyse(path);
            TimeSpan length = mediaInfo.Duration;
            return length;
        }

        public void ConcatenateVideos()
        {
            String[] videos = { SAVEPATH + "video0.mp4", SAVEPATH + "video1.mp4", SAVEPATH + "video2.mp4", SAVEPATH + "video3.mp4", SAVEPATH + "video4.mp4",
                                SAVEPATH + "video5.mp4", SAVEPATH + "video6.mp4", SAVEPATH + "video7.mp4", SAVEPATH + "video8.mp4", SAVEPATH + "video9.mp4", SAVEPATH + "video10.mp4", SAVEPATH + "video11.mp4", SAVEPATH + "video12.mp4",
                                SAVEPATH + "video13.mp4", SAVEPATH + "video14.mp4", SAVEPATH + "video15.mp4", SAVEPATH + "video16.mp4", SAVEPATH + "video17.mp4", SAVEPATH + "video18.mp4", SAVEPATH + "video19.mp4", };

            FFMpeg.Join(DONEVIDEO + videoName + ".mp4", videos);

        }

        public void DeleteTempFiles()
        {
            DirectoryInfo di = new DirectoryInfo(SAVEPATH);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }


    }
}