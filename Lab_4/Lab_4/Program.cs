using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab_4
{	
	class BitContainer : IEnumerable<bool> {
		
		List<int> list; 
		int free;
		public BitContainer() {
			list = new List<int>();
			list.Add(0);
			free = 32;
		}
		
		public int Length {
			get { return list.Count * 32 - free; }
		}
		
		public override string ToString()
		{
			string res = "";
			foreach (bool bit in this) {
				res += (bit ? "1" : "0");
			}
			return res;
		}

		public void Clear() {
			list.Clear();
			list.Add(0);
			free = 32;
		}
		
		public bool getBit(int i) {
			if (i < 0 || i >= Length)
				throw new ArgumentException("Invalid index");
			return Convert.ToBoolean((list[i / 32] >> (i % 32)) & 1);
		}
		
		public void setBit(int i, bool bit) {
			if (i < 0 || i >= Length)
				throw new ArgumentException("Invalid index");
			list[i / 32] &= ~(1 << (i % 32)); 
			list[i / 32] |= (bit ? 1 : 0) << (i % 32);
		}
		
		public bool this[int i] {
			get { return getBit(i); }
			set { setBit(i, value); }
		}
		
		public void pushBit(bool bit) {
			if (bit) {
				list[list.Count - 1] |= 1 << (32 - free);
			}
			free--;
			if (free == 0) {
				list.Add(0);
				free = 32;
			}
		}
		
		public void pushBit(int bit) {
			if (bit != 1  && bit != 0) 
				throw new ArgumentException("Invalid data: " +
				                            "int bit must be only 0 or 1");
			pushBit(Convert.ToBoolean(bit));
		}
		
		
		public void Insert(int index, bool bit) {
			if (index < 0 || index > Length)
				throw new ArgumentException("Invalid index");
			for (int i = list.Count - 1; i > index / 32; i--) {
				list[i] <<= 1;
				list[i] |= list[i - 1] >> 31;
			}
			
			int right, left, mid;
			int mask = unchecked((1 << (index % 32)) - 1);
			right = list[index / 32] & ~mask;
			left = list[index / 32] & mask;
			mid = (bit? 1 : 0) << (index % 32);
			list[index / 32] = (right << 1) | mid | left;
			
			free--;
			if (free == 0) {
				list.Add(0);
				free = 32;
			}
		}
		
		public void RemoveAt(int index) {
			if (index < 0 || index >= Length)
				throw new ArgumentException("Invalid index");
			
			int mask = unchecked((1 << (index % 32)) - 1);
			int left = list[index / 32] & mask;
			list[index / 32] >>= 1;
			list[index / 32] &= ~mask;
			list[index / 32] |= left;
			
			for (int i = index / 32; i < list.Count - 1; i++) {
				list[i] |= (list[i + 1] & 1) << 31;
				list[i + 1] >>= 1;
			}
			free++;
			if (free == 33) {
				list.RemoveAt(list.Count - 1);
				free = 1;
			}
		}
		
		
		public IEnumerator<bool> GetEnumerator() {
			return new BitEnumerator(this);
		}
		
		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
	    }

		class BitEnumerator : IEnumerator<bool> {
			BitContainer container;
			int position = -1;
			
			public BitEnumerator(BitContainer b) {
				this.container = b;
			}
			
			public bool Current {
				get {
					return container.getBit(position);
				}
			}
			
			object IEnumerator.Current {
				get { return Current; }
			}
			
			public bool MoveNext() {
				position++;
 		    	return (position < container.Length);
			}
			
			void IDisposable.Dispose() { }
			
			public void Reset() {
		        position = -1;
		    }
			
		}
	}
	
	class Program
	{
		public static void Main(string[] args)
		{
			BitContainer b = new BitContainer();
			Console.WriteLine("Length of empty container is {0}", b.Length);
			
			for (int i = 0; i < 2; i++)  {
				b.pushBit(0);
				b.pushBit(true);
			}
			Console.WriteLine("Length of container with 4 elements is {0}", b.Length);
			Console.WriteLine("Container with 0101 is equal to {0}", b.ToString());
			
			foreach (bool bit in b) {
				Console.WriteLine(bit);
			}
			
			for (int i = 0; i < b.Length; i++) {
				b[i] = !b[i];
			}
			Console.WriteLine("Container with 1010 is equal to {0}", b.ToString());
			
			b.RemoveAt(3);
			b.RemoveAt(1);
			
			Console.WriteLine("Container without zeroes is equal to {0}", b.ToString());
			
			b.Insert(1, false);
			b.Insert(3, false);
			
			Console.WriteLine("Container with zeroes is equal to {0}", b.ToString());
			
			Console.Write("\nPress any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}