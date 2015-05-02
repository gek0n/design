using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Threading;

namespace FluentTask
{
	internal class Program
	{
		private static void Main()
		{


			var behaviour = new Behavior()
				.Say("Привет мир!")
				.UntilKeyPressed(b => b
					.Say("Ля-ля-ля!")
					.Say("Тру-лю-лю"))
				.Jump(JumpHeight.High)
				.UntilKeyPressed(b => b
					.Say("Aa-a-a-a-aaaaaa!!!")
					.Say("[набирает воздух в легкие]"))
				.Say("Ой!")
				.Delay(TimeSpan.FromSeconds(1))
				.Say("Кто здесь?!")
				.Delay(TimeSpan.FromMilliseconds(2000));

			behaviour.Execute();


		}

	    class Behavior
	    {

	        private readonly List<Action> actions;

	        public Behavior()
	        {
	            actions = new List<Action>();
	        }

	        private Behavior(IEnumerable<Action> Action)
	        {
	            if (this.actions == null)
	            {
                    this.actions = new List<Action>();
	            }
	            this.actions = actions.ToList();
	        }

	        public Behavior Say(string mes)
	        {
                this.actions.Add(new Action(() => Console.WriteLine(mes)));
	            return this;
	        }

            public Behavior Jump(JumpHeight hight)
	        {
                this.actions.Add(new Action(() => Console.WriteLine("[подпрыгнул на " + hight.ToString() + " метров]")));
                return this;
	        }

            public Behavior UntilKeyPressed(Func<Behavior, Behavior> bh)
	        {
                this.actions.Add(() =>
                {
                    while (! Console.KeyAvailable)
                    {
                        var b = new Behavior();
                        bh(b);
                        b.Execute();
                        Thread.Sleep(500);
                    }
                    Console.ReadKey(true);
                }
                );
                return this;
	        }

	        public Behavior Delay(TimeSpan tm)
	        {
                this.actions.Add(new Action(() => Thread.Sleep(tm)));
	            return this;
	        }

            public void Execute()
	        {
                foreach (var action in actions)
                {
                    action();
                }
	        }
	    }
	}
}