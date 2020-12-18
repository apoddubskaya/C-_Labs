using System;
using RatioNS;

namespace Lab_3
{
	
	class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			
			Ratio b = new Ratio(10, 2);
			Ratio a = new Ratio(2, 7);
			Console.WriteLine("{0}  {1}", a.ToString(), b.ToString());
			
			Console.WriteLine("{0}", a + b);
			Console.WriteLine("{0}", a - b);
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}