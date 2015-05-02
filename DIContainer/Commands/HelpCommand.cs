using System;
using System.Collections.Generic;
using System.IO;

namespace DIContainer.Commands
{
    public class HelpCommand : BaseCommand
    {
        private Lazy<IEnumerable<ICommand>> cmds;
        private TextWriter tw;
        public HelpCommand(Lazy<IEnumerable<ICommand>> commands, TextWriter tw)
        {
            this.cmds = commands;
            this.tw = tw;
        }

        public override void Execute()
        {
//            Console.WriteLine("It's my help:\nTimer \nPrintTime");
            this.tw.WriteLine("My commands is:\n");
            foreach (var cmd in cmds.Value)
            {
                this.tw.WriteLine(cmd.Name.ToString() + "\n");
            }
        }
        
    }
}