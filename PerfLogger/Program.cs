using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace PerfLogger
{
	class Program
	{
		static void Main(string[] args)
		{
			
			var sum = 0.0;
			using (PerfLogger.Measure(t => Console.WriteLine("for: {0}", t)))
				for (var i = 0; i < 100000000; i++) sum += i;
			using (PerfLogger.Measure(t => Console.WriteLine("linq: {0}", t)))
				sum += Enumerable.Range(0, 100000000).Sum(i => (double)i);
			Console.WriteLine(sum);
			
		}

	    class PerfLogger : IDisposable
	    {
	        private static Action<Double> a;
	        private static Double t;
	        private PerfLogger()
	        {
	            t = Convert.ToDouble(DateTime.Now.Ticks);
	        }

	        public static PerfLogger Measure(Action<Double> act)
	        {
	            a = act;
                t = Convert.ToDouble(DateTime.Now.Ticks) - t;
                return new PerfLogger();
	        }

	        public void Dispose()
	        {
	            a(t);
	        }
	    }
	}
}
