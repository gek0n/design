using System;
using System.IO;

namespace DIContainer.Commands
{

    public class PrintTimeCommand : BaseCommand
    {
        private TextWriter tw;

        public PrintTimeCommand(TextWriter tw)
        {
            this.tw = tw;
        }

        public override void Execute()
        {
            this.tw.WriteLine(DateTime.Now);
        }
    }

}