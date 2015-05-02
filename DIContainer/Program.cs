using System;
using System.Collections;
using System.IO;
using System.Linq;
using DIContainer.Commands;
using Ninject;

namespace DIContainer
{
    public class Program
    {
        private readonly CommandLineArgs arguments;
        private readonly ICommand[] commands;

        public Program(CommandLineArgs arguments, params ICommand[] commands)
        {
            this.arguments = arguments;
            this.commands = commands;
        
        }

        static void Main(string[] args)
        {
            Lazy<HelpCommand> killer = new Lazy<HelpCommand>();
            TextWriter tw = Console.Out;
            var container = new StandardKernel();
            container.Bind<ICommand>().To<TimerCommand>();
            container.Bind<ICommand>().To<HelpCommand>();
            container.Bind<ICommand>().To<PrintTimeCommand>();
            container.Bind<CommandLineArgs>().ToConstant<CommandLineArgs>(new CommandLineArgs(args));
            container.Bind<TextWriter>().ToMethod(c => Console.Out);
            container.Bind<IList>().ToConstant<IList>(container.GetAll<ICommand>().ToList());
//            var arguments = new CommandLineArgs(args);
//            var printTime = new PrintTimeCommand();
//            var timer = new TimerCommand(arguments);
//            var help = new HelpCommand();
//            var commands = new ICommand[] { printTime, timer, help };
//            new Program(arguments, commands).Run();
            var progr = container.Get<Program>();
//            var b = ;
            progr.Run(tw);
        }

        public void Run(TextWriter tw)
        {
            if (arguments.Command == null)
            {
                tw.WriteLine("Please specify <command> as the first command line argument");
                return;
            }
            var command = commands.FirstOrDefault(c => c.Name.Equals(arguments.Command, StringComparison.InvariantCultureIgnoreCase));
            if (command == null)
                tw.WriteLine("Sorry. Unknown command {0}", arguments.Command);
            else
                command.Execute();
        }
    }
}
