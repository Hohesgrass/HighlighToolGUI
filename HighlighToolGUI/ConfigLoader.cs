using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighlightToolGUI
{
    public class ConfigLoader
    {
        public String SAVEPATH = @".\ffmpeg\video\";
        public String EXEPATH = @".\ffmpeg\bin\ffmpeg.exe";
        public String BINPATH = @".\ffmpeg\bin\";
        public String TMPPATH = @".\ffmpeg\tmp\";
        public String DONEVIDEO = @".\donevideos\";
        private String CONFPATH = @".\config.txt";

        private String[] lines;
        public ConfigLoader()
        {

        }

        public void SetSavepath(string path)
        {
            lines[0] = path;
            File.WriteAllLines(CONFPATH, lines);
            lines = File.ReadAllLines(CONFPATH);
        }

        public void SetExepath(string path)
        {
            lines[1] = path;
            File.WriteAllLines(CONFPATH, lines);
            lines = File.ReadAllLines(CONFPATH);
        }

        public void SetBinpath(string path)
        {
            lines[2] = path;
            File.WriteAllLines(CONFPATH, lines);
            lines = File.ReadAllLines(CONFPATH);
        }

        public void SetTmppath(string path)
        {
            lines[3] = path;
            File.WriteAllLines(CONFPATH, lines);
            lines = File.ReadAllLines(CONFPATH);
        }

    }
}
