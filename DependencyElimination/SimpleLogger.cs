using System;
using System.IO;

namespace DependencyElimination
{
    class SimpleLogger
    {
        private TextWriter tw;
        public SimpleLogger()
        {
            this.tw = Console.Out;
        }

        public SimpleLogger(TextWriter tw)
        {
            this.tw = tw;
        }

        public void log(string msg)
        {
            this.tw.WriteLine(msg);
        }
    } 
}