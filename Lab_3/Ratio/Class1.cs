using System;
using NUnit.Framework;
using RatioNS;

namespace RatioTests
{
    [TestFixture]
    public class RatioTestsClass
    {
    	
    	[Test] 
    	public void __5_10_Reduce__1_2__Returned() 
    	{
    		Ratio result = new Ratio(5, 10);
    		
    		Assert.AreEqual(result.Numerator, 1);
            Assert.AreEqual(result.Denominator, 2);
    	}
    	
    	[Test] 
    	public void UnaryPlusCheck() 
    	{
    		Ratio a = new Ratio(3, 7);
    		
    		Ratio result = +a;
    		
    		Assert.AreEqual(result.Numerator, 3);
            Assert.AreEqual(result.Denominator, 7);
    	}
    	
    	[Test] 
    	public void UnaryMinusCheck() 
    	{
    		Ratio a = new Ratio(3, 7);
    		
    		Ratio result = -a;
    		
    		Assert.AreEqual(result.Numerator, -3);
            Assert.AreEqual(result.Denominator, 7);
    	}
    	
        [Test]
        public void __2_1__Plus__5_1__7_1__Returned()
        {
        	Ratio a = new Ratio(5);
        	Ratio b = new Ratio(2);
        	
            Ratio result = a + b;  

            Assert.AreEqual(result.Numerator, 7);
            Assert.AreEqual(result.Denominator, 1);
        }
        
        [Test]
        public void __1_2__Plus__1_3__5_6__Returned()
        {
        	Ratio a = new Ratio(1, 2);
        	Ratio b = new Ratio(1, 3);
        	
            Ratio result = a + b;  

            Assert.AreEqual(result.Numerator, 5);
            Assert.AreEqual(result.Denominator, 6);
        }
        [Test]
        public void __1_2__Minus__1_3__1_6__Returned()
        {
        	Ratio a = new Ratio(1, 2);
        	Ratio b = new Ratio(1, 3);
        	
            Ratio result = a - b;  

            Assert.AreEqual(result.Numerator, 1);
            Assert.AreEqual(result.Denominator, 6);
        }
        
        [Test]
        public void __1_2__Mult__2_3__1_3__Returned()
        {
        	Ratio a = new Ratio(1, 2);
        	Ratio b = new Ratio(2, 3);
        	
            Ratio result = a * b;  

            Assert.AreEqual(result.Numerator, 1);
            Assert.AreEqual(result.Denominator, 3);
        }
        
        [Test]
        public void __1_2__Div__3_2__1_3__Returned()
        {
        	Ratio a = new Ratio(1, 2);
        	Ratio b = new Ratio(3, 2);
        	
            Ratio result = a / b;  

            Assert.AreEqual(result.Numerator, 1);
            Assert.AreEqual(result.Denominator, 3);
        }
        
        [Test]
		public void ExceptionWhenDenominatorIsZero()
		{
        	Assert.Throws<Ratio.DivisionByZeroException>(() => new Ratio(5, 0));
		}
		
		[Test]
		public void ExceptionWhenDivideByZero()
		{
			Ratio a = new Ratio(5);
			Ratio b = new Ratio(0);
			
			Assert.Throws<Ratio.DivisionByZeroException>(() => {Ratio result = a / b;});
		}
		
    }
}