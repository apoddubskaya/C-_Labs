using System;

namespace RatioNS
{
	/// <summary>
	/// Description of MyClass.
	/// </summary>
	public class Ratio {
		
		public class DivisionByZeroException : ArgumentException {
			public DivisionByZeroException(string msg) : base(msg) {}
		}
		
		private int numerator;
		public int Numerator {
			get {return numerator; }
			set {
				numerator = value;
				reduce();
			}
		}
		private int denominator;
		public int Denominator {
			get {return denominator; }
			set {
				if (value == 0) {
					throw new DivisionByZeroException("Denominator is 0");
				}
				denominator = value;
				reduce();
			}
		}
		public Ratio(int num) {
			numerator = num;
			denominator = 1;
		}
		public Ratio(int num, int denom) {
			if (denom == 0) {
				throw new DivisionByZeroException("Denominator is 0");
			}
			numerator = num;
			denominator = denom;
			reduce();
		}
		
		public static Ratio operator + (Ratio a) {
			return new Ratio(a.numerator, a.denominator);
		}
		
		public static Ratio operator - (Ratio a) {
			return new Ratio(-a.numerator, a.denominator);
		}
		
		public static Ratio operator + (Ratio a, Ratio b) {
			int GCD = gcd(a.denominator, b.denominator);
			return new Ratio(a.numerator * (b.denominator / GCD) + (a.denominator / GCD) * b.numerator,
			                 (a.denominator / GCD)* b.denominator);
		}
		
		public static Ratio operator - (Ratio a, Ratio b) {
			int GCD = gcd(a.denominator, b.denominator);
			return new Ratio(a.numerator * (b.denominator / GCD) - (a.denominator / GCD) * b.numerator,
			                 (a.denominator / GCD)* b.denominator);
		}
		
		public static Ratio operator * (Ratio a, Ratio b) {
			int GCDad = gcd(a.numerator, b.denominator),
				GCDbc = gcd(b.numerator, a.denominator);
			return new Ratio((a.numerator / GCDad)* (b.numerator / GCDbc), (a.denominator / GCDbc)* (b.denominator / GCDad));
		}
		
		public static Ratio operator / (Ratio a, Ratio b) {
			if (b.numerator == 0) {
				throw new DivisionByZeroException("You делишь на 0 !!!!!!!!!!!");
			}
			int GCDac = gcd(a.numerator, b.numerator),
				GCDbd = gcd(b.denominator, a.denominator);
			return new Ratio((a.numerator / GCDac)* (b.denominator / GCDbd), (a.denominator / GCDbd)* (b.numerator / GCDac));
		}
		
		public double ToDouble() {
			return numerator / (double)denominator;
		}
		public override string ToString()
		{
			return string.Format("{0} / {1}", numerator, denominator);
		}
		
		private static int gcd(int a, int b) {
			if (b == 0) 
				return a;
			return gcd(b, a % b);
		}
		private void reduce() {
			int GCD = gcd(Math.Abs(numerator), Math.Abs(denominator));
			if (GCD != 1) {
				numerator /= GCD;
				denominator /= GCD;
			}
		}
		
	}
}