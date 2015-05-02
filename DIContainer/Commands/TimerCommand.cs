using System;
using System.IO;
using System.Threading;

namespace DIContainer.Commands
{
    public class TimerCommand : BaseCommand
    {
        private readonly CommandLineArgs arguments;
        private TextWriter tw;

        public TimerCommand(CommandLineArgs arguments, TextWriter tw)
        {
            this.arguments = arguments;
            this.tw = tw;
        }

        public override void Execute()
        {
            var timeout = TimeSpan.FromMilliseconds(arguments.GetInt(0));
            this.tw.WriteLine("Waiting for " + timeout);
            Thread.Sleep(timeout);
            this.tw.WriteLine("Done!");
        }
    }
}